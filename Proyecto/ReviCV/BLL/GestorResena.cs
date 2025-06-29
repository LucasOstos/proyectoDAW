using ENTIDADES;
using DAL;
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
            resenaDAL.GuardarResena(r);
        }
    }
}
