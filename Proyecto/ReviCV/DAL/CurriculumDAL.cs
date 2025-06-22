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
        private static CurriculumDAL instancia;
        public static CurriculumDAL Instancia
        {
            get
            {
                if (instancia == null) { instancia = new CurriculumDAL(); }
                return instancia;
            }
        }

        public void GuardarCurriculum(Curriculum cv)
        {
            string query = "INSERT INTO Curriculum (UsernameUsuario, Curriculum) VALUES (@UsernameUsuario, @Curriculum)";

            using (SqlCommand cmd = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();

                cmd.Parameters.AddWithValue("@UsernameUsuario", cv.Usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@Curriculum", cv.ArchivoCV);

                cmd.ExecuteNonQuery();

                Conexion.Instancia.CerrarConexion();
            }
        }

        public Curriculum ObtenerCurriculumPorID(int id)
        {
            Curriculum cv = null;

            string query = "SELECT ID_CV, UsernameUsuario, Curriculum FROM Curriculum WHERE ID_CV = @ID";

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



    }
}
