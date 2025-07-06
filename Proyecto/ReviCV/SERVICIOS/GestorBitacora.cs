using ENTIDADES;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Tecnico;

namespace SERVICIOS
{
    public class GestorBitacora
    {

        public List<Bitacora> ObtenerLogs()
        {
            BitacoraDAL bitacoraDAL = new BitacoraDAL();
            return bitacoraDAL.ObtenerLogs();
        }

        public void GuardarLogBitacora(string pOperacion, string pUsuario)
        {
            BitacoraDAL bitacoraDAL = new BitacoraDAL();
            int id = bitacoraDAL.GuardarLog(pOperacion, pUsuario);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Bitacora, id);
        }

    }
}
