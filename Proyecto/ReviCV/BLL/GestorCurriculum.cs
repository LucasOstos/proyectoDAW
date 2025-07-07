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
            int id = curriculumDAL.GuardarCurriculum(pCurriculum);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Curriculum, id);
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
        public void AltaIdioma(string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            int id = curriculumDAL.AltaIdioma(idioma);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Idioma, id);
        }
        public void BajaIdioma(int id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.BajaIdioma(id);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.GuardarIntegridadTabla(TablasBD.Idioma);
        }
        public bool IdiomaEnUso(int id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.IdiomaEnUso(id);
        }
        public void ModificarIdioma(int id, string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.ModificarIdioma(id, idioma);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Idioma, id);
        }



        public Dictionary<int, string> ObtenerRubros()
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.ObtenerRubros();
        }
        public void AltaRubro(string rubro)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            int id = curriculumDAL.AltaRubro(rubro);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Rubro, id);
        }
        public void BajaRubro(int id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.BajaRubro(id);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.GuardarIntegridadTabla(TablasBD.Rubro);
        }
        public bool RubroEnUso(int id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.RubroEnUso(id);
        }
        public void ModificarRubro(int id, string rubro)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.ModificarRubro(id, rubro);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Rubro, id);
        }

        public List<Curriculum> ObtenerCurriculumsPorUsuario(string nombreUsuario)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            return curriculumDAL.ObtenerCurriculumsPorUsuario(nombreUsuario);
        }

        public void EliminarCurriculum(int idCV)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            curriculumDAL.EliminarCurriculum(idCV);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.GuardarIntegridadTabla(TablasBD.Curriculum);
        }
    }
}
