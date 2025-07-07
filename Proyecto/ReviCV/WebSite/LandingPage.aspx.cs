using BLL;
using ENTIDADES;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LandingPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarRubros();
            CargarIdiomas();
        }
    }

    private void CargarIdiomas()
    {
        GestorCurriculum gCurriculums = new GestorCurriculum();
        var idiomas = gCurriculums.ObtenerIdiomas();

        ddlIdioma.DataSource = idiomas;
        ddlIdioma.DataTextField = "Value"; 
        ddlIdioma.DataValueField = "Key";   
        ddlIdioma.DataBind();

        ddlIdioma.Items.Insert(0, new ListItem("¿En qué idioma?", ""));
        ddlIdioma.Items[0].Attributes.Add("disabled", "true");
        ddlIdioma.Items[0].Selected = true;
    }

    private void CargarRubros()
    {
        GestorCurriculum gCurriculums = new GestorCurriculum();
        var rubros = gCurriculums.ObtenerRubros();

        ddlRubro.DataSource = rubros;
        ddlRubro.DataTextField = "Value";  
        ddlRubro.DataValueField = "Key";  
        ddlRubro.DataBind();

        ddlRubro.Items.Insert(0, new ListItem("¿Qué rubro queres analizar?", ""));
        ddlRubro.Items[0].Attributes.Add("disabled", "true");
        ddlRubro.Items[0].Selected = true;
    }


    protected void EvaluarCVBoton_Click(object sender, EventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (FiltroHabilitado.Value == "true")
            {
                Session["RubroSeleccionado"] = ddlRubro.SelectedValue;
                Session["IdiomaSeleccionado"] = ddlIdioma.SelectedValue;
            }
            else
            {
                Session["RubroSeleccionado"] = "";
                Session["IdiomaSeleccionado"] = "";
            }


            Response.Redirect("EvaluarCV.aspx");
        }
    }

    protected void imgUserIcon_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if (Session["Rol"].ToString() == "Usuario") Response.Redirect("PaginaPerfilUsuario.aspx");
            if (Session["Rol"].ToString() == "Administrador") Response.Redirect("MenuAdmin.aspx");
            if (Session["Rol"].ToString() == "Webmaster") Response.Redirect("WebMaster_Menu.aspx");
        }
    }
}