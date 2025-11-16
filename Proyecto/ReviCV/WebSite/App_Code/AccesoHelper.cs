using SERVICIOS.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class AccesoHelper
{
    public static bool ValidarAcceso(PermisoCompuesto rolDeSession, string permisoRequerido, string rolMinimo = null)
    {
        // No hay rol en sesión → acceso denegado
        if (rolDeSession == null) return false;

        // Verificar rol mínimo
        if (!string.IsNullOrEmpty(rolMinimo) && !rolDeSession.getNombre().Equals(rolMinimo, StringComparison.OrdinalIgnoreCase))
        {
            return false; // rol no permitido
        }

        // Verificar permiso requerido
        if (!string.IsNullOrEmpty(permisoRequerido) && !GestorPermisos.TienePermiso(rolDeSession, permisoRequerido))
        {
            return false; // permiso no concedido
        }

        // Todo ok → acceso permitido
        return true;
    }
}
