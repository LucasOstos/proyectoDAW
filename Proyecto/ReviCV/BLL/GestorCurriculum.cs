using DAL;
using ENTIDADES;
using ENTIDADES.Tecnico;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GestorCurriculum
    {
        public void GuardarCurriculum(Curriculum pCurriculum)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            pCurriculum.ArchivoCV = EncriptadorAES256.Encrypt(pCurriculum.ArchivoCV);
            int id = curriculumDAL.GuardarCurriculum(pCurriculum);
            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Curriculum, id);
        }

        public Curriculum ObtenerCurriculumPorID(int id)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
             Curriculum cv = curriculumDAL.ObtenerCurriculumPorID(id);
            cv.ArchivoCV = EncriptadorAES256.Decrypt(cv.ArchivoCV);
            return cv;
        }

        public Curriculum ObtenerCurriculumFiltrado(string rubro, string idioma)
        {
            CurriculumDAL curriculumDAL = new CurriculumDAL();
            Curriculum cv = curriculumDAL.ObtenerCurriculumFiltrado(rubro, idioma);
            cv.ArchivoCV = EncriptadorAES256.Decrypt(cv.ArchivoCV);
            return cv;
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
            List<Curriculum> cvs = curriculumDAL.ObtenerCurriculumsPorUsuario(nombreUsuario);
            foreach(Curriculum cv in cvs)
            {                
                cv.ArchivoCV = EncriptadorAES256.Decrypt(cv.ArchivoCV);
            }
            return cvs;
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
