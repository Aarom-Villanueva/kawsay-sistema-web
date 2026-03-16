using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BE
{
    public class ProgramacionDetalleBE
    {
        public Guid IdTemp { get; set; }
        public string CodCli { get; set; }
        public DateTime FecVisita { get; set; }
        public char TipServ { get; set; } // 'I' o 'M', tenganlo en cuenta
        public string ObsDet { get; set; }
    }
}