using BLL;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MenuAdmin_RubrosIdiomas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var estadoBD = Application["EstadoBD"];
        var rol = Session["Rol"]?.ToString();

        if (estadoBD is bool bdOk && !bdOk)
        {
            Response.Redirect("AvisoErrorBD.aspx");
        }

        if (rol != "Administrador")
        {
            Response.Redirect("LandingPage.aspx");
        }

        if (!IsPostBack)
        {
            CargarRubros();
            CargarIdiomas();
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
                gestorBitacora.GuardarLogBitacora($"Se agregó el idioma {txtDescripcionIdioma.Text}", Session["username"].ToString());

                txtDescripcionIdioma.Text = "";
                hfIdIdioma.Value = "";
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
                gestorBitacora.GuardarLogBitacora($"Se modificó el idioma {hfIdIdioma.Value}, ahora es {txtDescripcionIdioma.Text}", Session["username"].ToString());

                txtDescripcionIdioma.Text = "";
                hfIdIdioma.Value = "";
            }            
        }
    }

    protected void btnEliminarIdioma_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionIdioma.Text) && int.TryParse(hfIdIdioma.Value, out int idIdioma))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            gestorCurriculums.BajaIdioma(int.Parse(hfIdIdioma.Value));
            CargarIdiomas();

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.GuardarLogBitacora($"Se eliminó el idioma {txtDescripcionIdioma.Text}", Session["username"].ToString());

            txtDescripcionIdioma.Text = "";
            hfIdIdioma.Value = "";
        }
    }

    protected void btnAgregarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            Dictionary<int, string> rubros = gestorCurriculums.ObtenerRubros();
            if(!rubros.ContainsValue(txtDescripcionRubro.Text))
            {
                gestorCurriculums.AltaRubro(txtDescripcionRubro.Text);
                CargarRubros();

                GestorBitacora gestorBitacora = new GestorBitacora();
                gestorBitacora.GuardarLogBitacora($"Se agregó el rubro {txtDescripcionRubro.Text}", Session["username"].ToString());

                txtDescripcionRubro.Text = "";
                hfIdRubro.Value = "";
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
                gestorBitacora.GuardarLogBitacora($"Se modificó el rubro {hfIdRubro.Value}, ahora es {txtDescripcionRubro.Text}", Session["username"].ToString());

                txtDescripcionRubro.Text = "";
                hfIdRubro.Value = "";
            }                
        }
    }

    protected void btnEliminarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text) && int.TryParse(hfIdRubro.Value, out int idRubro))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            gestorCurriculums.BajaRubro(int.Parse(hfIdRubro.Value));
            CargarRubros();

            GestorBitacora gestorBitacora = new GestorBitacora();
            gestorBitacora.GuardarLogBitacora($"Se eliminó el rubro {txtDescripcionRubro.Text}", Session["username"].ToString());

            txtDescripcionRubro.Text = "";
            hfIdRubro.Value = "";
        }
    }
}