using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Dao;


public partial class Form_Viaje : System.Web.UI.Page
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
            cargarComboTransporte();
            cargarComboTemporada();
            btn_eliminar.Enabled = false;
        }

    }

    protected void btn_guardar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                ViajeEntidad viaje = new ViajeEntidad();
                viaje.nombreViaje = txt_origen.Text + " - " + txt_destino.Text;
                viaje.idCiudadDestino = CiudadDestinoDao.nombreCiudad(txt_destino.Text);
                viaje.idCiudadOrigen = CiudadOrigenDao.nombreCiudad(txt_origen.Text);
                viaje.fechaDesde = DateTime.Parse(txt_fechaDesde.Text);
                viaje.fechaHasta = DateTime.Parse(txt_fechaHasta.Text);
                viaje.soloIda = ckb_soloIda.Checked;
                viaje.precioTotal = float.Parse(txt_precio.Text);

                viaje.idTransporte = int.Parse(ddl_transporte.SelectedValue);

                viaje.idTemporada = int.Parse(ddl_temporada.SelectedValue);

                //guardo en DB
                ViajeDao.registrarViaje(viaje);
                //limpio el form
                limpiar();
            }
        }
        catch (Exception)
        {
            
        }
    }
        

    //CARGA EL COMBO TRANSPORTE
    public void cargarComboTransporte()
    {
        ddl_transporte.DataSource = Dao.TransporteDao.consultarTransporte();
        ddl_transporte.DataValueField = "idTransporte";
        ddl_transporte.DataTextField = "nombreTransporte";
        ddl_transporte.DataBind();
        ddl_transporte.Items.Insert(0, new ListItem("(Transporte)", "0"));
        ddl_transporte.SelectedIndex = 0;
    }
    //CARGA EL COMBO TEMPORADA
    public void cargarComboTemporada()
    {
        ddl_temporada.DataSource = Dao.TemporadaDao.consultarTemporada();
        ddl_temporada.DataValueField = "idTemporada";
        ddl_temporada.DataTextField = "nombre";
        ddl_temporada.DataBind();
        ddl_temporada.Items.Insert(0, new ListItem("(Temporada)", "0"));
        ddl_temporada.SelectedIndex = 0;
    }

    //ELIMINAR VIAJE
    protected void btn_elimnar_Click(object sender, EventArgs e)
    {
        int? id = int.Parse(dgv_viajes.SelectedDataKey.Value.ToString());
        if (id != null)
        {
            ViajeDao.eliminarViaje(id.Value);
            cargarGrilla();
            limpiar();
        }
    }

    //CARGAR GRILLA VIAJES
    protected void cargarGrilla()
    {
        dgv_viajes.DataSource = ViajeDao.consultarViaje();
        dgv_viajes.DataKeyNames = new string[] { "idViaje" };
        dgv_viajes.DataBind();
    }
    //CONSULTAR VIAJES - CARGA LA GRILLA
    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        dgv_viajes.Visible = true;
        cargarGrilla();

    }

    //LIMPIA EL FORM
    protected void limpiar()
    {
        txt_origen.Text = string.Empty;
        txt_destino.Text = string.Empty;
        txt_fechaDesde.Text = string.Empty;
        txt_fechaHasta.Text = string.Empty;
        txt_precio.Text = string.Empty; ;

    }

    //CUANDO SELECCIONO UN VIAJE DE LA GRILLA ME LO CARGA EN EL FORM
    protected void dgv_viajes_SelectedIndexChanged(object sender, EventArgs e)
    {
        limpiar();
        btn_modificar.Visible = true;


        ViajeEntidad viaje = ViajeDao.consultarViajeXID(int.Parse(dgv_viajes.SelectedDataKey.Value.ToString()));
        txt_origen.Text = viaje.origen;
        txt_destino.Text = viaje.destino;
        viaje.nombreViaje = txt_origen.Text + " - " + txt_destino.Text;
        txt_fechaDesde.Text = viaje.fechaDesde.ToShortDateString();
        txt_fechaHasta.Text = viaje.fechaHasta.ToShortDateString();
        txt_precio.Text = viaje.precioTotal.ToString();
        ddl_temporada.SelectedValue = viaje.idTemporada.ToString();
        ddl_transporte.SelectedValue = viaje.idTransporte.ToString();
        ckb_soloIda.Checked = viaje.soloIda.Value;

        btn_eliminar.Enabled = true;

    }
    // NUEVO _ ME LIMPIA LA GRILLA
    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        limpiar();
        dgv_viajes.Visible = false;
    }
    //EN BASE AL VIAJE CARGADO EN EL FORM ME LO MODIFICA
    protected void bnt_modificar_Click(object sender, EventArgs e)
    {
        ViajeEntidad viaje = new ViajeEntidad();
        viaje.idViaje = int.Parse(dgv_viajes.SelectedDataKey.Value.ToString());
        viaje.idCiudadDestino = CiudadDestinoDao.nombreCiudad(txt_destino.Text);
        viaje.idCiudadOrigen = CiudadOrigenDao.nombreCiudad(txt_origen.Text);
        viaje.fechaDesde = DateTime.Parse(txt_fechaDesde.Text);
        viaje.fechaHasta = DateTime.Parse(txt_fechaHasta.Text);
        viaje.soloIda = ckb_soloIda.Checked;
        viaje.precioTotal = float.Parse(txt_precio.Text);
        viaje.idTransporte = int.Parse(ddl_transporte.SelectedValue);
        viaje.idTemporada = int.Parse(ddl_temporada.SelectedValue);

        ViajeDao.actualizarViaje(viaje);

        limpiar();
        cargarGrilla();


    }
    
    protected void cv_transporte_ServerValidate1(object source, ServerValidateEventArgs args)
    {
        if (int.Parse(ddl_transporte.SelectedValue) <= 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    protected void cv_temporada_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (int.Parse(ddl_temporada.SelectedValue) <= 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    protected void cv_origen_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (CiudadDestinoDao.nombreCiudad(txt_origen.Text) == 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    protected void cv_destino_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (CiudadDestinoDao.nombreCiudad(txt_destino.Text) == 0)
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    protected void cv_origenDestino_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (CiudadDestinoDao.nombreCiudad(txt_destino.Text) == CiudadDestinoDao.nombreCiudad(txt_origen.Text))
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
}