using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CurriculumDAL
    {
        public void GuardarCurriculum(Curriculum cv)
        {
            string query = $"INSERT INTO {TablasBD.Curriculum} (UsernameUsuario, Curriculum, Idioma, Rubro) VALUES (@UsernameUsuario, @Curriculum, @Idioma, @Rubro)";

            using (SqlCommand cmd = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();

                cmd.Parameters.AddWithValue("@UsernameUsuario", cv.Usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@Curriculum", cv.ArchivoCV);
                cmd.Parameters.AddWithValue("@Idioma", cv.Idioma.Item1);
                cmd.Parameters.AddWithValue("@Rubro", cv.Rubro.Item1);

                cmd.ExecuteNonQuery();

                Conexion.Instancia.CerrarConexion();
            }
        }

        public Curriculum ObtenerCurriculumFiltrado(string rubro, string idioma)
        {
            Curriculum cv = null;
            int idCV = 0;
            string username = null;
            byte[] archivoBytes = null;

            List<string> condiciones = new List<string>();
            List<SqlParameter> parametros = new List<SqlParameter>();

            // Consulta base con TOP 1 y selección aleatoria
            string queryBase = $"SELECT TOP 1 ID_CV, UsernameUsuario, Curriculum FROM {TablasBD.Curriculum} WHERE 1=1";

            // Agregar filtros dinámicamente
            if (!string.IsNullOrEmpty(rubro))
            {
                condiciones.Add("Rubro = @Rubro");
                parametros.Add(new SqlParameter("@Rubro", rubro));
            }

            if (!string.IsNullOrEmpty(idioma))
            {
                condiciones.Add("Idioma = @Idioma");
                parametros.Add(new SqlParameter("@Idioma", idioma));
            }

            // Unir condiciones con AND y agregar ORDER BY NEWID()
            string whereClause = string.Join(" AND ", condiciones);
            string finalQuery = $"{queryBase} {(condiciones.Count > 0 ? " AND " + whereClause : "")} ORDER BY NEWID()";

            using (SqlConnection conn = Conexion.Instancia.ReturnConexion())
            {
                using (SqlCommand cmd = new SqlCommand(finalQuery, conn))
                {
                    cmd.Parameters.AddRange(parametros.ToArray());
                    Conexion.Instancia.AbrirConexion();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            idCV = dr.GetInt32(dr.GetOrdinal("ID_CV"));
                            username = dr.GetString(dr.GetOrdinal("UsernameUsuario"));

                            if (!dr.IsDBNull(dr.GetOrdinal("Curriculum")))
                            {
                                long length = dr.GetBytes(dr.GetOrdinal("Curriculum"), 0, null, 0, 0);
                                archivoBytes = new byte[length];
                                dr.GetBytes(dr.GetOrdinal("Curriculum"), 0, archivoBytes, 0, (int)length);
                            }
                        }
                    } // El SqlDataReader se cierra automáticamente aquí

                    Conexion.Instancia.CerrarConexion();
                }
            }

            // Ahora que la conexión está cerrada, podemos llamar a ObtenerUsuario
            if (username != null)
            {
                UsuarioDAL gUsuarios = new UsuarioDAL();
                var usuario = gUsuarios.ObtenerUsuario(username);

                cv = new Curriculum
                {
                    ID_CV = idCV,
                    Usuario = usuario,
                    ArchivoCV = archivoBytes
                };
            }

            return cv;
        }

        public Curriculum ObtenerCurriculumPorID(int id)
        {
            Curriculum cv = null;

            string query = $"SELECT ID_CV, UsernameUsuario, Curriculum FROM {TablasBD.Curriculum} WHERE ID_CV = @ID";

            using (SqlConnection conn = Conexion.Instancia.ReturnConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    Conexion.Instancia.AbrirConexion();

                    string username = null;
                    byte[] archivoBytes = null;
                    int idCV = 0;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            idCV = dr.GetInt32(dr.GetOrdinal("ID_CV"));
                            username = dr.GetString(dr.GetOrdinal("UsernameUsuario"));

                            if (!dr.IsDBNull(dr.GetOrdinal("Curriculum")))
                            {
                                long length = dr.GetBytes(dr.GetOrdinal("Curriculum"), 0, null, 0, 0);
                                archivoBytes = new byte[length];
                                dr.GetBytes(dr.GetOrdinal("Curriculum"), 0, archivoBytes, 0, (int)length);
                            }
                        }
                    }

                    // Cerrá conexión antes de llamar a otro método que abre reader
                    Conexion.Instancia.CerrarConexion();

                    if (username != null)
                    {
                        UsuarioDAL gUsuarios = new UsuarioDAL();
                        var usuario = gUsuarios.ObtenerUsuario(username);

                        cv = new Curriculum();
                        cv.ID_CV = idCV;
                        cv.Usuario = usuario;
                        cv.ArchivoCV = archivoBytes;
                    }
                    Conexion.Instancia.CerrarConexion();
                }
            }

            return cv;
        }

        public Dictionary<int, string> ObtenerIdiomas()
        {
            Dictionary<int, string> idiomas = new Dictionary<int, string>();
            string query = $"SELECT ID_Idioma, Idioma FROM {TablasBD.Idioma}";

            using (SqlConnection conn = Conexion.Instancia.ReturnConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    Conexion.Instancia.AbrirConexion();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID_Idioma"));
                            string nombre = reader.GetString(reader.GetOrdinal("Idioma"));
                            idiomas.Add(id, nombre);
                        }
                    }
                    Conexion.Instancia.CerrarConexion();
                }
            }

            return idiomas;
        }

        public Dictionary<int, string> ObtenerRubros()
        {
            Dictionary<int, string> rubros = new Dictionary<int, string>();
            string query = $"SELECT ID_Rubro, Rubro FROM {TablasBD.Rubro}";

            using (SqlConnection conn = Conexion.Instancia.ReturnConexion())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    Conexion.Instancia.AbrirConexion();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(reader.GetOrdinal("ID_Rubro"));
                            string nombre = reader.GetString(reader.GetOrdinal("Rubro"));
                            rubros.Add(id, nombre);
                        }
                    }
                    Conexion.Instancia.CerrarConexion();
                }
            }

            return rubros;
        }


    }
}
