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
        ddl_estado.Items.Insert(0, new ListItem("(Estado)", "0"));
        ddl_estado.SelectedIndex = 0;
    }

    protected void cargarComboViaje()
    {
        ddl_viaje.DataSource = ViajeDao.consultarViaje();
        ddl_viaje.DataTextField = "idViaje";
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
        

    }

    protected void cargarGrillaTurista()
    {
        dgv_detalleReserva.DataSource = TuristaDao.consultarTurista();
        dgv_detalleReserva.DataBind();
    }
    protected void btn_agregar_Click(object sender, EventArgs e)
    {
        TuristaEntidad turista = new TuristaEntidad();
        turista.DNI = int.Parse(txt_dniTurista.Text);
        turista.idTipoDocumento = int.Parse(ddl_tipoDNI.SelectedValue);
        turista.nombre = txt_nombre.Text;
        turista.apellido = txt_apellido.Text;
        turista.fechaNacimiento = DateTime.Parse(txt_fechaNacimiento.Text);
        TuristaDao.registrarTuristas(turista);
        limpiarDetalle();
        cargarGrillaTurista();
    }

    protected void limpiarDetalle()
    {
        ddl_tipoDNI.SelectedIndex = 0;
        txt_dniTurista.Text = string.Empty;
        txt_nombre.Text = string.Empty;
        txt_apellido.Text = string.Empty;
        txt_fechaNacimiento.Text = string.Empty;

    }
}