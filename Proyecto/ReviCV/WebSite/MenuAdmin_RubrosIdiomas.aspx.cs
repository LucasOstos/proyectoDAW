using BLL;
using ENTIDADES;
using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MenuAdmin_RubrosIdiomas : System.Web.UI.Page, IObserver
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AccesoHelper.ValidarAcceso((Session["Rol"] as PermisoCompuesto), PermisosStatic.pGestionRubrosIdiomas))
        {
            Response.Redirect("LandingPage.aspx", true);
            return;
        }

        if (Application["EstadoBD"] is bool bdOk && !bdOk)
        {
            Response.Redirect("AvisoErrorBD.aspx", true);
            return;
        }

        if (!IsPostBack)
        {
            CargarRubros();
            CargarIdiomas();
            TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD((Session["Usuario"] as Usuario).Idioma.ToString());
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
            if (c is LinkButton lbl && lbl.Attributes["data-key"] != null)
            {
                string clave = lbl.Attributes["data-key"];
                lbl.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is HtmlGenericControl html)
            {
                if (html.Attributes["data-key"] != null)
                {
                    string clave = html.Attributes["data-key"];
                    html.InnerText = TraductorDAL.TranslatorInstance.Traducir(clave);
                }
                else if (html.TagName.Equals("p", StringComparison.OrdinalIgnoreCase))
                {
                    string clave = html.Attributes["data-key"];
                    html.InnerText = TraductorDAL.TranslatorInstance.Traducir(clave);
                }
                else if (html.TagName.Equals("h1", StringComparison.OrdinalIgnoreCase))
                {
                    string clave = html.Attributes["data-key"];
                    html.InnerText = TraductorDAL.TranslatorInstance.Traducir(clave);
                }
                else if (html.TagName.Equals("h2", StringComparison.OrdinalIgnoreCase))
                {
                    string clave = html.Attributes["data-key"];
                    html.InnerText = TraductorDAL.TranslatorInstance.Traducir(clave);
                }
            }
            if (c.HasControls())
            {
                RecorrerControles(c);
            }
        }
    }
    private void CargarRubros()
    {
        GestorCurriculum gestorCurriculums = new GestorCurriculum();
        Dictionary<int, string> rubros = gestorCurriculums.ObtenerRubros();

        gvRubros.DataSource = rubros.Select(r => new { Id_Rubro = r.Key, Rubro = r.Value }).ToList();
        gvRubros.DataBind();
    }

    private void CargarIdiomas()
    {
        GestorCurriculum gestorCurriculums = new GestorCurriculum();
        Dictionary<int, string> idiomas = gestorCurriculums.ObtenerIdiomas();

        gvIdiomas.DataSource = idiomas.Select(i => new { Id_Idioma = i.Key, Idioma = i.Value }).ToList();
        gvIdiomas.DataBind();
    }

    protected void btnInicio_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin.aspx");
    }

    protected void btnUsuarios_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin_Usuarios.aspx");
    }

    protected void btnRubrosIdiomas_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuAdmin_RubrosIdiomas.aspx");
    }

    protected void btnVolverALanding_Click(object sender, EventArgs e)
    {
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnBitacora_Click(object sender, EventArgs e)
    {

    }


    protected void btnAgregarIdioma_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionIdioma.Text))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            Dictionary<int, string> idiomas = gestorCurriculums.ObtenerIdiomas();
            if (!idiomas.ContainsValue(txtDescripcionIdioma.Text))
            {
                gestorCurriculums.AltaIdioma(txtDescripcionIdioma.Text);
                CargarIdiomas();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se agregó el idioma {txtDescripcionIdioma.Text}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());

                txtDescripcionIdioma.Text = "";
                hfIdIdioma.Value = "";
            }
            else
            {
                string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'El idioma ingresado ya existe.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
                });
            } else {
                window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
            }
        });";

                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
            }
        }
    }


    protected void btnModificarIdioma_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionIdioma.Text) && int.TryParse(hfIdIdioma.Value, out int idIdioma))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            Dictionary<int, string> idiomas = gestorCurriculums.ObtenerIdiomas();
            if (!idiomas.ContainsValue(txtDescripcionIdioma.Text))
            {
                gestorCurriculums.ModificarIdioma(int.Parse(hfIdIdioma.Value), txtDescripcionIdioma.Text);
                CargarIdiomas();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se modificó el idioma {hfIdIdioma.Value}, ahora es {txtDescripcionIdioma.Text}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());

                txtDescripcionIdioma.Text = "";
                hfIdIdioma.Value = "";
            }
            else
            {
                string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'El idioma ingresado ya existe.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
                });
            } else {
                window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
            }
        });";

                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
            }
        }
    }

    protected void btnEliminarIdioma_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionIdioma.Text) && int.TryParse(hfIdIdioma.Value, out int idIdioma))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            if (gestorCurriculums.IdiomaEnUso(int.Parse(hfIdIdioma.Value)))
            {
                gestorCurriculums.BajaIdioma(int.Parse(hfIdIdioma.Value));
                CargarIdiomas();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se eliminó el idioma {txtDescripcionIdioma.Text}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());

                txtDescripcionIdioma.Text = "";
                hfIdIdioma.Value = "";
            }
            else
            {
                string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'El idioma a eliminar esta siendo usado.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
                });
            } else {
                window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
            }
        });";

                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
            }
        }
    }

    protected void btnAgregarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            Dictionary<int, string> rubros = gestorCurriculums.ObtenerRubros();
            if (!rubros.ContainsValue(txtDescripcionRubro.Text))
            {
                gestorCurriculums.AltaRubro(txtDescripcionRubro.Text);
                CargarRubros();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se agregó el rubro {txtDescripcionRubro.Text}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());

                txtDescripcionRubro.Text = "";
                hfIdRubro.Value = "";
            }
            else
            {
                string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'El rubro ingresado ya existe.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
                });
            } else {
                window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
            }
        });";

                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
            }
        }
    }

    protected void btnModificarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text) && int.TryParse(hfIdRubro.Value, out int idRubro))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            Dictionary<int, string> rubros = gestorCurriculums.ObtenerRubros();
            if (!rubros.ContainsValue(txtDescripcionRubro.Text))
            {
                gestorCurriculums.ModificarRubro(int.Parse(hfIdRubro.Value), txtDescripcionRubro.Text);
                CargarRubros();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se modificó el rubro {hfIdRubro.Value}, ahora es {txtDescripcionRubro.Text}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());

                txtDescripcionRubro.Text = "";
                hfIdRubro.Value = "";
            }
            else
            {
                string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'El rubro ingresado ya existe.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
                });
            } else {
                window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
            }
        });";

                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
            }
        }
    }

    protected void btnEliminarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text) && int.TryParse(hfIdRubro.Value, out int idRubro))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            if (gestorCurriculums.RubroEnUso(int.Parse(hfIdRubro.Value)))
            {
                gestorCurriculums.BajaRubro(int.Parse(hfIdRubro.Value));
                CargarRubros();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se eliminó el rubro {txtDescripcionRubro.Text}", (Session["Usuario"] as Usuario).NombreUsuario.ToString());

                txtDescripcionRubro.Text = "";
                hfIdRubro.Value = "";
            }
            else
            {
                string script = @"
            document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: 'Oops...',
                    text: 'El rubro a eliminar esta siendo usado.',
                    icon: 'error',
                    confirmButtonText: 'Ok',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
                });
            } else {
                window.location.href = 'MenuAdmin_RubrosIdiomas.aspx';
            }
        });";

                ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "SwalSuccess",
                script,
                true
            );
            }
        }
    }

    protected void btnVerPerfilUsuario_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaginaPerfilUsuario.aspx");
    }
}