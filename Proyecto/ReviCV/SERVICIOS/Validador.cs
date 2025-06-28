using ENTIDADES;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public class Validador
    {
        public bool Verificar(string pUsuario, string pContra)
        {            
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ValidarUsuario(pUsuario, pContra);
        }
    }
}
