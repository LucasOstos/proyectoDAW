using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class IntegridadDAL
    {
        private readonly HashSet<string> tablasExcluidas = new HashSet<string>
        {
            TablasBD.DigitoVerificador.ToString(),
            TablasBD.Bitacora.ToString()
        };

        public List<string> ObtenerTablasAVerificar()
        {
            var tablas = new List<string>();
            using (var conn = Conexion.Instancia.ReturnConexion())
            {
                string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombreTabla = reader.GetString(0);
                        if (!tablasExcluidas.Contains(nombreTabla))
                            tablas.Add(nombreTabla);
                    }
                }
            }
            return tablas;
        }

        public (List<string[]> filas, List<string[]> columnas) ObtenerDatosTabla(TablasBD tabla)
        {
            string nombreTabla = tabla.ToString();
            var filas = new List<string[]>();
            var columnas = new List<List<string>>();

            using (var conn = Conexion.Instancia.ReturnConexion())
            {

                var colCountCmd = new SqlCommand("SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @tabla", conn);
                colCountCmd.Parameters.AddWithValue("@tabla", nombreTabla);
                int colCount = (int)colCountCmd.ExecuteScalar();

                for (int i = 0; i < colCount; i++)
                    columnas.Add(new List<string>());

                using (var cmd = new SqlCommand($"SELECT * FROM {nombreTabla}", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fila = new string[colCount];
                        for (int i = 0; i < colCount; i++)
                        {
                            string val = reader[i].ToString();
                            fila[i] = val;
                            columnas[i].Add(val);
                        }
                        filas.Add(fila);
                    }
                }
            }

            var columnasFinales = new List<string[]>();
            foreach (var col in columnas)
                columnasFinales.Add(col.ToArray());

            return (filas, columnasFinales);
        }

        public void GuardarRegistroIntegridad(TablasBD tabla, string HVD, string VVD)
        {
            string nombreTabla = tabla.ToString();

            string query = $@"
                MERGE INTO {TablasBD.DigitoVerificador} AS destino
                USING (SELECT @NombreTabla AS NombreTabla) AS origen
                ON (destino.NombreTabla = origen.NombreTabla)
                WHEN MATCHED THEN 
                    UPDATE SET DigitoVerificadorHorizontal = @HVD, DigitoVerificadorVertical = @VVD
                WHEN NOT MATCHED THEN
                    INSERT (NombreTabla, DigitoVerificadorHorizontal, DigitoVerificadorVertical) 
                    VALUES (@NombreTabla, @HVD, @VVD);";

            using (var conn = Conexion.Instancia.ReturnConexion())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NombreTabla", nombreTabla);
                cmd.Parameters.AddWithValue("@HVD", HVD);
                cmd.Parameters.AddWithValue("@VVD", VVD);
                cmd.ExecuteNonQuery();
            }
        }

        public (string HVD, string VVD)? LeerRegistroIntegridad(TablasBD tabla)
        {
            string nombreTabla = tabla.ToString();
            string query = $@"
                SELECT DigitoVerificadorHorizontal, DigitoVerificadorVertical 
                FROM {TablasBD.DigitoVerificador} 
                WHERE NombreTabla = @NombreTabla";

            using (var conn = Conexion.Instancia.ReturnConexion())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NombreTabla", nombreTabla);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (reader.GetString(0), reader.GetString(1));
                    }
                }
            }

            return null;
        }
    }
}
