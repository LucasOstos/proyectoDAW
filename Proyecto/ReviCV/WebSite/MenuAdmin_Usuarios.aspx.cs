using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using BE;
using BLL;
using System.Web.UI.WebControls;
using SERVICIOS;
using ENTIDADES;

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
        if (!IsPostBack)
        {
            CargarUsuarios();
            CargarRoles();
        }
    }


    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Encriptador encriptador = new Encriptador();
        if (int.TryParse(txtDni.Text, out int x) && !string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(ddlRol.SelectedValue))
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
            LimpiarTxt();
        }
    }


    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        GestorUsuario gestorUsuarios = new GestorUsuario();
        CargarUsuariosFiltrados(txtFiltroDni.Text, txtFiltroUsername.Text, txtFiltroEmail.Text, ddlFiltroRol.SelectedValue.ToString());
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        Encriptador encriptador = new Encriptador();
        if (int.TryParse(txtDni.Text, out int x) && !string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(ddlRol.SelectedValue))
        {
            Usuario usuario = new Usuario
            (
                int.Parse(txtDni.Text),
                txtNombre.Text,
                txtApellido.Text,
                txtUsername.Text,
                "",
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

    private void CargarRoles()
    {
        List<ListItem> roles = new List<ListItem>
    {
        new ListItem("Selecciona un rol", ""),
        new ListItem("Administrador", "Admin"),
        new ListItem("Webmaster", "Webmaster"),
        new ListItem("Usuario", "Usuario")
    };

        // Desactivar la opción por defecto
        roles[0].Attributes.Add("disabled", "true");
        roles[0].Selected = true;

        ddlRol.Items.Clear();
        ddlFiltroRol.Items.Clear();

        ddlRol.Items.AddRange(roles.ToArray());
        ddlFiltroRol.Items.Add(new ListItem("Todos los roles", ""));
        for (int i = 1; i < roles.Count; i++)
        {
            ddlFiltroRol.Items.Add(roles[i]);
        }
    }


    protected void btnBitacora_Click(object sender, EventArgs e)
    {

    }

    protected void btnRubrosIdiomas_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin_RubrosIdiomas.aspx");
    }
}
