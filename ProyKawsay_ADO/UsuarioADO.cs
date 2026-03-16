using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyKawsay_BE;

namespace ProyKawsay_ADO
{
    public class UsuarioADO
    {
        public UsuarioBE Autenticar(string login, string pass)
        {
            using (var db = new BD_KawsayEntities())
            {
                var u = db.TB_USUARIO
                          .FirstOrDefault(x => x.Login_Usuario == login && x.Pass_Usuario == pass);

                if (u == null) return null;

                return new UsuarioBE
                {
                    LoginUsuario = u.Login_Usuario,
                    NombreCompleto = (u.Nom_Usuario ?? "") + " " + (u.Ape_Pat_Usuario ?? "") + " " + (u.Ape_Mat_Usuario ?? ""),
                    NivelUsuario = u.Niv_Usuario ?? 0,
                    EstUsuario = u.Est_Usuario ?? 0
                };
            }
        }
    }
}