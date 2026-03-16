using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BE
{
    public class ReporteViajeDetalleBE
    {
        public string Codigo { get; set; }
        public DateTime Fecha { get; set; }
        public string Incidencia { get; set; }
        public string EstadoViaje { get; set; } // Pendiente / Acabado
    }
}
