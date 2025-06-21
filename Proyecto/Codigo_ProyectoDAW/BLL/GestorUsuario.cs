using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GestorUsuario
    {
        private static GestorUsuario instancia;
        public static GestorUsuario Instancia
        {
            get
            {
                if (instancia == null) { instancia = new GestorUsuario(); }
                return instancia;
            }
        }
        public Usuario ObtenerUsuario(string pUsuario)
        {
            return UsuarioDAL.Instancia.ObtenerUsuario(pUsuario);
        }

        public void InsertarUsuario(Usuario pUsuario)
        {
            UsuarioDAL.Instancia.InsertarUsuario(pUsuario);
        }
    }
}
