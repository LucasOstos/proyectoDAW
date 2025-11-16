using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;
using ENTIDADES;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MenuAdmin_Permisos : System.Web.UI.Page, IObserver
{
    private Lazy<GestorPermisos> _gestor = new Lazy<GestorPermisos>(() => new GestorPermisos());
    private GestorPermisos GP => _gestor.Value;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AccesoHelper.ValidarAcceso((Session["Rol"] as PermisoCompuesto), PermisosStatic.pGestionPermisos))
        {
            Response.Redirect("LandingPage.aspx");
            return;
        }

        if (Application["EstadoBD"] is bool bdOk && !bdOk)
        {
            Response.Redirect("AvisoErrorBD.aspx", true);
            return;
        }

        if (!IsPostBack)
        {
            CargarRolesYGrupos();
            CargarArbolPermisos();
            CargarPermisosAsignados();
            TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD("Ingles");
            //TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD((Session["Usuario"] as Usuario).Idioma);
            Actualizar();
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
            // LINKBUTTON con ícono FA
            if (c is LinkButton link && link.Attributes["data-key"] != null)
            {
                string clave = link.Attributes["data-key"];
                string textoTraducido = TraductorDAL.TranslatorInstance.Traducir(clave);

                string htmlActual = link.Text;

                string icono = "";
                if (htmlActual.Contains("</i>"))
                {
                    int finIcono = htmlActual.IndexOf("</i>") + 4;
                    icono = htmlActual.Substring(0, finIcono);
                }

                link.Text = $"{icono} {textoTraducido}";
            }

            // BUTTON estándar ASP
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // TEXTBOX placeholder
            else if (c is TextBox tb && tb.Attributes["data-key"] != null)
            {
                string clave = tb.Attributes["data-key"];
                tb.Attributes["placeholder"] = TraductorDAL.TranslatorInstance.Traducir(clave);
            }

            // HTML genérico
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

            // Recursividad
            if (c.HasControls())
            {
                RecorrerControles(c);
            }
        }
    }

    private void CargarRolesYGrupos()
    {
        List<Permiso> permisos = GP.ObtenerPermisos("Compuesto");
        permisos.RemoveAll(x => x.nombre == "SysAdmin");

        ddlRolesGrupos.DataSource = permisos.Select(p =>
            new
            {
                nombre = ((p as PermisoCompuesto).EsRol ? "ROL: " : "GRUPO: ") + p.nombre,
                valor = p.nombre
            }
        ).ToList();

        ddlRolesGrupos.DataTextField = "nombre";
        ddlRolesGrupos.DataValueField = "valor";
        ddlRolesGrupos.DataBind();

        ddlRolesGrupos.Items.Insert(0, new ListItem("-- Seleccione --", ""));
    }

    protected void ddlRolesGrupos_SelectedIndexChanged(object sender, EventArgs e)
    {
        bool haySeleccion = ddlRolesGrupos.SelectedIndex > 0;

        btnEliminar.Enabled = haySeleccion;
        btnModificarNombre.Enabled = haySeleccion;
        btnGuardarCambios.Enabled = haySeleccion;

        CargarPermisosAsignados();
    }

    private void CargarPermisosAsignados()
    {
        chkListPermisos.Items.Clear();
        var permisosSimples = GP.ObtenerPermisos("Todos excepto rol");

        foreach (var permiso in permisosSimples)
        {
            chkListPermisos.Items.Add(new ListItem(permiso.nombre, permiso.nombre));
        }

        string rolSeleccionado = ddlRolesGrupos.SelectedValue;
        List<PermisoCompuesto> RootsPermits = GP.ObtenerPermisosEnArbol();
        Permiso selected = RootsPermits.Find(x => x.nombre == rolSeleccionado);

        if (selected is PermisoCompuesto compoundPermit)
        {
            foreach (Permiso p in compoundPermit.PermisosIncluidos)
                MarcarPermisoEnLista(p);
        }
    }

    private void MarcarPermisoEnLista(Permiso permiso)
    {
        for (int i = 0; i < chkListPermisos.Items.Count; i++)
        {
            if (chkListPermisos.Items[i].Value == permiso.nombre)
            {
                chkListPermisos.Items[i].Selected = true;
                break;
            }
        }
    }

    private void CargarArbolPermisos()
    {
        treeViewPermisos.Nodes.Clear();
        var rootPermisos = GP.ObtenerPermisosEnArbol();

        foreach (var permiso in rootPermisos)
        {
            AgregarNodoRecursivo(permiso, treeViewPermisos.Nodes);
        }
    }

    private void AgregarNodoRecursivo(Permiso permiso, TreeNodeCollection parentNodes)
    {
        string label = permiso is PermisoCompuesto c
            ? (c.EsRol ? "ROL: " : "GRUPO: ") + permiso.nombre
            : permiso.nombre;

        TreeNode nodo = new TreeNode(label);
        parentNodes.Add(nodo);

        if (permiso is PermisoCompuesto comp)
            foreach (var sub in comp.PermisosIncluidos)
                AgregarNodoRecursivo(sub, nodo.ChildNodes);
    }

    protected void chkListPermisos_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnGuardarCambios.Enabled = true;
    }

    protected void btnEliminar_Click(object sender, EventArgs e)
    {
        string script = $@"
Swal.fire({{
    title: '¿Eliminar?',
    text: 'Esta acción eliminará el rol/grupo y todos sus vínculos.',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Eliminar',
    cancelButtonText: 'Cancelar'
}}).then((result) => {{
    if (result.isConfirmed) {{
        __doPostBack('{btnEliminarConfirmar.UniqueID}', '');
    }}
}});
";

        ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmEliminar", script, true);
    }

    protected void btnEliminarConfirmar_Click(object sender, EventArgs e)
    {
        if (ddlRolesGrupos.Text == "Webmaster" || ddlRolesGrupos.Text == "Usuario")
        {
            string script = @"
Swal.fire({
    title: 'Error',
    text: 'No puede eliminar ese rol.',
    icon: 'error',
    confirmButtonText: 'Aceptar'
});";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SwalError", script, true);
            return;
        }
        GP.QuitarPermiso(ddlRolesGrupos.Text);

        CargarRolesYGrupos();
        CargarArbolPermisos();
        CargarPermisosAsignados();
    }

    protected void btnModificarNombre_Click(object sender, EventArgs e)
    {
        string script = @"
Swal.fire({
    title: 'Modificar nombre',
    input: 'text',
    inputLabel: 'Nuevo nombre del permiso',
    inputPlaceholder: 'Escriba el nuevo nombre',
    showCancelButton: true,
    confirmButtonText: 'Guardar',
    cancelButtonText: 'Cancelar'
}).then((result) => {
    if (result.isConfirmed && result.value && result.value.trim() !== '') {
        document.getElementById('" + hfNuevoNombre.ClientID + @"').value = result.value;
        __doPostBack('" + btnConfirmarCambio.UniqueID + @"','');
    }
});
";

        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SwalModificarNombre", script, true);
    }

    protected void btnConfirmarCambio_Click(object sender, EventArgs e)
    {
        string nuevoNombre = hfNuevoNombre.Value;
        if (string.IsNullOrWhiteSpace(nuevoNombre)) return;

        GP.ModificarNombrePermiso(ddlRolesGrupos.SelectedValue, nuevoNombre);

        GestorBitacora gestorBitacora = new GestorBitacora();
        gestorBitacora.GuardarLogBitacora($"Se cambió el nombre de {ddlRolesGrupos.SelectedValue} a {hfNuevoNombre}", (Session["Usuario"] as Usuario).NombreUsuario);

        CargarRolesYGrupos();
        CargarArbolPermisos();
        CargarPermisosAsignados();
    }

    protected void btnCrearRol_Click(object sender, EventArgs e)
    {
        GeneracionDePermisoCompuesto(true);
    }

    protected void btnCrearGrupo_Click(object sender, EventArgs e)
    {
        GeneracionDePermisoCompuesto(false);
    }

    private void GeneracionDePermisoCompuesto(bool esRol)
    {
        if (string.IsNullOrWhiteSpace(txtNuevoNombre.Text))
        {
            string script = @"
Swal.fire({
    title: 'Error',
    text: 'El nombre del permiso no puede estar vacío.',
    icon: 'error',
    confirmButtonText: 'Aceptar'
});";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SwalError", script, true);
            return;
        }

        if (GP.ExistePermiso(txtNuevoNombre.Text))
        {
            string script = @"
Swal.fire({
    title: 'Error',
    text: 'Ya existe un permiso con ese nombre.',
    icon: 'error',
    confirmButtonText: 'Aceptar'
});";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SwalError", script, true);
            return;
        }

        string listaPermisosAgregados = "";
        if (!GP.AgregarPermisoCompuesto(txtNuevoNombre.Text, AgregarPermisosCheckeadosAPermisoSeleccionado(txtNuevoNombre.Text, out listaPermisosAgregados), esRol))
        {
            string script = @"
Swal.fire({
    title: 'Error',
    text: 'Hubo un error al guardar el permiso.',
    icon: 'error',
    confirmButtonText: 'Aceptar'
});";

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "SwalError", script, true);
            return;
        }

        GestorBitacora gestorBitacora = new GestorBitacora();
        gestorBitacora.GuardarLogBitacora($"Se creó el nuevo {(esRol ? "Rol" : "Grupo de permisos")} \"{txtNuevoNombre}\"", (Session["Usuario"] as Usuario).NombreUsuario);
        gestorBitacora.GuardarLogBitacora($"Se agregaron los siguientes permisos a \"{txtNuevoNombre}\": {listaPermisosAgregados}", (Session["Usuario"] as Usuario).NombreUsuario);

        txtNuevoNombre.Text = "";
        CargarRolesYGrupos();
        CargarArbolPermisos();
        CargarPermisosAsignados();
    }

    public List<string> AgregarPermisosCheckeadosAPermisoSeleccionado(string nombrePermiso, out string listaPermisos)
    {
        List<string> items = new List<string>();
        foreach (ListItem item in chkListPermisos.Items)
            if (item.Selected) items.Add(item.Text);

        listaPermisos = string.Join(", ", items);
        return items;
    }


    protected void btnGuardarCambios_Click(object sender, EventArgs e)
    {
        string listaPermisosAgregados = "";
        GestorPermisos gestorPermisos = new GestorPermisos();
        if (!gestorPermisos.ModificarPermisoCompuesto(ddlRolesGrupos.Text, AgregarPermisosCheckeadosAPermisoSeleccionado(txtNuevoNombre.Text, out _)))
        {
            string script = @"
Swal.fire({
    title: 'Error',
    text: 'Hubo un error al modificar el permiso.',
    icon: 'error',
    confirmButtonText: 'Aceptar'
});";


            ScriptManager.RegisterStartupScript(
                this.Page,
                this.Page.GetType(),
                "SwalError",
                script,
                true
            );
            return;
        }

        GestorBitacora gestorBitacora = new GestorBitacora();
        gestorBitacora.GuardarLogBitacora($"Se modificó \"{ddlRolesGrupos.Text}\" y se agregaron los siguientes permisos: {listaPermisosAgregados}", (Session["Usuario"] as Usuario).NombreUsuario);

        CargarRolesYGrupos();
        CargarArbolPermisos();
        CargarPermisosAsignados();
    }

    #region TOPBAR

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
        GestorBitacora gestorBitacora = new GestorBitacora();
        gestorBitacora.GuardarLogBitacora("Logout", (Session["Usuario"] as Usuario).NombreUsuario.ToString());
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnVolverALanding_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnBitacora_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraPage.aspx");
    }

    protected void btnRubrosIdiomas_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin_RubrosIdiomas.aspx");
    }

    #endregion

    protected void btnVerPerfilUsuario_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaginaPerfilUsuario.aspx");
    }

    protected void btnPermisos_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin_Permisos.aspx");
    }
}