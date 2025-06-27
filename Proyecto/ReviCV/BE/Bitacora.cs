using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bitacora
    {
        public DateTime Fecha {  get; set; }
        public string Operacion {  get; set; }
        public string Usuario {  get; set; }        

        public Bitacora(DateTime pFecha, string pOperacion, string pUsuario)
        {
            Fecha = pFecha;
            Operacion = pOperacion;
            Usuario = pUsuario;
        }
    }
}
