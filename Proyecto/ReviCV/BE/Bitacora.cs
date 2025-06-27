using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Bitacora
    {
        public string ID {  get; set; }
        public string Evento {  get; set; }
        public string Criticidad {  get; set; }
        public string Modulo {  get; set; }

        public Bitacora(string pID, string pEvento, string pCriticidad, string pModulo)
        {
            ID = pID;
            Evento = pEvento;
            Criticidad = pCriticidad;
            Modulo = pModulo;
        }
    }
}
