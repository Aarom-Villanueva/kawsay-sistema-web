using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioWeb_KawsayGUI
{
    public partial class MasterModelo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }

            lblUsuario.Text = "Usuario : " + Session["Usuario"].ToString();

            if (!IsPostBack)
            {
                // Obtiene la ruta de la página actual (ej: "/Default.aspx")
                string currentPage = Request.Url.AbsolutePath;
            }
        }
    }
}