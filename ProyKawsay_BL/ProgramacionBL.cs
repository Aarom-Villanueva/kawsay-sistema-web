using ProyKawsay_ADO;
using ProyKawsay_BE;
using System.Collections.Generic;
using System.Data;

namespace ProyKawsay_BL
{
    public class ProgramacionBL
    {
        private readonly ProgramacionADO dao = new ProgramacionADO();

        public List<ComboBE> ListarTecnicosActivos() => dao.ListarTecnicosActivos();
        public List<ComboBE> ListarClientesActivos() => dao.ListarClientesActivos();
        public string Registrar(ProgramacionBE cab, List<ProgramacionDetalleBE> det) => dao.Registrar(cab, det);
    }
}