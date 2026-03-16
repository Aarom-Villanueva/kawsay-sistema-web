using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BE
{
    public class ServicioTecnicoDetalleBE
    {
        public string Codigo { get; set; }      // I00001 / M00001
        public string Tipo { get; set; }        // Instalación / Mantenimiento
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public string Ubicacion { get; set; }   // Ubicación del cliente (Dep-Prov-Dis)
        public string Detalle { get; set; } // Purificador o Incidencia 
    }
}
