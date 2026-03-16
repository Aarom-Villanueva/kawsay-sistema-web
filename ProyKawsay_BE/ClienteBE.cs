using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BE
{
    // *** MUY IMPORTANTE: La clase debe ser PUBLIC para ser accesible desde BL y GUI. ***
    public class ClienteBE
    {
        // 1. Campos de identificación y contacto (NOT NULL en la DB)
        public string Cod_Cli { get; set; } // CHAR(6)
        public string Dni_Cli { get; set; } // CHAR(8)
        public string Nom_Cli { get; set; } // VARCHAR(30)
        public string Ape_Pat_Cli { get; set; } // VARCHAR(30)
        public string Ape_Mat_Cli { get; set; } // VARCHAR(30)
        public string Tel_Cli { get; set; } // VARCHAR(12)
        public string Cor_Cli { get; set; } // VARCHAR(50) NULL (puede ser string o string?)

        // Propiedad calculada: Usada en la GUI para mostrar el nombre completo
        public string NombresCompletos => $"{Ape_Pat_Cli} {Ape_Mat_Cli}, {Nom_Cli}";

        // 2. Campos de ubicación y estado
        public string Dir_Cli { get; set; } // VARCHAR(60) NULL
        public string Cod_Ubi { get; set; } // CHAR(6) NOT NULL (FK a TB_UBIGEO)
        public string Est_Cli { get; set; } // CHAR(1) NOT NULL ('A' o 'I')
        public DateTime Fec_Nac_Cli { get; set; } // DATE NOT NULL

        // 3. Campos de auditoría y binario (Usamos tipos anulables para los campos NULL en la DB)
        public byte[] Fot_Cli { get; set; } // VARBINARY(MAX) NULL
        public DateTime? Fec_Registro { get; set; } // DATETIME NULL
        public string Usu_Registro { get; set; } // VARCHAR(20) NULL
        public DateTime? Fec_Ult_Mod { get; set; } // DATETIME NULL
        public string Usu_Ult_Mod { get; set; } // VARCHAR(20) NULL
    }
}