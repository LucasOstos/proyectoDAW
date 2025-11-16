using BLL;

public class EliminarCVCommand : ICommand
{
    private readonly int idCV;

    public EliminarCVCommand(int idCV)
    {
        this.idCV = idCV;
    }

    public string Ejecutar()
    {
        GestorCurriculum gestor = new GestorCurriculum();
        gestor.EliminarCurriculum(idCV);

        return @"
Swal.fire({
    title: 'CV eliminado',
    text: 'El CV se eliminó correctamente',
    icon: 'success',
    confirmButtonText: 'Ok'
}).then(() => {
    location.reload();
});";
    }
}
