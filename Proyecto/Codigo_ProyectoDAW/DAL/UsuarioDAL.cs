using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UsuarioDAL
    {
        private static UsuarioDAL instancia;
        public static UsuarioDAL Instancia
        {
            get
            {
                if (instancia == null) { instancia = new UsuarioDAL(); }
                return instancia;
            }
        }
        public bool ValidarUsuario(string pUsuario, string Contra)
        {
            bool x = false;
            string query = $"SELECT * FROM Usuario WHERE username = @username AND contraseña = @contraseña";
            using (SqlCommand CM = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                CM.Parameters.AddWithValue("@username", pUsuario);
                CM.Parameters.AddWithValue("@contraseña", Contra);
                Conexion.Instancia.AbrirConexion();
                using (SqlDataReader DR = CM.ExecuteReader())
                {
                    while (DR.Read())
                    {
                        if (DR != null)
                        {
                            x = true;
                            break;
                        }
                    }
                }
            }
            Conexion.Instancia.CerrarConexion();
            return x;
        }
        public Usuario ObtenerUsuario(string pUsuario)
        {
            Usuario U = null;
            string Query = $"SELECT * FROM Usuario WHERE username = @username";
            using (SqlCommand CM = new SqlCommand(Query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                CM.Parameters.AddWithValue("@username", pUsuario);
                using (SqlDataReader DR = CM.ExecuteReader())
                {
                    if (DR.Read())
                    {
                        U = new Usuario(int.Parse(DR[0].ToString()), DR[1].ToString(), DR[2].ToString(), int.Parse(DR[3].ToString()), DR[4].ToString(), DR[5].ToString(), DR[6].ToString());                        
                    }
                }
            }
            Conexion.Instancia.CerrarConexion();
            return U;
        }
    }
}
