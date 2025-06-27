using BE;
using BLL;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Sesion.Instancia.IsLogueado())
            {
                Usuario u = GestorUsuario.Instancia.ObtenerUsuario(tbNombreUsuario.Text);
                if (u != null)
                {
                    if (Sesion.Instancia.Verificar(u.NombreUsuario, tbContraseña.Text))
                    {
                        Sesion.Instancia.LogIn(u);
                        GuardarSession(u);
                        Response.Redirect("Probando Session.aspx");
                        labelErrores.ForeColor = System.Drawing.Color.Green; labelErrores.Text = "Bien virgo";
                    }
                    else { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "Credenciales incorrectas"; }
                }
                else { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "No existe el usuario"; }
            }
            else { labelErrores.ForeColor = System.Drawing.Color.Orange; labelErrores.Text = "Ya hay una sesión iniciada"; }
        }
        catch { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "Tiempo de espera agotado."; }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Sesion.Instancia.LogOut();
        Session["username"] = null;
        Response.Redirect("Sign_Up.aspx");
    }
    public void GuardarSession(Usuario u)
    {
        Session["username"] = $"{u.NombreUsuario}";
        Session["Rol"] = $"{u.Rol}";
        Session["DNI"] = $"{u.DNI}";
        Session["Nombre"] = $"{u.Nombre}";
        Session["Apellido"] = $"{u.Apellido}";
        Session["Mail"] = $"{u.Email}";
        Session["Passw"] = $"{u.Password}";
    }
}