using ProyKawsay_BE;
using ProyKawsay_BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioWeb_KawsayGUI.Consultas
{
    public partial class Cliente : System.Web.UI.Page
    {
        private ClienteBL _clienteBL = new ClienteBL();

        // Elimina el método private void MostrarMensaje(string mensaje)

        // Nuevo método para limpiar mensajes de error
        private void LimpiarErrores()
        {
            lblErrorCodigo.Visible = false;
            lblErrorFechaInicio.Visible = false;
            lblErrorFechaFin.Visible = false;
            lblErrorCodigo.Text = "";
            lblErrorFechaInicio.Text = "";
            lblErrorFechaFin.Text = "";
            pnlInfo.Visible = false; // Ocultar resultados hasta que haya éxito
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            LimpiarErrores(); // Limpiar errores antes de una nueva validación

            string codigoCliente = txtCodigoCliente.Text.Trim();
            DateTime fechaInicio, fechaFin;
            bool hayError = false; // Flag para rastrear si encontramos un error

            // ===================================
            // 1. Validaciones
            // ===================================

            // Validación de Código (Vacío)
            if (string.IsNullOrEmpty(codigoCliente))
            {
                lblErrorCodigo.Text = "Debe ingresar el código del cliente.";
                lblErrorCodigo.Visible = true;
                hayError = true;
            }
            // Validación de Código (Longitud) - Solo si no está vacío
            else if (codigoCliente.Length != 6)
            {
                lblErrorCodigo.Text = "El código debe tener 6 caracteres.";
                lblErrorCodigo.Visible = true;
                hayError = true;
            }

            // Validación de Fecha de Inicio
            if (!DateTime.TryParse(txtFechaInicio.Text, out fechaInicio))
            {
                lblErrorFechaInicio.Text = "Fecha de inicio no válida.";
                lblErrorFechaInicio.Visible = true;
                hayError = true;
            }

            // Validación de Fecha de Fin
            if (!DateTime.TryParse(txtFechaFin.Text, out fechaFin))
            {
                lblErrorFechaFin.Text = "Fecha de fin no válida.";
                lblErrorFechaFin.Visible = true;
                hayError = true;
            }

            // Si ya hay errores de formato, salimos antes de la comparación de fechas
            if (hayError)
            {
                return;
            }

            // 2. Ajustar la fecha de fin (si la validación pasó)
            fechaFin = fechaFin.Date.AddDays(1).AddSeconds(-1);

            // Validación de Rango de Fechas
            if (fechaInicio > fechaFin)
            {
                lblErrorFechaFin.Text = "La fecha de inicio no puede ser mayor a la fecha de fin.";
                lblErrorFechaFin.Visible = true;
                return;
            }

            // ===================================
            // 3. Ejecución de Consulta (Si no hay errores de validación)
            // ===================================

            // Cargar Datos del Cliente
            var cliente = CargarDatosCliente(codigoCliente);
            if (cliente == null)
            {
                // Este mensaje de "No encontrado" no puede ir en un validador de campo, así que lo dejamos en una alerta simple, o en un Label principal si tienes uno.
                // Como quieres evitar alerts, usaremos la Label del Código
                lblErrorCodigo.Text = $"El código de cliente {codigoCliente} no fue encontrado.";
                lblErrorCodigo.Visible = true;
                pnlInfo.Visible = false;
                return;
            }

            // Cargar Historial
            CargarHistorial(codigoCliente, fechaInicio, fechaFin);

            pnlInfo.Visible = true;
        }
        private ClienteBE CargarDatosCliente(string codigoCliente)
        {
            // Llama a la capa BL para obtener los datos del cliente y totales
            var cliente = _clienteBL.ObtenerPorCodigo(codigoCliente);

            if (cliente != null)
            {
                // Asignación de datos del Cliente (Nombres y Apellidos, DNI, etc.)
                lblNombres.Text = cliente.NombresCompletos;
                lblDNI.Text = cliente.Dni_Cli;
                lblCelular.Text = cliente.Tel_Cli;

                // Obtener Ubicación (Debe llamar a una función que decodifique Cod_Ubi, que está en BL)
                lblUbicacion.Text = _clienteBL.ObtenerNombreUbicacion(cliente.Cod_Ubi);

                lblDireccion.Text = cliente.Dir_Cli;
                lblEstado.Text = cliente.Est_Cli == "A" ? "Activo" : "Inactivo";

                // Obtener Totales de Instalación y Mantenimiento
                var totales = _clienteBL.ObtenerTotalesServicios(cliente.Cod_Cli);
                lblInstalacion.Text = totales.TotalInstalaciones.ToString();
                lblMantenimiento.Text = totales.TotalMantenimientos.ToString();

                return cliente;
            }
            return null;
        }

        private void CargarHistorial(string codigoCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            // Llama a la capa BL para obtener el historial unificado (List<ServicioBE>)
            List<ServicioBE> historial = _clienteBL.ObtenerHistorial(codigoCliente, fechaInicio, fechaFin);

            // Reemplaza la lógica de DataTable y enlaza con la lista de BE
            if (historial != null && historial.Any())
            {
                gvHistorial.DataSource = historial;
            }
            else
            {
                // Muestra EmptyDataText
                gvHistorial.DataSource = new List<ServicioBE>();
            }

            gvHistorial.DataBind();
        }

        // Método auxiliar para mostrar alertas
        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                $"alert('{mensaje.Replace("'", "\\'")}')", true);
        }
    }
}