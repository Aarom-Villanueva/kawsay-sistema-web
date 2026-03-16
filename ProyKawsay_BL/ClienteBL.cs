using ProyKawsay_ADO;
using ProyKawsay_BE;
using System;
using System.Collections.Generic;

namespace ProyKawsay_BL
{
    public class ClienteBL
    {
        // Instancia de la Capa de Acceso a Datos (ADO)
        private readonly ClienteADO dao = new ClienteADO();

        // 1. MÉTODO FALTANTE: Obtener el Cliente por Código
        public ClienteBE ObtenerPorCodigo(string codigoCliente)
        {
            // Delega la obtención de la entidad al ADO
            return dao.ObtenerClientePorCodigo(codigoCliente);
        }

        // 2. MÉTODO FALTANTE: Obtener la Ubicación por Código
        public string ObtenerNombreUbicacion(string codUbi)
        {
            // Delega la decodificación del código de ubicación al ADO
            return dao.ObtenerNombreUbicacion(codUbi);
        }

        // 3. Método para Obtener el Historial (Ya confirmado)
        public List<ServicioBE> ObtenerHistorial(string codigoCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            return dao.ObtenerHistorialServicios(codigoCliente, fechaInicio, fechaFin);
        }

        // 4. Método para Obtener los Totales (Ya confirmado)
        public (int TotalInstalaciones, int TotalMantenimientos) ObtenerTotalesServicios(string codigoCliente)
        {
            return dao.ObtenerTotalesServicios(codigoCliente);
        }
    }
}