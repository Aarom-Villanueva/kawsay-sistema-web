using ProyKawsay_BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace ProyKawsay_ADO
{
    public class ProgramacionADO
    {
        public List<ComboBE> ListarTecnicosActivos()
        {
            using (var ctx = new BD_KawsayEntities())
            {
                return ctx.TB_TECNICO
                    .Where(t => t.Est_Tec == "A")
                    .Select(t => new ComboBE
                    {
                        Codigo = t.Cod_Tec,
                        Texto = (t.Ape_Pat_Tec + " " + t.Ape_Mat_Tec + ", " + t.Nom_Tec)
                    })
                    .OrderBy(x => x.Texto)
                    .ToList();
            }
        }

        public List<ComboBE> ListarClientesActivos()
        {
            using (var ctx = new BD_KawsayEntities())
            {
                return ctx.TB_CLIENTE
                    .Where(c => c.Est_Cli == "A")
                    .Select(c => new ComboBE
                    {
                        Codigo = c.Cod_Cli,
                        Texto = (c.Ape_Pat_Cli + " " + c.Ape_Mat_Cli + ", " + c.Nom_Cli)
                    })
                    .OrderBy(x => x.Texto)
                    .ToList();
            }
        }

        private string GenerarCodigo(BD_KawsayEntities ctx)
        {
            // PR0001, PR0002...
            string ultimo = ctx.TB_PROGRAMACION
                .OrderByDescending(x => x.Cod_Prog)
                .Select(x => x.Cod_Prog)
                .FirstOrDefault();

            int n = (ultimo == null) ? 1 : int.Parse(ultimo.Substring(2)) + 1;
            return "PR" + n.ToString("0000");
        }

        public string Registrar(ProgramacionBE cab, List<ProgramacionDetalleBE> detalles)
        {
            using (var ctx = new BD_KawsayEntities())
            using (var tx = ctx.Database.BeginTransaction())
            {
                try
                {
                    string cod = GenerarCodigo(ctx);

                    var prog = new TB_PROGRAMACION
                    {
                        Cod_Prog = cod,
                        Cod_Tec = cab.CodTec,
                        Fec_Ini = cab.FecIni.Date,
                        Fec_Fin = cab.FecFin.Date,
                        Obs_Prog = cab.ObsProg,
                        Usu_Registro = cab.UsuRegistro,
                        Est_Prog = "A"
                    };
                    ctx.TB_PROGRAMACION.Add(prog);
                    ctx.SaveChanges();

                    foreach (var d in detalles)
                    {
                        ctx.TB_PROGRAMACION_DETALLES.Add(new TB_PROGRAMACION_DETALLES
                        {
                            Cod_Prog = cod,
                            Cod_Cli = d.CodCli,
                            Fec_Visita = d.FecVisita,
                            Tip_Serv = d.TipServ.ToString(),
                            Obs_Det = d.ObsDet
                        });
                    }

                    ctx.SaveChanges();
                    tx.Commit();
                    return cod;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }
    }
}