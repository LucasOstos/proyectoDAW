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
        if (!Sesion.Instancia.IsLogueado())
        {
            Usuario u = GestorUsuario.Instancia.ObtenerUsuario(tbNombreUsuario.Text);
            if (u != null)
            {
                if(Sesion.Instancia.Verificar(u.NombreUsuario, tbContraseña.Text))
                {
                    Sesion.Instancia.LogIn(u);
                    labelErrores.ForeColor = System.Drawing.Color.Green; labelErrores.Text = "Bien virgo";
                }
                else { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "Credenciales incorrectas"; }
            }
            else { labelErrores.ForeColor = System.Drawing.Color.Red; labelErrores.Text = "No existe el usuario"; }
        }
        else { labelErrores.ForeColor = System.Drawing.Color.Orange; labelErrores.Text = "Ya hay una sesión iniciada"; }
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Sesion.Instancia.LogOut();
    }
}