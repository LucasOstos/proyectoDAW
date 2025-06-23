using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class GestorCurriculums
    {
        public void GuardarCurriculum(Curriculum pCurriculum)
        {
            CurriculumDAL.Instancia.GuardarCurriculum(pCurriculum);
        }

        public Curriculum ObtenerCurriculumPorID(int id)
        {
            return CurriculumDAL.Instancia.ObtenerCurriculumPorID(id);
        }

        public Curriculum ObtenerCurriculumFiltrado(string rubro, string idioma)
        {
            return CurriculumDAL.Instancia.ObtenerCurriculumFiltrado(rubro, idioma);
        }

        //Se obtienen los idiomas y rubros desde el gestor de Curriculums ya que solo son tablas que afectan a los mismos
        public Dictionary<int, string> ObtenerIdiomas()
        {
            return CurriculumDAL.Instancia.ObtenerIdiomas();
        }

        public Dictionary<int, string> ObtenerRubros()
        {
            return CurriculumDAL.Instancia.ObtenerRubros();
        }
    }
}
