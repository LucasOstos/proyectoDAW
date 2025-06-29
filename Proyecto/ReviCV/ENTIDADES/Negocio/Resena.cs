using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Resena
    {
        public int ID_Resena { get; set; }
        public int ID_CV { get; set; }
        public string UsuarioReseñador { get; set; }
        public int Contenido { get; set; }
        public int Diseno {get; set;}
        public int Claridad { get; set; }
        public int Relevancia { get; set; }
        public string Comentarios { get; set; }

        public Resena(int idReseña, int idCV, string nUsuarioReseñador, int nDiseno, int nClaridad, int nRelevancia, string comentarios, int a)
        {
            ID_Resena = idReseña;
            ID_CV = idCV;
            UsuarioReseñador = nUsuarioReseñador;
            Diseno = nDiseno;
            Claridad = nClaridad;
            Relevancia = nRelevancia;
            Comentarios = comentarios;
        }

        public Resena() { }
    }
}
