using ProyKawsay_ADO;
using ProyKawsay_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BL
{
    public class TecnicoBL
    {
        private readonly TecnicoADO _ado = new TecnicoADO();

        public TecnicoCabeceraBE ObtenerCabecera(string codTec)
            => _ado.ObtenerTecnicoCabecera(codTec);

        public List<ServicioTecnicoDetalleBE> ObtenerHistorial(string codTec, DateTime fecIni, DateTime fecFin)
            => _ado.ListarHistorialPorTecnico(codTec, fecIni, fecFin);
    }
}
