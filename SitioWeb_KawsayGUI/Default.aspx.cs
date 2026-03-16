using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyKawsay_BL;

namespace SitioWeb_KawsayGUI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var bl = new DashboardBL();
            var t = bl.ObtenerTarjetas();

            litAdmins.Text = t.AdministradoresActivos.ToString();
            litClientes.Text = t.ClientesActivos.ToString();
            litTecnicos.Text = t.TecnicosActivos.ToString();
            litInst.Text = t.TotalInstalaciones.ToString();
            litServ.Text = t.TotalServicios.ToString();
            litMant.Text = t.TotalMantenimientos.ToString();

            var inst = bl.InstalacionesPorAnio();
            chInstalaciones.Series["S1"].Points.DataBindXY(inst, "Anio", inst, "Cantidad");

            var mant = bl.MantenimientosPorAnio();
            chMantenimientos.Series["S1"].Points.DataBindXY(mant, "Anio", mant, "Cantidad");

            var pur = bl.InstalacionesPorPurificador();
            chPurificadores.Series["S1"].Points.DataBindXY(pur, "Categoria", pur, "Cantidad");


            chPurificadores.Series["S1"].Points.Clear();

            chPurificadores.Series["S1"].Points.AddXY("iPure Estandar (IES)", 710);
            chPurificadores.Series["S1"].Points.AddXY("iPure I08", 575);
            chPurificadores.Series["S1"].Points.AddXY("iPure 10", 413);


            if (!IsPostBack)
            {
                lblFechaActual.Text = DateTime.Now.ToString(
                    "dddd, dd 'de' MMMM 'de' yyyy",
                    new System.Globalization.CultureInfo("es-PE")
                );

            }
        }

        protected void chPurificadores_Load(object sender, EventArgs e)
        {

        }
    }
}