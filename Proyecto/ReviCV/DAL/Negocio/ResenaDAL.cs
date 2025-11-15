using ENTIDADES;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ResenaDAL
    {
        public int GuardarResena(Resena r)
        {
            string query = @"INSERT INTO Resena (Diseno, Comentarios, ID_CV, username_resenador, Contenido, Claridad, Relevancia) 
                     VALUES (@Diseno, @Comentarios, @ID_CV, @UsernameResenador, @Contenido, @Claridad, @Relevancia);
                     SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlCommand cmd = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {


                cmd.Parameters.AddWithValue("@Diseno", r.Diseno);
                cmd.Parameters.AddWithValue("@Comentarios", r.Comentarios);
                cmd.Parameters.AddWithValue("@ID_CV", r.ID_CV);
                cmd.Parameters.AddWithValue("@UsernameResenador", r.UsuarioReseñador);
                cmd.Parameters.AddWithValue("@Contenido", r.Contenido);
                cmd.Parameters.AddWithValue("@Claridad", r.Claridad);
                cmd.Parameters.AddWithValue("@Relevancia", r.Relevancia);

                int nuevoId = (int)cmd.ExecuteScalar();



                return nuevoId;
            }
        }

        public List<Resena> LeerResenasDeCV(int idCV)
        {
            List<Resena> lista = new List<Resena>();

            string query = $"SELECT * FROM {TablasBD.Resena} WHERE ID_CV = @ID";

            using (SqlConnection conn = Conexion.Instancia.ReturnConexion())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", idCV);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Resena r = new Resena();

                        r.ID_Resena = dr.GetInt32(dr.GetOrdinal("ID"));
                        r.ID_CV = dr.GetInt32(dr.GetOrdinal("ID_CV"));
                        r.UsuarioReseñador = dr.GetString(dr.GetOrdinal("username_resenador"));
                        r.Contenido = dr.GetInt32(dr.GetOrdinal("Contenido"));
                        r.Diseno = dr.GetInt32(dr.GetOrdinal("Diseno"));
                        r.Claridad = dr.GetInt32(dr.GetOrdinal("Claridad"));
                        r.Relevancia = dr.GetInt32(dr.GetOrdinal("Relevancia"));
                        r.Comentarios = dr.GetString(dr.GetOrdinal("Comentarios"));

                        lista.Add(r);
                    }
                }
            }

            return lista;
        }

    }
}
