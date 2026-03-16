using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyKawsay_ADO;

namespace ProyKawsay_BE
{
    public class ReporteViajeBL
    {
        private readonly ReporteViajeADO _ado = new ReporteViajeADO();

        public ReporteViajeCabeceraBE ObtenerCabecera(string codVeh)
            => _ado.ObtenerCabeceraReporte(codVeh);

        public List<ReporteViajeDetalleBE> ObtenerHistorial(
            string codVeh, DateTime fecIni, DateTime fecFin)
            => _ado.ListarViajesPorVehiculo(codVeh, fecIni, fecFin);
    }
}
