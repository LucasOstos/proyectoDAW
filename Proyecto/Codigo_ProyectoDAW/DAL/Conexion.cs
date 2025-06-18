using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Conexion
    {        
        private SqlConnection CO = new SqlConnection("Data Source=.;Initial Catalog=Proyecto_DAW;Integrated Security=True;");
        private static Conexion instancia;
        public static Conexion Instancia
        {
            get
            {
                if (instancia == null) instancia = new Conexion();
                return instancia;
            }
        }
        public void AbrirConexion()
        {
            if(CO.State == System.Data.ConnectionState.Open)
            {
            }
            else { CO.Open(); }
        }
        public void CerrarConexion()
        {
            CO.Close();
        }
        public SqlConnection ReturnConexion()
        {
            return CO;
        }
    }
}
