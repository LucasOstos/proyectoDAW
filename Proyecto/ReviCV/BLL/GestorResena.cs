using DAL;
using ENTIDADES;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GestorResena
    {
        public void GuardarResena(Resena r)
        {
            ResenaDAL resenaDAL = new ResenaDAL();
            int id =  resenaDAL.GuardarResena(r);

            GestorIntegridad gestorIntegridad = new GestorIntegridad();
            gestorIntegridad.ActualizarDVHRegistro(TablasBD.Resena, id);
        }
    }
}
