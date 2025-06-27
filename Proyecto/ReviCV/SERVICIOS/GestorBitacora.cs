using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public class GestorBitacora
    {
        private static GestorBitacora instancia;
        public static GestorBitacora Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new GestorBitacora();
                }
                return instancia;
            }
        }
        public List<Bitacora> ObtenerLogs()
        {
            List<Bitacora> listaLogs = new List<Bitacora>();
            string query = "SELECT * FROM Bitacora";
            using(SqlCommand CM = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                using(SqlDataReader DR = CM.ExecuteReader())
                {
                    while(DR.Read())
                    {
                        Bitacora log = new Bitacora(DR[0].ToString(), DR[1].ToString(), DR[2].ToString(), DR[3].ToString());
                        listaLogs.Add(log);
                    }
                }
            }
            return listaLogs;
        }
        public void GuardarLog(string pID, string pEvento, string pCriticidad, string pModulo)
        {
            string query = "INSERT INTO Bitacora (ID, Evento, Criticidad, Modulo) VALUES (@ID, @Evento, @Criticidad, @Modulo)";
            using (SqlCommand CM = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                CM.Parameters.AddWithValue("@ID", pID);
                CM.Parameters.AddWithValue("@Evento", pEvento);
                CM.Parameters.AddWithValue("@Criticidad", pCriticidad);
                CM.Parameters.AddWithValue("@Modulo", pModulo);
                CM.ExecuteNonQuery();
                Conexion.Instancia.CerrarConexion();
            }
        }
    }
}
