using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DAL;
using ENTIDADES;

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
            return BitacoraDAL.Instancia.ObtenerLogs();
        }
        public List<Bitacora> FiltrosBitacora(DateTime? desde, DateTime? hasta, string usuario, string operacion)
        {
            return BitacoraDAL.Instancia.FiltrosBitacora(desde, hasta, usuario, operacion);
        }
        public void GuardarLog(string pOperacion, string pUsuario)
        {
            BitacoraDAL.Instancia.GuardarLog(pOperacion, pUsuario);
        }
    }
}
