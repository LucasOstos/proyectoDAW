using SERVICIOS.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class AccesoHelper
{
    /// <summary>
    /// Valida acceso según rol y permiso requerido.
    /// </summary>
    /// <param name="rolDeSession">Rol del usuario en sesión.</param>
    /// <param name="permisoRequerido">Permiso a validar; null si no se requiere.</param>
    /// <param name="rolMinimo">Nombre de rol mínimo requerido; null si no se requiere.</param>
    /// <returns>True si el usuario tiene acceso; false si no.</returns>
    public static bool ValidarAcceso(PermisoCompuesto rolDeSession, string permisoRequerido = null, string rolMinimo = null)
    {
        if (rolDeSession == null) return false; // sin rol → acceso denegado

        if(rolDeSession.getNombre() == "Administrador") return true;

        if (!string.IsNullOrEmpty(rolMinimo) && !rolDeSession.getNombre().Equals(rolMinimo, StringComparison.OrdinalIgnoreCase)) return false; // rol insuficiente

        if (permisoRequerido == null) return true; // no se requiere permiso específico

        return GestorPermisos.TienePermiso(rolDeSession, permisoRequerido); // validar permiso
    }
}
