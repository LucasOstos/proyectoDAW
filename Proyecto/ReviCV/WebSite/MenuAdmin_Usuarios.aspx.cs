using System;
using System.Web.UI;
using System.Collections.Generic;
using BE;
using BLL;
using System.Web.UI.WebControls;
using SERVICIOS;

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
    private void CargarUsuariosFiltrados(string dni, string username, string mail, string rol)
    {
        GestorUsuario gestorUsuarios = new GestorUsuario();
        List<Usuario> usuarios = gestorUsuarios.FiltrarUsuarios(dni, username, mail, rol);
        gvUsuarios.DataSource = usuarios;
        gvUsuarios.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        CargarUsuarios();
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Encriptador encriptador = new Encriptador();
        if (int.TryParse(txtDni.Text, out int x) && txtNombre.Text != "" && txtApellido.Text != "" && txtUsername.Text != "" && txtEmail.Text != "" && ddlRol.SelectedValue != "")
        {
            Usuario usuario = new Usuario
                    (
                        int.Parse(txtDni.Text),
                        txtNombre.Text,
                        txtApellido.Text,
                        txtUsername.Text,
                        encriptador.EncriptarIrreversible(txtDni.Text),
                        txtEmail.Text,
                        ddlRol.SelectedValue
                    );
            GestorUsuario gestorUsuarios = new GestorUsuario();
            gestorUsuarios.InsertarUsuario(usuario);
            CargarUsuarios();
        }
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        GestorUsuario gestorUsuarios = new GestorUsuario();
        CargarUsuariosFiltrados(txtFiltroDni.Text, txtFiltroUsername.Text, txtFiltroEmail.Text, ddlFiltroRol.SelectedValue.ToString());
    }

    protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow fila = gvUsuarios.SelectedRow;
        txtDni.Text = fila.Cells[1].Text;
        txtNombre.Text = fila.Cells[2].Text;
        txtApellido.Text = fila.Cells[3].Text;
        txtUsername.Text = fila.Cells[4].Text;
        txtEmail.Text = fila.Cells[5].Text;
        ddlRol.SelectedValue = fila.Cells[6].Text;
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtDni.Text, out int x) && txtNombre.Text != "" && txtApellido.Text != "" && txtUsername.Text != "" && txtEmail.Text != "" && ddlRol.SelectedValue != "")
        {
            Usuario usuario = new Usuario
                                        (
                                            int.Parse(txtDni.Text),
                                            txtNombre.Text,
                                            txtApellido.Text,
                                            txtUsername.Text,
                                            txtDni.Text,
                                            txtEmail.Text,
                                            ddlRol.SelectedValue
                                        );
            GestorUsuario gestorUsuario = new GestorUsuario();
            gestorUsuario.ModificarUsuario(usuario);
            CargarUsuarios();
            LimpiarTxt();
        }
            
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if(txtDni.Text != "")
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            gestorUsuario.EliminarUsuario(txtDni.Text.ToString());
            CargarUsuarios();
            LimpiarTxt();
        }
    }
    public void LimpiarTxt()
    {
        txtApellido.Text = "";
        txtDni.Text = "";
        txtEmail.Text = "";
        txtNombre.Text = "";
        txtUsername.Text = "";
        ddlRol.SelectedValue = "";
    }
    public void LimpiarFiltros()
    {
        txtFiltroEmail.Text = "";
        txtFiltroDni.Text = "";
        txtFiltroUsername.Text = "";
        ddlFiltroRol.SelectedValue = "";
        CargarUsuarios();
    }

    protected void btnLimpiar_Click(object sender, EventArgs e)
    {
        LimpiarFiltros();
    }
}
