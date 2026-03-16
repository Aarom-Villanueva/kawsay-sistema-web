using ProyKawsay_BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyKawsay_ADO
{
    public class ClienteADO
    {
        // ... (otros métodos) ...

        public List<ServicioBE> ObtenerHistorialServicios(string codigoCliente, DateTime fechaInicio, DateTime fechaFin)
        {
            using (var ctx = new BD_KawsayEntities())
            {
                DateTime fechaFinIncluyente = fechaFin.Date.AddDays(1).AddMilliseconds(-1);

                // ==============================
                // 1. CONSULTA DE INSTALACIONES
                // ==============================
                var instalaciones = ctx.TB_INSTALACION
                    // Asumimos que TB_PURIFICADOR y TB_TECNICO son accesibles a través de propiedades de navegación
                    // En caso contrario, usar ctx.TB_PURIFICADOR o JOINs explícitos
                    .Where(i => i.Cod_Cli == codigoCliente &&
                                i.Fec_Ins >= fechaInicio.Date &&
                                i.Fec_Ins <= fechaFinIncluyente)
                    .Select(i => new ServicioBE
                    {
                        Codigo = i.Cod_Ins,
                        Tipo = "Instalación",
                        Fecha = i.Fec_Ins,
                        // Asumimos que TB_TECNICO está relacionado con TB_INSTALACION
                        Tecnico = i.TB_TECNICO.Nom_Tec + " " + i.TB_TECNICO.Ape_Pat_Tec, // Concatenar
                        // Usamos Cod_Pur (Purificador) como parte del detalle
                        Detalle = "Modelo de Purificador: " + i.TB_PURIFICADOR.Tip_Pur,
                        CodigoCliente = i.Cod_Cli
                    });

                // ==============================
                // 2. CONSULTA DE MANTENIMIENTOS
                // ==============================
                var mantenimientos = ctx.TB_MANTENIMIENTO
                    .Where(m => m.Cod_Cli == codigoCliente &&
                                m.Fec_Man >= fechaInicio.Date &&
                                m.Fec_Man <= fechaFinIncluyente)
                    .Select(m => new ServicioBE
                    {
                        Codigo = m.Cod_Man,
                        Tipo = "Mantenimiento",
                        Fecha = m.Fec_Man,
                        // Asumimos que TB_TECNICO está relacionado con TB_MANTENIMIENTO
                        Tecnico = m.TB_TECNICO.Nom_Tec + " " + m.TB_TECNICO.Ape_Pat_Tec, // Concatenar
                        Detalle = m.Obs_Man, // El detalle está en Obs_Man
                        CodigoCliente = m.Cod_Cli
                    });

                // ==============================
                // 3. UNIFICACIÓN Y ORDENAMIENTO
                // ==============================
                // Usamos Union para combinar las dos listas (IQueryable<ServicioBE>)
                return instalaciones.Union(mantenimientos)
                                    .OrderBy(s => s.Fecha)
                                    .ToList();
            }
        }

        // El método para Totales también debe ser ajustado para consultar ambas tablas
        public (int TotalInstalaciones, int TotalMantenimientos) ObtenerTotalesServicios(string codigoCliente)
        {
            using (var ctx = new BD_KawsayEntities())
            {
                int totalInstalaciones = ctx.TB_INSTALACION.Count(i => i.Cod_Cli == codigoCliente);
                int totalMantenimientos = ctx.TB_MANTENIMIENTO.Count(m => m.Cod_Cli == codigoCliente);

                return (totalInstalaciones, totalMantenimientos);
            }
        }
        public ClienteBE ObtenerClientePorCodigo(string codigoCliente)
        {
            using (var ctx = new BD_KawsayEntities())
            {
                var clienteDB = ctx.TB_CLIENTE
                    .FirstOrDefault(c => c.Cod_Cli == codigoCliente);

                if (clienteDB != null)
                {
                    // Mapeo de la entidad de la DB (TB_CLIENTE) a la entidad de negocio (ClienteBE)
                    return new ClienteBE
                    {
                        Cod_Cli = clienteDB.Cod_Cli,
                        Dni_Cli = clienteDB.Dni_Cli,
                        Nom_Cli = clienteDB.Nom_Cli,
                        Ape_Pat_Cli = clienteDB.Ape_Pat_Cli,
                        Ape_Mat_Cli = clienteDB.Ape_Mat_Cli,
                        Tel_Cli = clienteDB.Tel_Cli,
                        Dir_Cli = clienteDB.Dir_Cli,
                        Cor_Cli = clienteDB.Cor_Cli,
                        Est_Cli = clienteDB.Est_Cli,
                        Fec_Nac_Cli = clienteDB.Fec_Nac_Cli,
                        Cod_Ubi = clienteDB.Cod_Ubi,
                        // El resto de campos de auditoría son opcionales para la consulta
                    };
                }
                return null;
            }
        }

        // ==============================================
        // 2. OBTENER UBICACIÓN (Método faltante 2)
        // ==============================================
        public string ObtenerNombreUbicacion(string codUbi)
        {
            using (var ctx = new BD_KawsayEntities())
            {
                // Asumo que TB_UBIGEO es la tabla que contiene la descripción geográfica
                // Y que tiene una propiedad de navegación o un campo para el nombre completo.
                var ubi = ctx.TB_UBIGEO.FirstOrDefault(u => u.Cod_Ubi == codUbi);

                if (ubi != null)
                {
                    // Debes usar las propiedades reales de tu tabla TB_UBIGEO para concatenar 
                    // (Ejemplo: Nom_Dep, Nom_Prov, Nom_Dist)
                    return $"{ubi.Dep_Ubi}-{ubi.Pro_Ubi}-{ubi.Dis_Ubi}";
                    // Reemplaza Nom_Dep, Nom_Prov, Nom_Dist con los nombres correctos de tus campos en TB_UBIGEO
                }
                return "Ubicación Desconocida";
            }
        }
    }
}