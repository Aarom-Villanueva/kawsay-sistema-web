using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyKawsay_BE;

namespace ProyKawsay_ADO
{
    public class TecnicoADO
    {
        public TecnicoCabeceraBE ObtenerTecnicoCabecera(string codTec)
        {
            using (var db = new BD_KawsayEntities())
            {
                var q = (from t in db.TB_TECNICO
                         join u in db.TB_UBIGEO on t.Cod_Ubi equals u.Cod_Ubi
                         where t.Cod_Tec == codTec
                         select new TecnicoCabeceraBE
                         {
                             CodTec = t.Cod_Tec,
                             NombreCompleto = t.Nom_Tec + " " + t.Ape_Pat_Tec + " " + t.Ape_Mat_Tec,
                             Dni = t.Dni_Tec,
                             Celular = t.Tel_Tec,
                             Licencia = t.Lic_Tec,
                             Direccion = t.Dir_Tec,
                             Ubicacion = u.Dep_Ubi + "-" + u.Pro_Ubi + "-" + u.Dis_Ubi,
                             Estado = (t.Est_Tec == "A") ? "Activo" : "Inactivo",
                             Especialidad = (t.Esp_Tec == "I") ? "Instalación" : "Mantenimiento"
                         }).FirstOrDefault();

                return q;
            }
        }

        public List<ServicioTecnicoDetalleBE> ListarHistorialPorTecnico(
    string codTec, DateTime fecIni, DateTime fecFin)
        {
            using (var db = new BD_KawsayEntities())
            {
                // INSTALACIONES (incluye cliente + ubigeo + purificador)
                var instalaciones = from i in db.TB_INSTALACION
                                    join c in db.TB_CLIENTE on i.Cod_Cli equals c.Cod_Cli
                                    join u in db.TB_UBIGEO on c.Cod_Ubi equals u.Cod_Ubi
                                    join p in db.TB_PURIFICADOR on i.Cod_Pur equals p.Cod_Pur
                                    where i.Cod_Tec == codTec
                                       && i.Fec_Ins >= fecIni
                                       && i.Fec_Ins <= fecFin
                                    select new ServicioTecnicoDetalleBE
                                    {
                                        Codigo = i.Cod_Ins,
                                        Tipo = "Instalación",
                                        Fecha = i.Fec_Ins,
                                        Cliente = c.Nom_Cli + " " + c.Ape_Pat_Cli + " " + c.Ape_Mat_Cli,
                                        Ubicacion = u.Dep_Ubi + "-" + u.Pro_Ubi + "-" + u.Dis_Ubi,
                                        Detalle = "Modelo de Purificador: " + p.Tip_Pur
                                    };

                // MANTENIMIENTOS (incluye cliente + ubigeo + observación)
                var mantenimientos = from m in db.TB_MANTENIMIENTO
                                     join c in db.TB_CLIENTE on m.Cod_Cli equals c.Cod_Cli
                                     join u in db.TB_UBIGEO on c.Cod_Ubi equals u.Cod_Ubi
                                     where m.Cod_Tec == codTec
                                        && m.Fec_Man >= fecIni
                                        && m.Fec_Man <= fecFin
                                     select new ServicioTecnicoDetalleBE
                                     {
                                         Codigo = m.Cod_Man,
                                         Tipo = "Mantenimiento",
                                         Fecha = m.Fec_Man,
                                         Cliente = c.Nom_Cli + " " + c.Ape_Pat_Cli + " " + c.Ape_Mat_Cli,
                                         Ubicacion = u.Dep_Ubi + "-" + u.Pro_Ubi + "-" + u.Dis_Ubi,
                                         Detalle = "Observación: " + m.Obs_Man
                                     };

                // UNION + Orden cronológico
                return instalaciones
                    .Union(mantenimientos)
                    .OrderBy(x => x.Fecha)
                    .ToList();
                }
            }
        }
    }
