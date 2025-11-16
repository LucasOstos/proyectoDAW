using BLL;
using ENTIDADES;
using System;

public class ActualizarDatosUsuarioCommand : ICommand
{
    private readonly Usuario usuario;
    private readonly string url;

    public ActualizarDatosUsuarioCommand(Usuario usuario, string url)
    {
        this.usuario = usuario;
        this.url = url;
    }

    public string Ejecutar()
    {
        if (string.IsNullOrWhiteSpace(usuario.Nombre) ||
            string.IsNullOrWhiteSpace(usuario.Apellido) ||
            string.IsNullOrWhiteSpace(usuario.Email))
        {
            return GenerarScriptSweetAlert("Oops...", "Necesitas completar todos los campos.", "error");
        }

        GestorUsuario gestor = new GestorUsuario();
        gestor.ModificarUsuario(usuario);

        return GenerarScriptSweetAlert("¡Datos actualizados!", "Los cambios se guardaron con éxito.", "success");
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
        }}).then(() => {{
            window.location.href = '{url}';
        }});
    }} else {{
        window.location.href = '{url}';
    }}
}});
";
    }

}
