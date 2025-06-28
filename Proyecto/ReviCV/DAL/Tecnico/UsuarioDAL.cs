using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class UsuarioDAL
    {
        public bool ValidarUsuario(string pUsuario, string Contra)
        {
            bool x = false;
            string query = $"SELECT * FROM {TablasBD.Usuario} WHERE username = @username AND password = @password";
            using (SqlCommand CM = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                CM.Parameters.AddWithValue("@username", pUsuario);
                CM.Parameters.AddWithValue("@password", Contra);
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
            string Query = $"SELECT * FROM {TablasBD.Usuario} WHERE username = @username";
            using (SqlCommand CM = new SqlCommand(Query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                CM.Parameters.AddWithValue("@username", pUsuario);
                using (SqlDataReader DR = CM.ExecuteReader())
                {
                    if (DR.Read())
                    {
                        U = new Usuario(int.Parse(DR[0].ToString()), DR[1].ToString(), DR[2].ToString(), DR[3].ToString(), "", DR[5].ToString(), DR[6].ToString());
                    }
                }
            }
            Conexion.Instancia.CerrarConexion();
            return U;
        }

        public void InsertarUsuario(Usuario U)
        {
            string Query = $"INSERT INTO {TablasBD.Usuario} (DNI, Nombre, Apellido, username, password, Mail, Rol) VALUES (@DNI, @Nombre, @Apellido, @Username, @Pass, @Mail, @Rol)";

            using (SqlCommand CM = new SqlCommand(Query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                CM.Parameters.AddWithValue("@DNI", U.DNI);
                CM.Parameters.AddWithValue("@Nombre", U.Nombre);
                CM.Parameters.AddWithValue("@Apellido", U.Apellido);
                CM.Parameters.AddWithValue("@Username", U.NombreUsuario);
                CM.Parameters.AddWithValue("@Pass", U.Password);
                CM.Parameters.AddWithValue("@Mail", U.Email);
                CM.Parameters.AddWithValue("@Rol", U.Rol);
                CM.ExecuteNonQuery();
            }
            Conexion.Instancia.CerrarConexion();
        }

        public List<string> ObtenerTodosNombresUsuarios()
        {
            List<string> U = new List<string>();
            string Query = $"SELECT * FROM {TablasBD.Usuario}";
            using (SqlCommand CM = new SqlCommand(Query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                using (SqlDataReader DR = CM.ExecuteReader())
                {
                    while (DR.Read())
                    {
                        U.Add(DR[3].ToString());
                    }
                }
            }
            Conexion.Instancia.CerrarConexion();
            return U;
        }

        public List<Usuario> ObtenerTodosUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = $"SELECT * FROM {TablasBD.Usuario}";

            using (SqlCommand cm = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                using (SqlDataReader dr = cm.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Usuario u = new Usuario
                        (
                            int.Parse(dr["DNI"].ToString()),
                            dr["Nombre"].ToString(),
                            dr["Apellido"].ToString(),
                            dr["username"].ToString(),
                            "",
                            dr["Mail"].ToString(),
                            dr["Rol"].ToString()
                        );

                        usuarios.Add(u);
                    }
                }
            }
            Conexion.Instancia.CerrarConexion();
            return usuarios;
        }

    }
}
