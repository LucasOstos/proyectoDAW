using ENTIDADES;
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
                        Bitacora log = new Bitacora(int.Parse(DR[0].ToString()), DateTime.Parse(DR[1].ToString()), DR[2].ToString(), DR[3].ToString());
                        listaLogs.Add(log);
                    }
                }
            }
            return listaLogs;
        }
        public void GuardarLog(string pOperacion, string pUsuario)
        {
            string query = "INSERT INTO Bitacora (Fecha, Operacion, Usuario) VALUES (@Fecha, @Operacion, @Usuario)";
            using (SqlCommand CM = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                CM.Parameters.AddWithValue("@Fecha", DateTime.Now);
                CM.Parameters.AddWithValue("@Operacion", pOperacion);
                CM.Parameters.AddWithValue("@Usuario", pUsuario);
                CM.ExecuteNonQuery();
                Conexion.Instancia.CerrarConexion();
            }
        }
    }
}
