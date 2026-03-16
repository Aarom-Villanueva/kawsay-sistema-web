using ProyKawsay_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioWeb_KawsayGUI.Reportes
{
    public partial class Reportes : System.Web.UI.Page
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

            string codVeh = txtCodigoVehiculo.Text.Trim();
            DateTime fecIni, fecFin;
            bool hayError = false;

            // Validación Código Vehículo
            if (string.IsNullOrEmpty(codVeh))
            {
                lblErrorCodigo.Text = "Debe ingresar el código del vehículo.";
                lblErrorCodigo.Visible = true;
                hayError = true;
            }
            else if (codVeh.Length != 6)
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

            // Ajuste de fecha fin
            fecFin = fecFin.Date.AddDays(1).AddSeconds(-1);

            if (fecIni > fecFin)
            {
                lblErrorFechaFin.Text = "La fecha de inicio no puede ser mayor a la fecha de fin.";
                lblErrorFechaFin.Visible = true;
                return;
            }

            var bl = new ReporteViajeBL();

            // Cabecera
            var cab = bl.ObtenerCabecera(codVeh);
            if (cab == null)
            {
                lblErrorCodigo.Text = $"El código de vehículo {codVeh} no fue encontrado.";
                lblErrorCodigo.Visible = true;
                return;
            }

            pnlInfo.Visible = true;

            lblPlaca.Text = cab.Placa;
            lblMarca.Text = cab.Marca;
            lblModelo.Text = cab.Modelo;
            lblNSerie.Text = cab.NumeroSerie;
            lblEstado.Text = cab.EstadoVehiculo;

            lblTecnico.Text = cab.Tecnico;
            lblDNI.Text = cab.DNI;
            lblLicencia.Text = cab.Licencia;

            // Detalle
            var lista = bl.ObtenerHistorial(codVeh, fecIni, fecFin);
            gvHistorial.DataSource = lista;
            gvHistorial.DataBind();
        }
    }
}