using BLL;
using ENTIDADES;
using SERVICIOS.Traducciones;
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
            string text = TraductorDAL.TranslatorInstance.Traducir("alertTodosCampos");
            return GenerarScriptSweetAlert("Oops...", text, "error");
        }

        GestorUsuario gestor = new GestorUsuario();
        gestor.ModificarUsuario(usuario);

        string title2 = TraductorDAL.TranslatorInstance.Traducir("datosActualizados");
        string text2 = TraductorDAL.TranslatorInstance.Traducir("CambiosExito");
        return GenerarScriptSweetAlert(title2, text2, "success");
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
