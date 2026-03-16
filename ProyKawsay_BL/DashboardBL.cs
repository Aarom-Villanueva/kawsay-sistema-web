using ProyKawsay_ADO;
using ProyKawsay_BE;
using System.Collections.Generic;

namespace ProyKawsay_BL
{
    public class DashboardBL
    {
        private readonly DashboardADO dao = new DashboardADO();

        public DashboardBE ObtenerTarjetas() => dao.ObtenerTarjetas();
        public List<AnioCantidadBE> InstalacionesPorAnio() => dao.InstalacionesPorAnio();
        public List<AnioCantidadBE> MantenimientosPorAnio() => dao.MantenimientosPorAnio();
        public List<CategoriaCantidadBE> InstalacionesPorPurificador() => dao.InstalacionesPorPurificador();
    }
}
