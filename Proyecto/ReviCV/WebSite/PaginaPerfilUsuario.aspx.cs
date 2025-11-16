using BLL;
using ENTIDADES;
using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class PaginaPerfilUsuario : System.Web.UI.Page, IObserver
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AccesoHelper.ValidarAcceso(Session["Rol"] as PermisoCompuesto))
        {
            Response.Redirect("LandingPage.aspx");
            return;
        }

        if (!IsPostBack)
        {
            TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD((Session["Usuario"] as Usuario).Idioma.ToString());
            Actualizar();
            SettearHiddenFields();
            CargarIdiomas();
            CargarRubros();
            CargarIdiomas2();

            ddlIdioma.SelectedValue = (Session["Usuario"] as Usuario).Idioma.ToString();
        }
    }

    private void CargarIdiomas2()
    {
        ddlIdioma.Items.Clear();
        ddlIdioma.Items.Add(new ListItem("Español", "Español"));
        ddlIdioma.Items.Add(new ListItem("Inglés", "Ingles"));
        ddlIdioma.Items.Add(new ListItem("Portugués", "Portugues"));
    }

    //Esto se ejecuta antes que Page_Load y sirve para crear controles dinamicos como los de los cvs que tiene cada usuario - Matt
    protected void Page_Init(object sender, EventArgs e)
    {
        // Validación temprana
        if (Session["Rol"] == null || string.IsNullOrEmpty(Session["Usuario"]?.ToString())) Response.Redirect("LandingPage.aspx");

        CargarCurriculums();
    }
    private void SettearHiddenFields()
    {
        string nombreUsuario = (Session["Usuario"] as Usuario).NombreUsuario as string;

        if (!string.IsNullOrEmpty(nombreUsuario))
        {
            GestorUsuario gestorUsuario = new GestorUsuario();
            Usuario usuario = gestorUsuario.ObtenerUsuario(nombreUsuario);

            if (usuario != null)
            {
                username.Text = usuario.NombreUsuario;
                firstName.Text = usuario.Nombre;
                lastName.Text = usuario.Apellido;
                email.Text = usuario.Email;
                hfOriginalUsername.Value = usuario.NombreUsuario;
                hfOriginalFirstName.Value = usuario.Nombre;
                hfOriginalLastName.Value = usuario.Apellido;
                hfOriginalEmail.Value = usuario.Email;
                hfOriginalDNI.Value = usuario.DNI.ToString();
                hfOriginalRol.Value = usuario.Rol;
                lblNombrePerfil.Text = usuario.Nombre + " " + usuario.Apellido;
                lblUsuarioPerfil.Text = usuario.NombreUsuario;
            }
        }
    }
    public void Actualizar()
    {
        RecorrerControles(this);
    }
    void RecorrerControles(Control controlPadre)
    {
        foreach (Control c in controlPadre.Controls)
        {
            // LINKBUTTON
            if (c is LinkButton lbl && lbl.Attributes["data-key"] != null)
            {
                string clave = lbl.Attributes["data-key"];
                lbl.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // BUTTON ASP.NET
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // HTML BUTTON  ← ESTE DEBE IR ANTES DE HtmlGenericControl
            else if (c is HtmlButton btn2 && btn2.Attributes["data-key"] != null)
            {
                string clave = btn2.Attributes["data-key"];
                btn2.InnerText = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // TEXTBOX
            else if (c is TextBox tb && tb.Attributes["data-key"] != null)
            {
                string clave = tb.Attributes["data-key"];
                tb.Attributes["placeholder"] = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // DROPDOWNLIST
            else if (c is DropDownList ddl && ddl.Attributes["data-key"] != null)
            {
                string clave = ddl.Attributes["data-key"];
                ddl.Attributes["placeholder"] = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // HTML GENERIC CONTROL
            else if (c is HtmlGenericControl html && html.Attributes["data-key"] != null)
            {
                string clave = html.Attributes["data-key"];
                html.InnerHtml = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            if (c.HasControls())
                RecorrerControles(c);
        }
    }

    void RecorrerControles2(Control controlPadre)
    {
        foreach (Control c in controlPadre.Controls)
        {
            if (c is LinkButton lbtn && lbtn.Attributes["data-key"] != null)
            {
                string clave = lbtn.Attributes["data-key"];
                lbtn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is Label lbl && lbl.Attributes["data-key"] != null)
            {

                string clave = lbl.Attributes["data-key"];
                if (clave != "VerReseñas") lbl.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
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
                html.InnerText = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // Llamada recursiva para procesar controles anidados (Esto estaba bien)
            if (c.HasControls())
            {
                RecorrerControles2(c);
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Usuario usuario = new Usuario
        {
            NombreUsuario = username.Text,
            Nombre = firstName.Text,
            Apellido = lastName.Text,
            Email = email.Text,
            Rol = hfOriginalRol.Value, 
            DNI = int.Parse(hfOriginalDNI.Value),   
            Idioma = ddlIdioma.Text
        };

        var command = new ActualizarDatosUsuarioCommand(usuario, "PaginaPerfilUsuario.aspx");

        string script = command.Ejecutar();

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "SwalActualizarDatos",
            script,
            true
        );
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        username.Text = hfOriginalUsername.Value;
        firstName.Text = hfOriginalFirstName.Value;
        lastName.Text = hfOriginalLastName.Value;
        email.Text = hfOriginalEmail.Value;
    }

    protected void btnCambiarPassword_Click(object sender, EventArgs e)
    {
        var command = new CambiarPasswordCommand(
            int.Parse(hfOriginalDNI.Value),
            newPassword.Text,
            confirmPassword.Text
        );

        string script = command.Ejecutar();

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "SwalPassword",
            script,
            true
        );
    }

    protected void btnSubirArchivo_Click(object sender, EventArgs e)
    {
        if (!fileUpload.HasFile)
        {
            string script = @"
            Swal.fire({
                title: 'Oops...',
                text: 'No se seleccionó ningún archivo.',
                icon: 'error',
                confirmButtonText: 'Ok'
            });";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "SwalCV", script, true);
            return;
        }

        byte[] archivoBytes;
        using (var ms = new MemoryStream())
        {
            fileUpload.PostedFile.InputStream.CopyTo(ms);
            archivoBytes = ms.ToArray();
        }

        var idioma = (int.Parse(ddlIdiomas.SelectedValue), ddlIdiomas.SelectedItem.Text);
        var rubro = (int.Parse(ddlRubros.SelectedValue), ddlRubros.SelectedItem.Text);

        var command = new SubirCVCommand(
            (Session["Usuario"] as Usuario).NombreUsuario,
            archivoBytes,
            hfNombreArchivo.Value,
            idioma,
            rubro
        );

        string scriptResultado = command.Ejecutar();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SwalCV", scriptResultado, true);
    }

    private void CargarIdiomas()
    {
        GestorCurriculum gCurriculums = new GestorCurriculum();
        var idiomas = gCurriculums.ObtenerIdiomas();

        ddlIdiomas.DataSource = idiomas;
        ddlIdiomas.DataTextField = "Value";
        ddlIdiomas.DataValueField = "Key";
        ddlIdiomas.DataBind();

        ddlIdiomas.Items.Insert(0, new ListItem(TraductorDAL.TranslatorInstance.Traducir("IdiomaDelCv"), ""));
        ddlIdiomas.Items[0].Attributes.Add("disabled", "true");
        ddlIdiomas.Items[0].Selected = true;
    }

    private void CargarRubros()
    {
        GestorCurriculum gCurriculums = new GestorCurriculum();
        var rubros = gCurriculums.ObtenerRubros();

        ddlRubros.DataSource = rubros;
        ddlRubros.DataTextField = "Value";
        ddlRubros.DataValueField = "Key";
        ddlRubros.DataBind();

        ddlRubros.Items.Insert(0, new ListItem(TraductorDAL.TranslatorInstance.Traducir("RubroDelCv"), ""));
        ddlRubros.Items[0].Attributes.Add("disabled", "true");
        ddlRubros.Items[0].Selected = true;
    }

    private void CargarCurriculums()
    {
        string nombreUsuario = (Session["Usuario"] as Usuario).NombreUsuario?.ToString();
        if (string.IsNullOrEmpty(nombreUsuario)) return;

        GestorCurriculum gestor = new GestorCurriculum();
        var cvs = gestor.ObtenerCurriculumsPorUsuario(nombreUsuario);

        phCurriculums.Controls.Clear();

        var factory = new CurriculumBasicoFactory(btnEliminar_Click, BtnVerResenias_Command);

        foreach (var cv in cvs)
        {
            var contenedor = factory.CrearPanel(cv);
            phCurriculums.Controls.Add(contenedor);
        }
    }


    protected void BtnVerResenias_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect($"VerResenias.aspx?id={e.CommandArgument}");
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        int idCV = int.Parse(((LinkButton)sender).CommandArgument);
        var command = new EliminarCVCommand(idCV);
        string scriptResultado = command.Ejecutar();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "SwalEliminarCV", scriptResultado, true);
        CargarCurriculums();
    }


    protected void btnVolverPrincipal_Click(object sender, EventArgs e)
    {
        var rol = Session["Rol"] as PermisoCompuesto;
        if (AccesoHelper.ValidarAcceso(rol, PermisosStatic.pAccesoMenuAdmin))
        {
            Response.Redirect("MenuAdmin.aspx", true);
            return;
        }
        if (AccesoHelper.ValidarAcceso(rol, PermisosStatic.pAccesoMenuWebmaster))
        {
            Response.Redirect("WebMaster_Menu.aspx");
            return;
        }
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        var command = new LogoutCommand(Session["Usuario"] as Usuario);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Logout", command.Ejecutar(), true);
    }
}