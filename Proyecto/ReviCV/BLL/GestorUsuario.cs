using ENTIDADES;
using DAL;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GestorUsuario
    {
        public Usuario ObtenerUsuario(string pUsuario)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ObtenerUsuario(pUsuario);
        }

        public void InsertarUsuario(Usuario pUsuario)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            usuarioDAL.InsertarUsuario(pUsuario);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.GuardarIntegridad(TablasBD.Usuario);
        }

        public List<string> ObtenerTodosNombresUsuarios()
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ObtenerTodosNombresUsuarios();
        }
    }
}
