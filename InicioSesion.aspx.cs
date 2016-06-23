using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dao;
using Entidades;

public partial class InicioSesion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btn_login_Click(object sender, EventArgs e)
    {
        if (ValidarUsuario(txt_usuario.Text, txt_clave.Text))
        {
            Session["Usuario"] = txt_usuario.Text;
            Response.Redirect("Default.aspx");
        }
        else
            Session["Usuario"] = string.Empty;
    }

    private bool ValidarUsuario(string usuario, string clave)
    {
        //LLamar al DAO
        

        if (UsuarioDao.consultarUser(txt_usuario.Text,txt_clave.Text))
        {
            List<string> roles = new List<string>();
            roles.Add("Administrador");
            roles.Add("Gerente");
            Session["Roles"] = roles;
            return true;
        }
        else
            return false;
    }
}