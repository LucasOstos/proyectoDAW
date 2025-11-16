using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS.Permisos
{
    public class PermisoSimple : Permiso
    {
        public PermisoSimple(string nNombre) : base(nNombre) { }
        public override bool ContienePermiso(string nombrePermiso)
        => nombre == nombrePermiso;
    }
}
