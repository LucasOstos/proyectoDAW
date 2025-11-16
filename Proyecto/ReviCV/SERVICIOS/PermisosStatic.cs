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
        public const string pSysAdmin = "SysAdmin";

        /// <summary> Rol de Administrados. </summary>
        public const string pAdmin = "Administrador";

        /// <summary> Rol de Webmaster </summary>
        public const string pWeb = "Webmaster";

        /// <summary> Rol de Usuario </summary>
        public const string pUsuario = "Webmaster";

        /// <summary> Permiso para acceder a la bitacora. </summary>
        public const string pAccesoBitacora = "Acceso Bitácora";

        /// <summary> Permiso para acceder a la gestión de usuarios. </summary>
        public const string pGestionUsuarios = "Gestión de Usuarios";

        /// <summary> Permiso para acceder a la integridad de tablas. </summary>
        public const string pAccesoIntegridad = "Acceso a Integridad";

        /// <summary> Permiso para acceder al menú de administrador. </summary>
        public const string pAccesoMenuAdmin = "Acceso a Menú de Administrador";

        /// <summary> Permiso para acceder al menú de webmaster. </summary>
        public const string pAccesoMenuWebmaster = "Acceso a Menú de Webmaster";

        /// <summary> Permiso para acceder a la sección de backup y restore. </summary>
        public const string pAccesoBackupRestore = "Acceso a Backup y Restore";

        public const string pEvaluarCV = "Evaluar CV";

        public const string pGestionPermisos = "Gestión de Permisos";

        public const string pGestionRubrosIdiomas = "Gestión de Rubros e Idiomas";

        public const string pVerResenias = "Ver Reseñas";
    }
}
