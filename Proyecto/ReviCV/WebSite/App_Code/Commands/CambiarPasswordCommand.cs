using BLL;
using SERVICIOS.Traducciones;
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
            string text = TraductorDAL.TranslatorInstance.Traducir("formatoContraseniaIncorrecto");
            return GenerarScriptSweetAlert(
                "Oops...",
                text,
                "error"
            );
        }

        if (nuevaPassword != confirmarPassword)
        {
            string text2 = TraductorDAL.TranslatorInstance.Traducir("contraseniasNoCoinciden");
            return GenerarScriptSweetAlert(
                "Oops...",
                text2,
                "error"
            );
        }
        string titlel3 = TraductorDAL.TranslatorInstance.Traducir("contraseniaActualizada");
        string text3 = TraductorDAL.TranslatorInstance.Traducir("contraseniaCambioExitoso");
        gestorUsuario.CambiarPassword(dni, nuevaPassword);
        return GenerarScriptSweetAlert(
            titlel3,
            text3,
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
