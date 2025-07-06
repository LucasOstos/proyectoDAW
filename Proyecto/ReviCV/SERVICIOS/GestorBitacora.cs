using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Tecnico;
using ENTIDADES;


namespace SERVICIOS
{
    public class GestorBitacora
    {

        public List<Bitacora> ObtenerLogs()
        {
            BitacoraDAL bitacoraDAL = new BitacoraDAL();
            return bitacoraDAL.ObtenerLogs();
        }

        public List<Bitacora> FiltrosBitacora(DateTime? desde, DateTime? hasta, string usuario, string operacion)
        {
            BitacoraDAL bitacoraDAL = new BitacoraDAL();
            return bitacoraDAL.FiltrosBitacora(desde, hasta, usuario, operacion);
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
