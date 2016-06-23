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
        activarBoton();
        if (!IsPostBack)
        {
            cargarComboTransporte();
        }
    }

    protected void activarBoton()
    {
        if (chk_activarTransporte.Checked || chk_activarDestino.Checked || chk_activarFecha.Checked)
        {
            btn_consultar.Enabled = true;
        }
        else
            btn_consultar.Enabled = false;
    }
    protected void btn_consultar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            cargarGrilla();
        }
    }

    protected void cargarComboTransporte()
    {
        ddl_transporte.DataSource = TransporteDao.consultarTransporte();
        ddl_transporte.DataTextField = "nombreTransporte";
        ddl_transporte.DataValueField = "idTransporte";
        ddl_transporte.DataBind();
        ddl_transporte.Items.Insert(0, new ListItem("(Transporte)", "0"));
        ddl_transporte.SelectedIndex = 0;
    }

    protected void cargarGrilla()
    {
        DateTime? fecha = null;
        int transporte = 0;
        int destino = 0;
        if (chk_activarFecha.Checked)
        {
            if (txt_fecha.Text != "")
            {
                fecha = DateTime.Parse(txt_fecha.Text);

            }
        }
        if (chk_activarTransporte.Checked)
        {
            transporte = int.Parse(ddl_transporte.SelectedValue);
        }
        if (chk_activarDestino.Checked)
        {
            destino = CiudadDestinoDao.nombreCiudad(txt_destino.Text);
        }
        gv_reporte.DataSource = ViajeDao.reporteViaje(transporte, fecha, destino);
        gv_reporte.DataBind();
    }

    protected void gv_reporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_reporte.PageIndex = e.NewPageIndex;
        cargarGrilla();
    }
    protected void chk_activarTransporte_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_activarTransporte.Checked)
        {
            ddl_transporte.Enabled = true;
        }
        else
            ddl_transporte.Enabled = false;

    }
    protected void chk_activarDestino_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_activarDestino.Checked)
        {
            txt_destino.Enabled = true;
        }
        else
            txt_destino.Enabled = false;
    }
    protected void chk_activarFecha_CheckedChanged(object sender, EventArgs e)
    {
        if (chk_activarFecha.Checked)
        {
            txt_fecha.Enabled = true;
        }
        else
            txt_fecha.Enabled = false;

    }
    protected void cv_transporte_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (int.Parse(ddl_transporte.SelectedValue) == 0)
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
}