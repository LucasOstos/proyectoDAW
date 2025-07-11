﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class IntegridadDAL
    {
        private readonly HashSet<string> tablasExcluidas = new HashSet<string>
        {
            TablasBD.DigitoVerificador.ToString()
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

        public List<string> ObtenerDVHs(TablasBD tabla)
        {
            string nombreTabla = tabla.ToString();
            var dvhs = new List<string>();
            using (var conn = Conexion.Instancia.ReturnConexion())
            {
                string query = $"SELECT DVH FROM {nombreTabla}";
                using (var cmd = new SqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if(!reader.IsDBNull(0))
                        {
                            dvhs.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return dvhs;
        }

        public List<(string[] datos, string dvh)> ObtenerDatosTabla(TablasBD tabla)
        {
            string nombreTabla = tabla.ToString();
            var resultado = new List<(string[] datos, string dvh)>();

            using (var conn = Conexion.Instancia.ReturnConexion())
            {
                var colsCmd = new SqlCommand(@"
            SELECT COLUMN_NAME 
            FROM INFORMATION_SCHEMA.COLUMNS 
            WHERE TABLE_NAME = @tabla AND COLUMN_NAME <> 'DVH'
            ORDER BY ORDINAL_POSITION", conn);

                colsCmd.Parameters.AddWithValue("@tabla", nombreTabla);

                var columnasValidas = new List<string>();
                using (var readerCols = colsCmd.ExecuteReader())
                {
                    while (readerCols.Read())
                    {
                        columnasValidas.Add(readerCols.GetString(0));
                    }
                }

                string selectCols = string.Join(", ", columnasValidas) + ", DVH";

                using (var cmd = new SqlCommand($"SELECT {selectCols} FROM {nombreTabla}", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    int colCount = columnasValidas.Count;

                    while (reader.Read())
                    {
                        var datosFila = new string[colCount];
                        for (int i = 0; i < colCount; i++)
                        {
                            datosFila[i] = reader[i]?.ToString() ?? string.Empty;
                        }

                        string dvh = reader[colCount]?.ToString() ?? string.Empty;

                        resultado.Add((datosFila, dvh));
                    }
                }
            }

            return resultado;
        }



        public void GuardarRegistroIntegridad(TablasBD tabla, string DVV, int cantidadRegistros)
        {
            string query = $@"
                MERGE INTO {TablasBD.DigitoVerificador} AS destino
                USING (SELECT @NombreTabla AS NombreTabla) AS origen
                ON (destino.NombreTabla = origen.NombreTabla)
                WHEN MATCHED THEN 
                    UPDATE SET DigitoVerificadorVertical = @DVV, CantidadRegistros = @CR
                WHEN NOT MATCHED THEN
                    INSERT (NombreTabla, DigitoVerificadorVertical, CantidadRegistros) 
                    VALUES (@NombreTabla, @DVV, @CR);";

            using (var conn = Conexion.Instancia.ReturnConexion())
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NombreTabla", tabla.ToString());
                cmd.Parameters.AddWithValue("@DVV", DVV);
                cmd.Parameters.AddWithValue("@CR", cantidadRegistros);
                cmd.ExecuteNonQuery();
            }
        }

        public (string DVV, int CR)? LeerRegistroIntegridad(TablasBD tabla)
        {
            string nombreTabla = tabla.ToString();
            string query = $@"
        SELECT DigitoVerificadorVertical, CantidadRegistros
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
                        // Verificar si alguno de los campos es null en la base
                        if (reader.IsDBNull(0) || reader.IsDBNull(1))
                            return null;

                        return (reader.GetString(0), reader.GetInt32(1));
                    }
                }
            }

            return null;
        }

        public void GuardarNuevoDVH(TablasBD tabla, string clavePK, string dvhCalculado)
        {
            string nombreTabla = tabla.ToString();
            string nombrePK = ObtenerClavePrimariaDesdeBD(nombreTabla);

            if (string.IsNullOrEmpty(nombrePK))
                throw new InvalidOperationException($"No se encontró clave primaria para la tabla {nombreTabla}");

            string query = $@"UPDATE {nombreTabla} SET DVH = @DVH WHERE {nombrePK} = @Clave";

            using (var conn = Conexion.Instancia.ReturnConexion())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@DVH", dvhCalculado);
                    cmd.Parameters.AddWithValue("@Clave", clavePK);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private string ObtenerClavePrimariaDesdeBD(string nombreTabla)
        {
            string query = @"
                SELECT KU.COLUMN_NAME
                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
                INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KU
                ON TC.CONSTRAINT_NAME = KU.CONSTRAINT_NAME
                AND TC.TABLE_NAME = KU.TABLE_NAME
                WHERE TC.CONSTRAINT_TYPE = 'PRIMARY KEY'
                AND KU.TABLE_NAME = @Tabla";

            using (var conn = Conexion.Instancia.ReturnConexion())
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Tabla", nombreTabla);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0);
                        }
                    }
                }
            }

            return null;
        }

    }
}
