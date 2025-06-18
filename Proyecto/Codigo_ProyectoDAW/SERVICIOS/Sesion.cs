using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public class Sesion
    {
        public Usuario Usuario;
        private static Sesion instancia;
        public static Sesion Instancia
        {
            get
            {
                if (instancia == null) instancia = new Sesion();
                return instancia;
            }
        }

        public void LogIn(Usuario pUsuario)
        {
            Usuario = pUsuario;
        }
        public void LogOut()
        {
            if (Usuario != null)
            {
                Usuario = null;
            }
        }
        public bool IsLogueado()
        {
            return Usuario == null ? false : true;
        }

        public bool Verificar(string pUsuario, string pContra)
        {            
            return UsuarioDAL.Instancia.ValidarUsuario(pUsuario, pContra);
        }
    }
}
