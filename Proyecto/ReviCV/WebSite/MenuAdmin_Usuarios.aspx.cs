using BLL;
using ENTIDADES;
using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;
using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class MenuAdmin_Usuarios : Page, IObserver
{
    public void Actualizar()
    {
        RecorrerControles(this);
    }
    void RecorrerControles(Control controlPadre)
    {
        foreach (Control c in controlPadre.Controls)
        {
            if (c is LinkButton lbtn && lbtn.Attributes["data-key"] != null)
            {
                string clave = lbtn.Attributes["data-key"];
                string traduccion = TraductorDAL.TranslatorInstance.Traducir(clave);

                string html = lbtn.Text;
                string icono = "";

                if (html.Contains("</i>"))
                {
                    int finIcono = html.IndexOf("</i>") + 4;
                    icono = html.Substring(0, finIcono);
                }

                lbtn.Text = $"{icono} {traduccion}";
            }
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is TextBox tb && tb.Attributes["data-key"] != null)
            {
                string clave = tb.Attributes["data-key"];
                tb.Attributes["placeholder"] = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is DropDownList ddl && ddl.Attributes["data-key"] != null)
            {
                string clave = ddl.Attributes["data-key"];
                ddl.Attributes["placeholder"] = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is HtmlGenericControl html && html.Attributes["data-key"] != null)
            {
                string clave = html.Attributes["data-key"];
                string htmlAnterior = html.InnerHtml;

                string icono = "";
                if (htmlAnterior.Contains("</i>"))
                {
                    int finIcono = htmlAnterior.IndexOf("</i>") + 4;
                    icono = htmlAnterior.Substring(0, finIcono);
                }

                string traduccion = TraductorDAL.TranslatorInstance.Traducir(clave);
                html.InnerHtml = $"{icono} {traduccion}";
            }
            if (c.HasControls())
                RecorrerControles(c);
        }
    }

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
        var command = new LogoutCommand(Session["Usuario"] as Usuario);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Logout", command.Ejecutar(), true);
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
        if (!AccesoHelper.ValidarAcceso(Session["Rol"] as PermisoCompuesto, PermisosStatic.pGestionUsuarios))
        {
            Response.Redirect("LandingPage.aspx");
            return;
        }

        if (Application["EstadoBD"] is bool bdOk && !bdOk)
        {
            Response.Redirect("AvisoErrorBD.aspx");
            return;
        }

        if (!IsPostBack)
        {
            CargarUsuarios();
            CargarRoles();
            TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD((Session["Usuario"] as Usuario).Idioma.ToString());
            Actualizar();
        }
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Encriptador encriptador = new Encriptador();
        GestorUsuario gestorUsuarios = new GestorUsuario();
        if (int.TryParse(txtDni.Text, out int x) && !string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(ddlRol.SelectedValue))
        {
            if(!gestorUsuarios.UsuarioExistente(int.Parse(txtDni.Text), txtUsername.Text))            
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

                gestorUsuarios.InsertarUsuario(usuario);
                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se agregó el usuario {usuario.DNI}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());
                CargarUsuarios();
                LimpiarTxt();
            }            
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
        GestorUsuario gestorUsuario = new GestorUsuario();
        if (int.TryParse(txtDni.Text, out int x) && !string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtApellido.Text) && !string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(ddlRol.SelectedValue))
        {
            if(!gestorUsuario.UsernameRepetido(txtUsername.Text))
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


                gestorUsuario.ModificarUsuario(usuario);
                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se modificó el usuario {usuario.DNI}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());
                CargarUsuarios();
                LimpiarTxt();
            }            
        }
    }
    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        if(txtDni.Text != "")
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            gestorUsuario.EliminarUsuario(int.Parse(txtDni.Text));
            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.GuardarLogBitacora($"Se eliminó el usuario {txtDni.Text}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());
            CargarUsuarios();
            LimpiarTxt();
      
        }
    }
    protected void btnSerializar_Click(object sender, EventArgs e)
    {
        GestorUsuario gestorUsuario = new GestorUsuario();
        Usuario usuarioSerializado = gestorUsuario.ObtenerUsuario(txtUsername.Text);
        ServicioWeb sw = new ServicioWeb();
        sw.SerializarUsuario(usuarioSerializado);
    }
    protected void btnDeserializar_Click(object sender, EventArgs e)
    {        
        ServicioWeb sw = new ServicioWeb();
        Usuario usuario = sw.DeserializarUsuario(fuArchivo.FileName);
        CargarUsuarioDeserializado(usuario);
    }
    private void CargarUsuarioDeserializado(Usuario usuario)
    {
        if (usuario != null)
        {
            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario);
            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();
        }
    }
    public void LimpiarTxt()
    {
        txtApellido.Text = "";
        txtDni.Text = "";
        txtEmail.Text = "";
        txtNombre.Text = "";
        txtUsername.Text = "";
        ddlRol.SelectedValue = null;
    }
    public void CancelarModificacion()
    {
        btnAgregar.Enabled = true;
        txtApellido.Text = "";
        txtDni.Text = "";
        txtEmail.Text = "";
        txtNombre.Text = "";
        txtUsername.Text = "";
        ddlRol.SelectedValue = null;
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
        new ListItem("Administrador", "Administrador"),
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

    protected void btnCancelarEleccion_Click(object sender, EventArgs e)
    {
        CancelarModificacion();
    }




    protected void btnVerPerfilUsuario_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaginaPerfilUsuario.aspx");
    }

    protected void ddlRol_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnPermisos_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin_Permisos.aspx");
    }
}
