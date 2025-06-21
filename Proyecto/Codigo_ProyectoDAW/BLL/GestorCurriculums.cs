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
    }
}
