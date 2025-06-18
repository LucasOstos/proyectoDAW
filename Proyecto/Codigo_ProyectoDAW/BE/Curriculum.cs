using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Curriculum
    {
        public int Num_Curriculum { get; set; }
        public Usuario ID_Usuario { get; set; }
        public string Titulo { get; set; }
        public string Experiencia { get; set; }
        public string Educacion { get; set; }
        public string Habilidades { get; set; }

        public Curriculum(int numCurriculum, Usuario pUsuario, string pTitulo, string pExperiencia, string pEducacion, string pHabilidades)
        {
            Num_Curriculum = numCurriculum;
            ID_Usuario = pUsuario;
            Titulo = pTitulo;
            Experiencia = pExperiencia;
            Educacion = pEducacion;
            Habilidades = pHabilidades;
        }
    }
}
