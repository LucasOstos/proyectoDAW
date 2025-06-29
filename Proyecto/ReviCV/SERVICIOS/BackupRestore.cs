using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace SERVICIOS
{
    public class BackupRestore
    {
        private static BackupRestore Instance;
        public static BackupRestore DalBURS
        {
            get
            {
                if (Instance == null) { Instance = new BackupRestore(); }
                return Instance;
            }
        }
        public string RealizarBackup(string backupPath)
        {
            string nombreArchivo = $"ProyectoDAW_{DateTime.Now:ddMMyy_HHmm}.bak";
            string rutaCompleta = Path.Combine(backupPath, nombreArchivo);
            string comandoBackup = $"BACKUP DATABASE Proyecto_DAW TO DISK='{rutaCompleta}'";

            using (SqlCommand cmd = new SqlCommand(comandoBackup, Conexion.Instancia.ReturnConexion()))
            {
                Conexion.Instancia.AbrirConexion();
                cmd.ExecuteNonQuery();
                Conexion.Instancia.CerrarConexion();
            }

            return rutaCompleta;
        }
        public void RealizarRestore(string backupFilePath)
        {

            Conexion.Instancia.AbrirConexion();

            using (SqlCommand setMaster = new SqlCommand("USE master;", Conexion.Instancia.ReturnConexion()))
            {
                setMaster.ExecuteNonQuery();
            }
            //progressForm.ProgressValue = 25;
            using (SqlCommand setSingleUser = new SqlCommand("ALTER DATABASE Proyecto_DAW SET SINGLE_USER WITH ROLLBACK IMMEDIATE;", Conexion.Instancia.ReturnConexion()))
            {
                setSingleUser.ExecuteNonQuery();
            }
            //progressForm.ProgressValue = 50;
            string query = $"RESTORE DATABASE Proyecto_DAW FROM DISK = '{backupFilePath}' WITH REPLACE;";
            using (SqlCommand cmd = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
            {
                cmd.ExecuteNonQuery();
            }

            using (SqlCommand setMultiUser = new SqlCommand("ALTER DATABASE Proyecto_DAW SET MULTI_USER;", Conexion.Instancia.ReturnConexion()))
            {
                setMultiUser.ExecuteNonQuery();
            }

            Conexion.Instancia.CerrarConexion();
        }
    }
}
