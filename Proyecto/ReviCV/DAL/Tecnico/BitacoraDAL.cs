using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;

namespace DAL
{
    public class BitacoraDAL
    {
        private static BitacoraDAL instancia;
        public static BitacoraDAL Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new BitacoraDAL();
                }
                return instancia;
            }
        }
        public List<Bitacora> ObtenerLogs()
        {
            List<Bitacora> listaLogs = new List<Bitacora>();
            string query = "SELECT * FROM Bitacora ORDER BY Fecha DESC";
            using (SqlCommand CM = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                using (SqlDataReader DR = CM.ExecuteReader())
                {
                    while (DR.Read())
                    {
                        Bitacora log = new Bitacora(int.Parse(DR[0].ToString()), DateTime.Parse(DR[1].ToString()), DR[2].ToString(), DR[3].ToString());
                        listaLogs.Add(log);
                    }
                }
            }
            return listaLogs;
        }
        public List<Bitacora> FiltrosBitacora(DateTime? desde, DateTime? hasta, string usuario, string operacion)
        {
            List<Bitacora> listaLogs = new List<Bitacora>();

            string query = "SELECT * FROM Bitacora WHERE 1=1";

            if (desde.HasValue)
                query += " AND Fecha >= @Desde";

            if (hasta.HasValue)
                query += " AND Fecha <= @Hasta";

            if (!string.IsNullOrEmpty(usuario))
                query += " AND Usuario LIKE @Usuario";

            if (!string.IsNullOrEmpty(operacion))
                query += " AND Operacion LIKE @Operacion";

            using (SqlCommand cmd = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                if (desde.HasValue)
                    cmd.Parameters.AddWithValue("@Desde", desde.Value);

                if (hasta.HasValue)
                    cmd.Parameters.AddWithValue("@Hasta", hasta.Value);

                if (!string.IsNullOrEmpty(usuario))
                    cmd.Parameters.AddWithValue("@Usuario", "%" + usuario + "%");

                if (!string.IsNullOrEmpty(operacion))
                    cmd.Parameters.AddWithValue("@Operacion", "%" + operacion + "%");

                Conexion.Instancia.AbrirConexion();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Bitacora log = new Bitacora(int.Parse(dr["ID"].ToString()), DateTime.Parse(dr["Fecha"].ToString()), dr["Operacion"].ToString(), dr["Usuario"].ToString());
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
