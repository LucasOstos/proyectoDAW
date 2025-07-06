using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTIDADES;
using DAL;
using SERVICIOS;

namespace BLL
{
    public class GestorCurriculum
    {
        public void GuardarCurriculum(Curriculum pCurriculum)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.GuardarCurriculum(pCurriculum);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            //gestorIntegridad.GuardarIntegridad(TablasBD.Curriculum);
        }

        public Curriculum ObtenerCurriculumPorID(int id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.ObtenerCurriculumPorID(id);
        }

        public Curriculum ObtenerCurriculumFiltrado(string rubro, string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.ObtenerCurriculumFiltrado(rubro, idioma);
        }



        //Se obtienen los idiomas y rubros desde el gestor de Curriculums ya que solo son tablas que afectan a los mismos
        public Dictionary<int, string> ObtenerIdiomas()
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.ObtenerIdiomas();
        }

        public Dictionary<int, string> ObtenerRubros()
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.ObtenerRubros();
        }
        public void AltaIdioma(string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.AltaIdioma(idioma);
        }
        public void BajaIdioma(string id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.BajaIdioma(id);
        }
        public void ModificarIdioma(string id, string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.ModificarIdioma(id, idioma);
        }
        public void AltaRubro(string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.AltaRubro(idioma);
        }
        public void BajaRubro(string id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.BajaRubro(id);
        }
        public void ModificarRubro(string id, string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.ModificarRubro(id, idioma);
        }
    }
}
