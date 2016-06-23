using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dao;
using Entidades;

public partial class ConsultarViaje : System.Web.UI.Page
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
            cargarGrillaViaje();
        }
    }

    protected void cargarGrillaViaje()
    {
        gv_viaje.DataSource = ViajeDao.consultarViaje();
        gv_viaje.DataKeyNames = new string[] { "idViaje" };
        gv_viaje.DataBind();

    }
    //protected void gv_viaje_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    viajeSession.Add(new ViajeEntidad(){
    //    idViaje = gv_viaje.Rows.[gv_viaje.SelectedIndex].DataItem["idViaje"],
    //    fechaDesde = gv_viaje.Rows.[gv_viaje.SelectedIndex].DataItem["fechaDesde"],
    //    fechaHasta = gv_viaje.Rows.[gv_viaje.SelectedIndex].DataItem["fechaHasta"]        
    //    });
    //    Response.Redirect("Form_Reserva.aspx");
    //}
    //protected List<ViajeEntidad> viajeSession
    //{
    //    get
    //    {
    //        if (Session["Viaje"] == null)
    //            Session["Viaje"] = new List<ViajeEntidad>();
    //        return (List<ViajeEntidad>)Session["Viaje"];
    //    }
    //    set
    //    {
    //        Session["Viaje"] = value;
    //    }

    //}
}