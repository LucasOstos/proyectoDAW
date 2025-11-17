using BLL;
using SERVICIOS.Traducciones;

public class EliminarCVCommand : ICommand
{
    private readonly int idCV;

    public EliminarCVCommand(int idCV)
    {
        this.idCV = idCV;
    }

    public string Ejecutar()
    {
        string title = TraductorDAL.TranslatorInstance.Traducir("cvEliminado");
        string text = TraductorDAL.TranslatorInstance.Traducir("cvEliminadoCorrectamente");
        GestorCurriculum gestor = new GestorCurriculum();
        gestor.EliminarCurriculum(idCV);

        return $@"
Swal.fire({{title: {title},
    text: {text},
    icon: 'success',
    confirmButtonText: 'Ok'
        }}).then(() => {{
            window.location.href = 'PaginaPerfilUsuario.aspx';
        }});
    }} else {{
    }}
}});
";
    }
}
