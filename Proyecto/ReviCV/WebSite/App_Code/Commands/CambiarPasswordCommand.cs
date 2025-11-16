using BLL;
using System;

/// <summary>
/// Comando para cambiar la contraseña con validación y alertas sin redirección.
/// </summary>
public class CambiarPasswordCommand : ICommand
{
    private readonly int dni;
    private readonly string nuevaPassword;
    private readonly string confirmarPassword;

    private readonly GestorUsuario gestorUsuario;

    public CambiarPasswordCommand(int dni, string nuevaPassword, string confirmarPassword)
    {
        this.dni = dni;
        this.nuevaPassword = nuevaPassword;
        this.confirmarPassword = confirmarPassword;
        this.gestorUsuario = new GestorUsuario();
    }

    public string Ejecutar()
    {
        if (!gestorUsuario.ValidarContrasenia(nuevaPassword))
        {
            return GenerarScriptSweetAlert(
                "Oops...",
                "El formato de la contraseña es incorrecto",
                "error"
            );
        }

        if (nuevaPassword != confirmarPassword)
        {
            return GenerarScriptSweetAlert(
                "Oops...",
                "Las contraseñas no coinciden.",
                "error"
            );
        }

        gestorUsuario.CambiarPassword(dni, nuevaPassword);
        return GenerarScriptSweetAlert(
            "¡Actualizaste tu contraseña!",
            "Contraseña cambiada con éxito.",
            "success"
        );
    }

    private string GenerarScriptSweetAlert(string titulo, string texto, string icono)
    {
        return $@"
document.addEventListener('DOMContentLoaded', function() {{
    if (typeof Swal !== 'undefined') {{
        Swal.fire({{
            title: '{titulo}',
            text: '{texto}',
            icon: '{icono}',
            confirmButtonText: 'Ok',
            backdrop: true,
            allowOutsideClick: false,
            allowEscapeKey: false,
            customClass: {{ container: 'swal-container-fix' }}
        }});
    }}
}});";
    }
}
