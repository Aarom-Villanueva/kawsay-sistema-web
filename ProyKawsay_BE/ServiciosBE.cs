using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BE
{
    public class ServicioBE
    {
        // Propiedad que almacenará el código único (Cod_Ins o Cod_Man)
        public string Codigo { get; set; }

        // Propiedad que define si es "Instalación" o "Mantenimiento"
        public string Tipo { get; set; }

        // Propiedad que almacena la fecha del servicio (Fec_Ins o Fec_Man)
        public DateTime Fecha { get; set; }

        // Propiedad que contendrá el nombre completo del técnico
        public string Tecnico { get; set; }

        // Propiedad que contendrá la descripción del trabajo (Cod_Pur + Detalle u Obs_Man)
        public string Detalle { get; set; }

        // Propiedad auxiliar usada en la capa ADO para filtrar
        public string CodigoCliente { get; set; }
    }
}