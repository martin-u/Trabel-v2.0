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
        if (!IsPostBack)
        {
            cargarComboTransporte();
            cargarComboTemporada();
            btn_eliminar.Enabled = false;
        }

    }


    protected void btn_guardar_Click(object sender, EventArgs e)
    {

        ViajeEntidad viaje = new ViajeEntidad();

        viaje.idCiudadDestino = CiudadDestinoDao.nombreCiudad(txt_destino.Text);
        viaje.idCiudadOrigen = CiudadOrigenDao.nombreCiudad(txt_origen.Text);
        viaje.fechaDesde = DateTime.Parse(txt_fechaDesde.Text);
        viaje.fechaHasta = DateTime.Parse(txt_fechaHasta.Text);
        viaje.soloIda = ckb_soloIda.Checked;
        viaje.precioTotal = float.Parse(txt_precio.Text);
        viaje.idTransporte = int.Parse(ddl_transporte.SelectedValue);
        viaje.idTemporada = int.Parse(ddl_temporada.SelectedValue);

        //if (idViaje.HasValue)
        //{
        //    viaje.idViaje = idViaje.Value;
        //    ViajeDao.actualizarViaje(viaje);
        //}
        //else
        ViajeDao.registrarViaje(viaje);
        //idViaje = viaje.idViaje.Value;
        limpiar();
        cargarGrilla();

    }

    //protected int? idViaje
    //{
    //    get
    //    {
    //        if (ViewState["idViaje"] != null)
    //            return (int)ViewState["idViaje"];
    //        else return null;
    //    }
    //    set { ViewState["idViaje"] = value; }
    //}


    public void cargarComboTransporte()
    {
        ddl_transporte.DataSource = Dao.TransporteDao.consultarTransporte();
        ddl_transporte.DataValueField = "idTransporte";
        ddl_transporte.DataTextField = "nombre";
        ddl_transporte.DataBind();
    }

    public void cargarComboTemporada()
    {
        ddl_temporada.DataSource = Dao.TemporadaDao.consultarTemporada();
        ddl_temporada.DataValueField = "idTemporada";
        ddl_temporada.DataTextField = "nombre";
        ddl_temporada.DataBind();
    }

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

    protected void cargarGrilla()
    {
        dgv_viajes.DataSource = ViajeDao.consultarViaje();
        dgv_viajes.DataKeyNames = new string[] { "idViaje" };
        dgv_viajes.DataBind();
    }

    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        dgv_viajes.Visible = true;
        cargarGrilla();

    }


    protected void limpiar()
    {
        txt_origen.Text = string.Empty;
        txt_destino.Text = string.Empty;
        txt_fechaDesde.Text = string.Empty;
        txt_fechaHasta.Text = string.Empty;
        txt_precio.Text = string.Empty; ;

    }

    protected void dgv_viajes_SelectedIndexChanged(object sender, EventArgs e)
    {
        limpiar();
        btn_modificar.Visible = true;


        ViajeEntidad viaje = ViajeDao.consultarViajeXID(int.Parse(dgv_viajes.SelectedDataKey.Value.ToString()));
        txt_origen.Text = viaje.origen;
        txt_destino.Text = viaje.destino;
        txt_fechaDesde.Text = viaje.fechaDesde.ToShortDateString();
        txt_fechaHasta.Text = viaje.fechaHasta.ToShortDateString();
        txt_precio.Text = viaje.precioTotal.ToString();
        ddl_temporada.SelectedValue = viaje.idTemporada.ToString();
        ddl_transporte.SelectedValue = viaje.idTransporte.ToString();
        ckb_soloIda.Checked = viaje.soloIda.Value;

        btn_eliminar.Enabled = true;

    }

    protected void btn_nuevo_Click(object sender, EventArgs e)
    {
        limpiar();
        dgv_viajes.Visible = false;
    }

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
}