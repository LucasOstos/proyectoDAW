using BLL;
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
        if (Application["EstadoBD"].Equals(false)) Response.Redirect("AvisoErrorBD.aspx");
        if (Session["Rol"].ToString() != "Administrador") Response.Redirect("LandingPage.aspx");
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
            gestorCurriculums.AltaIdioma(txtDescripcionIdioma.Text);
            CargarIdiomas();
        }
    }


    protected void btnModificarIdioma_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionIdioma.Text) && int.TryParse(hfIdIdioma.Value, out int idIdioma))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            gestorCurriculums.ModificarIdioma(int.Parse(hfIdIdioma.Value), txtDescripcionIdioma.Text);
            CargarIdiomas();

            txtDescripcionIdioma.Text = "";
            hfIdIdioma.Value = "";
        }
    }

    protected void btnEliminarIdioma_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionIdioma.Text) && int.TryParse(hfIdIdioma.Value, out int idIdioma))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            gestorCurriculums.BajaIdioma(int.Parse(hfIdIdioma.Value));
            CargarIdiomas();

            txtDescripcionIdioma.Text = "";
            hfIdIdioma.Value = "";
        }
    }


    protected void btnAgregarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            gestorCurriculums.AltaRubro(txtDescripcionRubro.Text);
            CargarRubros();

            txtDescripcionRubro.Text = "";
            hfIdRubro.Value = "";
        }

    }

    protected void btnModificarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text) && int.TryParse(hfIdRubro.Value, out int idRubro))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            gestorCurriculums.ModificarRubro(int.Parse(hfIdRubro.Value), txtDescripcionRubro.Text);
            CargarRubros();

            txtDescripcionRubro.Text = "";
            hfIdRubro.Value = "";
        }
    }

    protected void btnEliminarRubro_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDescripcionRubro.Text) && int.TryParse(hfIdRubro.Value, out int idRubro))
        {
            GestorCurriculum gestorCurriculums = new GestorCurriculum();
            gestorCurriculums.BajaRubro(int.Parse(hfIdRubro.Value));
            CargarRubros();

            txtDescripcionRubro.Text = "";
            hfIdRubro.Value = "";
        }
    }
}