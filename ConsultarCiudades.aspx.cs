using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dao;

public partial class Ciudades : System.Web.UI.Page
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
        if (!Page.IsPostBack)
        {
            cargarGrillaCiudades();
        }
    }

    protected void cargarGrillaCiudades()
    {
        gv_ciudades.DataSource = CiudadDestinoDao.consultarCiudades();
        gv_ciudades.DataBind();
    }
}