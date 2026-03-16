using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyKawsay_BE
{
    public class UsuarioBE
    {
        public string LoginUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public int NivelUsuario { get; set; }
        public int EstUsuario { get; set; } // 1 activo, 0 inactivo
    }
}