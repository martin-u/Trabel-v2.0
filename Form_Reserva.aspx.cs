using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dao;
using Entidades;

public partial class Form_Reserva : System.Web.UI.Page
{   

    protected void Page_Load(object sender, EventArgs e)
    {
        List<string> roles = (List<string>)Session["Roles"];
        bool acceso = false;
        foreach (string rol in roles)
        {
            if (rol == "Administrador")
            {
                acceso = true;
                break;
            }
        }
        if (!acceso) Response.Redirect("InicioSesion.aspx");

        if (Session["Usuario"] == string.Empty)
        {
            //Usuario Anónimo
            Response.Redirect("InicioSesion.aspx");
        }

        if (!IsPostBack)
        {
            txt_fechaReserva.Text = DateTime.Today.ToShortDateString(); 
            cargarComboEstado();
            cargarComboHotel();
            cargarComboTipoDoc();
            cargarComboViaje();
            
        }
    }

    protected void cargarComboEstado()
    {
        ddl_estado.DataSource = EstadoDao.consultarEstado();
        ddl_estado.DataTextField = "nombre";
        ddl_estado.DataValueField = "idEstado";
        ddl_estado.DataBind();        
    }

    protected void cargarComboViaje()
    {
        ddl_viaje.DataSource = ViajeDao.consultarViaje();
        ddl_viaje.DataTextField = "nombreViaje";
        ddl_viaje.DataValueField = "idViaje";
        ddl_viaje.DataBind();
        ddl_viaje.Items.Insert(0, new ListItem("(Viaje)", "0"));
        ddl_viaje.SelectedIndex = 0;
    }

    protected void cargarComboHotel()
    {
        ddl_hotel.DataSource = HotelDao.consultarHotel();
        ddl_hotel.DataTextField = "nombre";
        ddl_hotel.DataValueField = "idHotel";
        ddl_hotel.DataBind();
        ddl_hotel.Items.Insert(0, new ListItem("(Hotel)", "0"));
        ddl_hotel.SelectedIndex = 0;
    }

