using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyKawsay_BL;
using ProyKawsay_BE;

namespace SitioWeb_KawsayGUI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var bl = new UsuarioBL();
                var user = bl.Autenticar(txtUser.Text.Trim(), txtPass.Text.Trim());

                if (user == null || user.EstUsuario != 1)
                    throw new Exception("Usuario o contraseña incorrectos.");

                Session["Usuario"] = user.LoginUsuario;
                Session["Nivel"] = user.NivelUsuario;

                Response.Redirect("Default.aspx");
            }
            catch (Exception ex)
            {
                litMsg.Text = $"<div class='alert alert-danger'>{ex.Message}</div>";
            }
        }
    }
}