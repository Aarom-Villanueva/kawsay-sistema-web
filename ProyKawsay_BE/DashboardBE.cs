using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BE
{
    public class DashboardBE
    {
        public int AdministradoresActivos { get; set; }
        public int ClientesActivos { get; set; }
        public int TecnicosActivos { get; set; }
        public int TotalInstalaciones { get; set; }
        public int TotalMantenimientos { get; set; }
        public int TotalServicios { get; set; }
    }
}

