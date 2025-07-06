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
            return BackupRestoreDAL.DalBURS.RealizarBackup(backupPath);
        }
        public void RealizarRestore(string backupFilePath)
        {

            BackupRestoreDAL.DalBURS.RealizarRestore(backupFilePath);
        }
    }
}
