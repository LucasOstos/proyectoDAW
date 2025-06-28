﻿using System;
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
        public List<Usuario> FiltrarUsuarios(string dni, string username, string email, string rol)
        {
            List<Usuario> lista = new List<Usuario>();
            List<string> condiciones = new List<string>();
            SqlCommand cmd = new SqlCommand();

            string query = $"SELECT * FROM {TablasBD.Usuario}";

            if (!string.IsNullOrWhiteSpace(dni))
            {
                condiciones.Add("DNI = @DNI");
                cmd.Parameters.AddWithValue("@DNI", dni);
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                condiciones.Add("username LIKE @Username");
                cmd.Parameters.AddWithValue("@Username", $"%{username}%");
            }

            if (!string.IsNullOrWhiteSpace(email))
            {
                condiciones.Add("Mail LIKE @Email");
                cmd.Parameters.AddWithValue("@Email", $"%{email}%");
            }

            if (!string.IsNullOrWhiteSpace(rol))
            {
                condiciones.Add("Rol = @Rol");
                cmd.Parameters.AddWithValue("@Rol", rol);
            }

            if (condiciones.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", condiciones);
            }

            cmd.CommandText = query;
            cmd.Connection = Conexion.Instancia.ReturnConexion();

            Conexion.Instancia.AbrirConexion();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Usuario u = new Usuario()
                    {
                        DNI = int.Parse(reader["DNI"].ToString()),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        NombreUsuario = reader["username"].ToString(),
                        Password = reader["password"].ToString(),
                        Email = reader["Mail"].ToString(),
                        Rol = reader["Rol"].ToString()
                    };
                    lista.Add(u);
                }
            }
            Conexion.Instancia.CerrarConexion();

            return lista;
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
        public void EliminarUsuario(string dni)
        {
            string query = $"DELETE FROM {TablasBD.Usuario} WHERE DNI = @DNI";

            using (SqlCommand cm = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                cm.Parameters.AddWithValue("@DNI", dni);
                cm.ExecuteNonQuery();
                Conexion.Instancia.CerrarConexion();
            }
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
        public void ModificarUsuario(Usuario U)
        {
            string query = $@"UPDATE {TablasBD.Usuario} SET Nombre = @Nombre,Apellido = @Apellido, username = @Username, Mail = @Mail, Rol = @Rol WHERE DNI = @DNI";

            using (SqlCommand cm = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                cm.Parameters.AddWithValue("@Nombre", U.Nombre);
                cm.Parameters.AddWithValue("@Apellido", U.Apellido);
                cm.Parameters.AddWithValue("@Username", U.NombreUsuario);
                cm.Parameters.AddWithValue("@Mail", U.Email);
                cm.Parameters.AddWithValue("@Rol", U.Rol);
                cm.Parameters.AddWithValue("@DNI", U.DNI);
                cm.ExecuteNonQuery();
                Conexion.Instancia.CerrarConexion();
            }
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
