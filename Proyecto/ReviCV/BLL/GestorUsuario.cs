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
            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            string DVH = gestorIntegridad.CalcularDigitoVerificador(pUsuario.ToArray());
            usuarioDAL.InsertarUsuario(pUsuario, DVH);
            gestorIntegridad.GuardarIntegridadTabla(TablasBD.Usuario);
        }
        public void EliminarUsuario(string dni)
        {
            UsuarioDAL usuarioDAL=new UsuarioDAL();
            usuarioDAL.EliminarUsuario(dni);
        }
        public void ModificarUsuario(Usuario nuevo)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            usuarioDAL.ModificarUsuario(nuevo);
        }
        public List<string> ObtenerTodosNombresUsuarios()
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ObtenerTodosNombresUsuarios();
        }

        public List<Usuario> ObtenerTodosUsuarios()
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.ObtenerTodosUsuarios();
        }
        public List<Usuario> FiltrarUsuarios(string dni, string username, string email, string rol)
        {
            UsuarioDAL usuarioDAL = new UsuarioDAL();
            return usuarioDAL.FiltrarUsuarios(dni, username, email, rol);
        }
    }
}
