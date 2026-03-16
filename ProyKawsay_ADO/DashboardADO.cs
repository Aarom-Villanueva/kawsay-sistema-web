using ProyKawsay_BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyKawsay_ADO
{
    public class DashboardADO
    {
        public DashboardBE ObtenerTarjetas()
        {
            using (var db = new BD_KawsayEntities())
            {
                int admins = db.TB_ADMINISTRADOR.Count(x => x.Est_Adm == "A");
                int clientes = db.TB_CLIENTE.Count(x => x.Est_Cli == "A");
                int tecnicos = db.TB_TECNICO.Count(x => x.Est_Tec == "A");

                int inst = db.TB_INSTALACION.Count(x => x.Est_Ins == "A");
                int mant = db.TB_MANTENIMIENTO.Count(x => x.Est_Man == "A");

                return new DashboardBE
                {
                    AdministradoresActivos = admins,
                    ClientesActivos = clientes,
                    TecnicosActivos = tecnicos,
                    TotalInstalaciones = inst,
                    TotalMantenimientos = mant,
                    TotalServicios = inst + mant
                };
            }
        }

        public List<AnioCantidadBE> InstalacionesPorAnio()
        {
            using (var db = new BD_KawsayEntities())
            {
                return db.TB_INSTALACION
                    .Where(x => x.Est_Ins == "A")
                    .GroupBy(x => x.Fec_Ins.Year)
                    .Select(g => new AnioCantidadBE { Anio = g.Key, Cantidad = g.Count() })
                    .OrderBy(x => x.Anio)
                    .ToList();
            }
        }

        public List<AnioCantidadBE> MantenimientosPorAnio()
        {
            using (var db = new BD_KawsayEntities())
            {
                return db.TB_MANTENIMIENTO
                    .Where(x => x.Est_Man == "A")
                    .GroupBy(x => x.Fec_Man.Year)
                    .Select(g => new AnioCantidadBE { Anio = g.Key, Cantidad = g.Count() })
                    .OrderBy(x => x.Anio)
                    .ToList();
            }
        }

        // PIE: por tipo de purificador (Tip_Pur), porque NO existe Des_Pur
        public List<CategoriaCantidadBE> InstalacionesPorPurificador()
        {
            using (var db = new BD_KawsayEntities())
            {
                return (from i in db.TB_INSTALACION
                        join p in db.TB_PURIFICADOR on i.Cod_Pur equals p.Cod_Pur
                        where i.Est_Ins == "A"
                        group i by p.Tip_Pur into g
                        select new CategoriaCantidadBE
                        {
                            Categoria = g.Key,
                            Cantidad = g.Count()
                        })
                        .OrderByDescending(x => x.Cantidad)
                        .ToList();
            }
        }
    }
}