    protected void cargarComboTipoDoc()
    {
        ddl_tipoDNI.DataSource = TipoDocumentoDao.consultarTipoDoc();
        ddl_tipoDNI.DataTextField = "nombre";
        ddl_tipoDNI.DataValueField = "idTipoDocumento";
        ddl_tipoDNI.DataBind();
        ddl_tipoDNI.Items.Insert(0, new ListItem("(TipoDocumento)", "0"));
        ddl_tipoDNI.SelectedIndex = 0;
    }
    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {            
                ReservaEntidad reserva = new ReservaEntidad();
                reserva.fechaReserva = DateTime.Parse(txt_fechaReserva.Text);
                reserva.idEstado = int.Parse(ddl_estado.SelectedValue);
                reserva.fechaIda = DateTime.Parse(txt_fechaIda.Text);
                reserva.fechaVuelta = DateTime.Parse(txt_fechaVuelta.Text);
                reserva.idViaje = int.Parse(ddl_viaje.SelectedValue);
                reserva.idHotel = int.Parse(ddl_hotel.SelectedValue);
                reserva.precioTotal = float.Parse(txt_precioTotal.Text);

                ReservaDao.registrarReserva(reserva, ListaTurista);
            bloquearDetalle();
            Session.RemoveAll();
            limpiarReserva();
            cargarGrillaTurista();
        }       

    }

    protected void cargarGrillaTurista()
    {
        dgv_detalleReserva.DataSource = ListaTurista;
        dgv_detalleReserva.DataBind();
    }
    protected void btn_agregar_Click(object sender, EventArgs e)
    {
        //TuristaEntidad turista = new TuristaEntidad();
        //turista.DNI = int.Parse(txt_dniTurista.Text);
        //turista.idTipoDocumento = int.Parse(ddl_tipoDNI.SelectedValue);
        //turista.nombre = txt_nombre.Text;
        //turista.apellido = txt_apellido.Text;
        //turista.fechaNacimiento = DateTime.Parse(txt_fechaNacimiento.Text);
        //TuristaDao.registrarTuristas(turista);
        //limpiarDetalle();
        //cargarGrillaTurista();
        ListaTurista.Add(new TuristaEntidad()
        {
            DNI = int.Parse(txt_dniTurista.Text),
            idTipoDocumento = int.Parse(ddl_tipoDNI.SelectedValue),
            nombre = txt_nombre.Text,
            apellido = txt_apellido.Text,
            fechaNacimiento = DateTime.Parse(txt_fechaNacimiento.Text)        
        });
        cargarGrillaTurista();
        limpiarDetalle();
        calcularPrecio();
    }

    protected void limpiarDetalle()
    {
        ddl_tipoDNI.SelectedIndex = 0;
        txt_dniTurista.Text = string.Empty;
        txt_nombre.Text = string.Empty;
        txt_apellido.Text = string.Empty;
        txt_fechaNacimiento.Text = string.Empty;

    }

    protected List<TuristaEntidad> ListaTurista
    {
        get
        {
            if (Session["ListaTurista"] == null)
                Session["ListaTurista"] = new List<TuristaEntidad>();
            return (List<TuristaEntidad>)Session["ListaTurista"];
        }
        set
        {
            Session["ListaTurista"] = value;
        }

    }

    protected void limpiarReserva()
    {
        ddl_estado.SelectedIndex = 0;
        txt_fechaIda.Text = string.Empty;
        txt_fechaVuelta.Text = string.Empty;
        ddl_viaje.SelectedIndex = 0;
        ddl_hotel.SelectedIndex = 0;
        txt_precioTotal.Text = string.Empty;
        dgv_detalleReserva.DataSource = null;
    }

    protected void calcularPrecio()
    {
        //me traen los precios
        HotelEntidad hotel = HotelDao.consultarHotelXID(int.Parse(ddl_hotel.SelectedValue));
        ViajeEntidad viaje = ViajeDao.consultarViajeSeleccionado(int.Parse(ddl_viaje.SelectedValue));
        //Para saber la cantidad de dias
        TimeSpan dias = DateTime.Parse(txt_fechaVuelta.Text) - DateTime.Parse(txt_fechaIda.Text);
        int cantidadDias = dias.Days;
        int cantidadTuristas = dgv_detalleReserva.Rows.Count;
        double precioViaje;
        double precioHotel = hotel.precio;
        if (viaje.soloIda.Value)
        {
            precioViaje = viaje.precioTotal / 2;
        }
        else
        {
            precioViaje = viaje.precioTotal;
        }
        switch (viaje.idTransporte)
        {
                //temporada alta
            case 1:
                precioViaje = (precioViaje * 1.5);
                precioHotel = (precioHotel * 1.5);
                break;
                //temporada media
            case 2:
                precioViaje = (precioViaje * 0.5);
                precioHotel = (precioHotel * 0.5);
                break;
                //temporada baja
            case 3:
                precioViaje = precioViaje * 1;
                precioHotel = (precioHotel * 1);
                break;
            default:
                break;
        }
        
        double precioTotal;
        //hago el calculo
        precioTotal = (cantidadTuristas * precioViaje) + ( precioHotel * cantidadDias);
        //lo cargo al precio total
        txt_precioTotal.Text =precioTotal.ToString();
        
    }
    protected void ddl_viaje_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ViajeEntidad viaje = ViajeDao.consultarViajeSeleccionado(int.Parse(ddl_viaje.SelectedValue));
        //if (viaje.soloIda.Value)
        //{
        //    txt_fechaVuelta.Enabled = false;
        //}
        //else
        //{
        //    txt_fechaVuelta.Enabled = true;
        //    ddl_hotel.Enabled = true;
        //}
    }   

    protected void cv_fechaIda_ServerValidate(object source, ServerValidateEventArgs args)
    {
        ViajeEntidad viaje = ViajeDao.consultarViajeSeleccionado(int.Parse(ddl_viaje.SelectedValue));
        if (viaje.fechaDesde <= DateTime.Parse(txt_fechaIda.Text))
        {
            args.IsValid = true;
        }
        else
        {
            
            args.IsValid = false;
            
        }

    }
    protected void cv_fechaVuelta_ServerValidate(object source, ServerValidateEventArgs args)
    {
        ViajeEntidad viaje = ViajeDao.consultarViajeSeleccionado(int.Parse(ddl_viaje.SelectedValue));
        if (viaje.fechaHasta >= DateTime.Parse(txt_fechaVuelta.Text))
        {
            args.IsValid = true;
        }
        else
        {
            
            args.IsValid = false;

        }
    }
    protected void ddl_hotel_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_tipoDNI.Enabled = true;
        txt_dniTurista.Enabled = true;
        txt_nombre.Enabled = true;
        txt_apellido.Enabled = true;
        txt_fechaNacimiento.Enabled = true;
        btn_agregar.Enabled = true;
    }
    protected void bloquearDetalle()
    {
        ddl_tipoDNI.Enabled = false;
        txt_dniTurista.Enabled = false;
        txt_nombre.Enabled = false;
        txt_apellido.Enabled = false;
        txt_fechaNacimiento.Enabled = false;
        btn_agregar.Enabled = false;
    }

    
}
   
