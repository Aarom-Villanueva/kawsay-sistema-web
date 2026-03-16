using ProyKawsay_ADO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyKawsay_BE;

namespace ProyKawsay_BL
{
    public class UsuarioBL
    {
        private readonly UsuarioADO _ado = new UsuarioADO();
        public UsuarioBE Autenticar(string login, string pass) => _ado.Autenticar(login, pass);
    }
}