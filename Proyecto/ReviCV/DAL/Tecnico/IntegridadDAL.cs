using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IntegridadDAL
    {

        public void SaveTableIntegrity(string tableName)
        {
            try
            {
                Dictionary<string, string> keyValues = DefineTableIntegrity(tableName);

                string query = @"
                    MERGE INTO TableVerifyDigitsTable AS target
                    USING (SELECT @TableName AS TableName) AS source
                    ON (target.TableName = source.TableName)
                    WHEN MATCHED THEN 
                        UPDATE SET HorizontalVerifyDigit = @HorizontalVerifyDigit, VerticalVerifyDigit = @VerticalVerifyDigit
                    WHEN NOT MATCHED THEN
                        INSERT (TableName, HorizontalVerifyDigit, VerticalVerifyDigit) 
                        VALUES (@TableName, @HorizontalVerifyDigit, @VerticalVerifyDigit);";

                using (SqlCommand cmd = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
                {
                    cmd.Parameters.AddWithValue("@TableName", tableName);
                    cmd.Parameters.AddWithValue("@HorizontalVerifyDigit", keyValues["HVD"]);
                    cmd.Parameters.AddWithValue("@VerticalVerifyDigit", keyValues["VVD"]);
                    Conexion.Instancia.AbrirConexion();
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex) { }
            finally
            {
                Conexion.Instancia.CerrarConexion();
            }
        }

        public bool CheckTableIntegrity(string tableName)
        {
            try
            {
                Dictionary<string, string> keyValues = DefineTableIntegrity(tableName);

                string query = $"SELECT * FROM TableVerifyDigitsTable WHERE TableName = '{tableName}'";

                using (var conn = Conexion.Instancia.ReturnConexion())
                {
                    Conexion.Instancia.AbrirConexion();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string a = reader[1].ToString();
                            if (reader[1].Equals(keyValues["HVD"]) && reader[2].Equals(keyValues["VVD"]))
                            {
                                Conexion.Instancia.CerrarConexion();
                                return true;
                            }
                            else
                            {
                                Conexion.Instancia.CerrarConexion();
                                return false;
                            }
                        }
                    }
                }

                Conexion.Instancia.CerrarConexion();
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                Conexion.Instancia.CerrarConexion();
            }
        }

        private string GenerateVerifyDigit(List<string[]> data)
        {
            //Guardo todos los datos hasheados
            List<string> hashedData = new List<string>();

            foreach (string[] row in data)
            {
                hashedData.Add(GenerateVerifyDigitUniqueRow(row));
            }

            return GenerateVerifyDigitUniqueRow(hashedData.ToArray());
        }

        private string GenerateVerifyDigitUniqueRow(string[] row)
        {
            //Guardo el hash actual
            string actualData = null;

            for (int i = 0; i < row.Length; i++)
            {
                //Le agrego el texto de la nueva posición al string
                actualData += row[i];

                //Hasheo el string para asegurarme evitar un overflow en tablas grandes
                //actualData = Encriptadr(actualData);
            }

            return actualData;
        }

        private Dictionary<string, string> DefineTableIntegrity(string tableName)
        {
            var rows = new List<string[]>();
            var columns = new List<string[]>(); // Cada elemento será un string[] que representa una columna

            using (var conn = Conexion.Instancia.ReturnConexion())
            {
                Conexion.Instancia.AbrirConexion();

                // Obtener la cantidad de filas y columnas
                int rowCount = (int)new SqlCommand($"SELECT COUNT(*) FROM {tableName};", conn).ExecuteScalar();
                int colCount = (int)new SqlCommand($"SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}';", conn).ExecuteScalar();

                // Inicializar la lista de columnas
                for (int i = 0; i < colCount; i++)
                {
                    columns.Add(new string[rowCount]); // Inicializar cada columna como un array de strings
                }

                // Cargar filas y columnas
                using (var cmd = new SqlCommand($"SELECT * FROM {tableName};", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    int currentRow = 0; // Para llevar el seguimiento de la fila actual

                    while (reader.Read())
                    {
                        var rowData = new string[colCount];
                        for (int col = 0; col < colCount; col++)
                        {
                            rowData[col] = reader[col].ToString();
                            columns[col][currentRow] = rowData[col]; // Asignar el valor a la columna correspondiente
                        }
                        rows.Add(rowData);
                        currentRow++; // Avanzar a la siguiente fila
                    }
                }
            }

            Conexion.Instancia.CerrarConexion();

            // Crear el diccionario de integridad
            return new Dictionary<string, string>
            {
                { "HVD", GenerateVerifyDigit(rows) ?? "0" },
                { "VVD", GenerateVerifyDigit(columns) ?? "0" }
            };
        }
    }
}
