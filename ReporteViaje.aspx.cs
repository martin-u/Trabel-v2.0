using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dao;
using Entidades;

public partial class ReporteViaje : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            cargarComboTransporte();
        }
    }
    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        cargarGrilla();
    }

    protected void cargarComboTransporte()
    {
        ddl_transporte.DataSource = TransporteDao.consultarTransporte();
        ddl_transporte.DataTextField = "nombre";
        ddl_transporte.DataValueField = "idTransporte";
        ddl_transporte.DataBind();
        ddl_transporte.Items.Insert(0,new ListItem ("(Transporte)","0"));
        ddl_transporte.SelectedIndex = 0; 
    }

    protected void cargarGrilla()
    {
        int destino = CiudadDestinoDao.nombreCiudad(txt_destino.Text);
        int transporte = int.Parse(ddl_transporte.SelectedValue);
        bool ida = ckb_soloIda.Checked;
        DateTime fecha = DateTime.Parse(txt_fecha.Text);

        gv_reporte.DataSource = ViajeDao.reporteViaje(transporte, ida, fecha, destino);
        gv_reporte.DataBind();
    }
}