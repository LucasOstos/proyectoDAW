using BLL;
using ENTIDADES;
using SERVICIOS.Traducciones;
using System;
using System.IO;
using System.Security.Policy;

public class SubirCVCommand : ICommand
{
    private readonly string nombreUsuario;
    private readonly byte[] archivoCV;
    private readonly string nombreArchivo;
    private readonly (int, string) idioma;
    private readonly (int, string) rubro;

    public SubirCVCommand(string nombreUsuario, byte[] archivoCV, string nombreArchivo, (int, string) idioma, (int, string) rubro)
    {
        this.nombreUsuario = nombreUsuario;
        this.archivoCV = archivoCV;
        this.nombreArchivo = nombreArchivo;
        this.idioma = idioma;
        this.rubro = rubro;
    }

    public string Ejecutar()
    {
        if (archivoCV == null || archivoCV.Length == 0)
        {
            string text = TraductorDAL.TranslatorInstance.Traducir("archivoNoSeleccionado");
            return GenerarScriptSweetAlert("Oops...", text, "error");
        }

        GestorCurriculum gestor = new GestorCurriculum();
        Curriculum cv = new Curriculum
        {
            Usuario = nombreUsuario,
            ArchivoCV = archivoCV,
            Nombre = nombreArchivo,
            Idioma = idioma,
            Rubro = rubro
        };
        gestor.GuardarCurriculum(cv);

        string title2 = TraductorDAL.TranslatorInstance.Traducir("cvSubido");
        string text2 = TraductorDAL.TranslatorInstance.Traducir("cvGuardado");
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
            window.location.href = 'PaginaPerfilUsuario.aspx';
        }});
    }} else {{
    }}
}});
";
    }

}
