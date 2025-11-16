using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SERVICIOS
{
    public static class PermisosStatic
    {
        /// <summary> Acceso irrestricto. Se saltea todas las validaciones. </summary>
        public const string rAdmin = "Administrador";

        /// <summary> Permiso para acceder a la bitacora. </summary>
        public const string pAccesoBitacora = "AccesoBitacora";

        /// <summary> Permiso para acceder a la gestión de usuarios. </summary>
        public const string pGestionUsuarios = "GestionUsuarios";

        /// <summary> Permiso para acceder a la integridad de tablas. </summary>
        public const string pIntegridadTablas = "AccederIntegridad";

        /// <summary> Permiso para acceder al menú de administrador. </summary>
        public const string pAccesoMenuAdmin = "AccesoMenuAdmin";

        /// <summary> Permiso para acceder al menú de webmaster. </summary>
        public const string pAccesoMenuWB = "AccesoMenuWB";

        /// <summary> Permiso para acceder a la sección de backup y restore. </summary>
        public const string pAccesoBackupRestore = "AccesoBackupRestore";
    }
}
