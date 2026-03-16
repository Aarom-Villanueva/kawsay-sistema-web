using ProyKawsay_BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioWeb_KawsayGUI.Consultas
{
    public partial class Tecnico : System.Web.UI.Page
    {

        private void LimpiarErrores()
        {
            lblErrorCodigo.Visible = false;
            lblErrorFechaInicio.Visible = false;
            lblErrorFechaFin.Visible = false;

            lblErrorCodigo.Text = "";
            lblErrorFechaInicio.Text = "";
            lblErrorFechaFin.Text = "";

            pnlInfo.Visible = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            LimpiarErrores();

            string codTec = txtCodigoTecnico.Text.Trim();
            DateTime fecIni, fecFin;
            bool hayError = false;

            // Validación Código
            if (string.IsNullOrEmpty(codTec))
            {
                lblErrorCodigo.Text = "Debe ingresar el código del técnico.";
                lblErrorCodigo.Visible = true;
                hayError = true;
            }
            else if (codTec.Length != 6)
            {
                lblErrorCodigo.Text = "El código debe tener 6 caracteres.";
                lblErrorCodigo.Visible = true;
                hayError = true;
            }

            // Validación Fecha Inicio
            if (!DateTime.TryParse(txtFechaInicio.Text, out fecIni))
            {
                lblErrorFechaInicio.Text = "Fecha de inicio no válida.";
                lblErrorFechaInicio.Visible = true;
                hayError = true;
            }

            // Validación Fecha Fin
            if (!DateTime.TryParse(txtFechaFin.Text, out fecFin))
            {
                lblErrorFechaFin.Text = "Fecha de fin no válida.";
                lblErrorFechaFin.Visible = true;
                hayError = true;
            }

            if (hayError) return;

            fecFin = fecFin.Date.AddDays(1).AddSeconds(-1);

            if (fecIni > fecFin)
            {
                lblErrorFechaFin.Text = "La fecha de inicio no puede ser mayor a la fecha de fin.";
                lblErrorFechaFin.Visible = true;
                return;
            }

            var bl = new TecnicoBL();
            var tec = bl.ObtenerCabecera(codTec);

            if (tec == null)
            {
                lblErrorCodigo.Text = $"El código de técnico {codTec} no fue encontrado.";
                lblErrorCodigo.Visible = true;
                return;
            }

            // Cargar cabecera
            pnlInfo.Visible = true;
            lblNombres.Text = tec.NombreCompleto;
            lblDNI.Text = tec.Dni;
            lblCelular.Text = tec.Celular;
            lblLicencia.Text = tec.Licencia;
            lblEspecialidad.Text = tec.Especialidad;
            lblUbicacion.Text = tec.Ubicacion;
            lblDireccion.Text = tec.Direccion;
            lblEstado.Text = tec.Estado;

            // Historial
            var lista = bl.ObtenerHistorial(codTec, fecIni, fecFin);
            gvHistorial.DataSource = lista;
            gvHistorial.DataBind();

            // Totales
            lblInstalaciones.Text = lista.Count(x => x.Tipo == "Instalación").ToString();
            lblMantenimientos.Text = lista.Count(x => x.Tipo == "Mantenimiento").ToString();
        }
    }
}