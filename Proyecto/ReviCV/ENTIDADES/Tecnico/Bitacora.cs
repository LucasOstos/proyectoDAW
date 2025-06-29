using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Bitacora
    {
        public int ID {  get; set; }
        public DateTime Fecha {  get; set; }
        public string Operacion {  get; set; }
        public string Usuario {  get; set; }        

        public Bitacora(int pID, DateTime pFecha, string pOperacion, string pUsuario)
        {
            ID = pID;
            Fecha = pFecha;
            Operacion = pOperacion;
            Usuario = pUsuario;
        }
    }
}
