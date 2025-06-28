using System;
using System.Web.UI;
using System.Collections.Generic;
using BE;
using BLL;

public partial class MenuAdmin_Usuarios : Page
{


    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin.aspx");
    }

    protected void btnUsuarios_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin_Usuarios.aspx");
    }

    protected void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnVolverALanding_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandingPage.aspx");
    }

    private void CargarUsuarios()
    {
        GestorUsuario gestorUsuarios = new GestorUsuario();
        List<Usuario> usuarios = gestorUsuarios.ObtenerTodosUsuarios();
        gvUsuarios.DataSource = usuarios;
        gvUsuarios.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CargarUsuarios();
    }
}
