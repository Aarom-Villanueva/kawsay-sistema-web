using ProyKawsay_BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_ADO
{
    public class ReporteViajeADO
    {
        public ReporteViajeCabeceraBE ObtenerCabeceraReporte(string codVeh)
        {
            using (var db = new BD_KawsayEntities())
            {
                var q = (from v in db.TB_VEHICULO
                         join r in db.TB_REPORTE_VIAJE on v.Cod_Veh equals r.Cod_Veh
                         join t in db.TB_TECNICO on r.Cod_Tec equals t.Cod_Tec
                         where v.Cod_Veh == codVeh
                         select new ReporteViajeCabeceraBE
                         {
                             Placa = v.Pla_Veh,
                             Marca = v.Mar_Veh,
                             Modelo = v.Mod_Veh,
                             NumeroSerie = v.Num_Ser_Veh,
                             EstadoVehiculo = (v.Est_Veh == "A") ? "Activo" : "Inactivo",

                             Tecnico = t.Nom_Tec + " " + t.Ape_Pat_Tec + " " + t.Ape_Mat_Tec,
                             DNI = t.Dni_Tec,
                             Licencia = t.Lic_Tec
                         }).FirstOrDefault();
                return q;
            }
        }

        public List<ReporteViajeDetalleBE> ListarViajesPorVehiculo(
            string codVeh, DateTime fecIni, DateTime fecFin)
        {
            using (var db = new BD_KawsayEntities())
            {
                var q = from r in db.TB_REPORTE_VIAJE
                        where r.Cod_Veh == codVeh
                           && r.Fec_Via >= fecIni
                           && r.Fec_Via <= fecFin
                        select new ReporteViajeDetalleBE
                        {
                            Codigo = r.Cod_Via,
                            Fecha = r.Fec_Via,
                            Incidencia = r.Inc_Via ?? "Sin incidencias",
                            EstadoViaje = r.Est_Via == "A" ? "Acabado" : "Pendiente"
                        };

                return q.OrderBy(x => x.Fecha).ToList();
            }
        }

    }
}
