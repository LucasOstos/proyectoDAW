using ENTIDADES;
using SERVICIOS;
using System.Web;

public class LogoutCommand : ICommand
{
    private readonly Usuario usuario;
    public LogoutCommand(Usuario usuario)
    {
        this.usuario = usuario;
    }

    public string Ejecutar()
    {
        if (usuario.Rol != PermisosStatic.pUsuario)
        {
            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.GuardarLogBitacora("Logout", usuario.NombreUsuario);
        }
        HttpContext.Current.Session.Clear();
        return "window.location.href='LandingPage.aspx';";
    }
}
