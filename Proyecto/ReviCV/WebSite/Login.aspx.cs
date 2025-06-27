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
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["username"] == null)
            {
                GestorUsuario gestorUsuario = new GestorUsuario();
                Usuario u = gestorUsuario.ObtenerUsuario(tbNombreUsuario.Text);
                if (u != null)
                {
                    Validador validador = new Validador();
                    if (validador.Verificar(u.NombreUsuario, Encriptador.Instancia.EncriptarContraseña(tbContraseña.Text)))
                    {
                        Session["username"] = $"{u.NombreUsuario}";
                        Session["Rol"] = $"{u.Rol}";
                        GestorBitacora.Instancia.GuardarLog("Login", Session["username"].ToString());
                        Response.Redirect("LandingPage.aspx");
                    }
                    else { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "Credenciales incorrectas"; }
                }
                else { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "No existe el usuario"; }
            }
            else { labelErrores.ForeColor = System.Drawing.Color.Orange; labelErrores.Text = "Ya hay una sesión iniciada"; }
        }
        catch { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "Tiempo de espera agotado."; }
    }

    protected void btnSignUp_Click(object sender, EventArgs e)
    {
        Response.Redirect("Sign_Up.aspx");
    }
}