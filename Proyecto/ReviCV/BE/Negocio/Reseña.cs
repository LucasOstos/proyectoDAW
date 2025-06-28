using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTIDADES
{
    public class Reseña
    {
        public int ID_Reseña { get; set; }
        public int ID_CV { get; set; }
        public int ID_UsuarioReseñador { get; set; }
        public decimal Calificacion { get; set; }
        public string Comentarios { get; set; }

        public Reseña(int idReseña, int idCV, int idUsuarioReseñador, decimal calificacion, string comentarios)
        {
            ID_Reseña = idReseña;
            ID_CV = idCV;
            ID_UsuarioReseñador = idUsuarioReseñador;
            Calificacion = calificacion;
            Comentarios = comentarios;
        }
    }
}
