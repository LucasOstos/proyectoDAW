using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using BLL;
using ENTIDADES;
using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;

public partial class LandingPage : System.Web.UI.Page, IObserver
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarRubros();
            CargarIdiomas();            
        }
        if(Session["Idioma"] == null) { TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD("Español"); }
        else { TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD(Session["Idioma"].ToString()); }
        Actualizar();
    }
    public void Actualizar()
    {
        RecorrerControles(this);
    }
    void RecorrerControles(Control controlPadre)
    {
        foreach (Control c in controlPadre.Controls)
        {
            if (c is Label lbl && lbl.Attributes["data-key"] != null)
            {
                string clave = lbl.Attributes["data-key"];
                lbl.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
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
                html.InnerHtml = icono + traduccion;
            }
            else if (c is DropDownList ddl && ddl.Attributes["data-key"] != null)
            {
                string clave = ddl.Attributes["data-key"];
                string traduccion = TraductorDAL.TranslatorInstance.Traducir(clave);

                if (ddl.Items.Count > 0)
                {
                    ddl.Items[0].Text = traduccion;
                }
            }
            if (c.HasControls())
                RecorrerControles(c);
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
        if (Session["Usuario"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            if ((Session["Usuario"] as Usuario).Rol.ToString() == PermisosStatic.pAccesoMenuAdmin) Response.Redirect("MenuAdmin.aspx");
            if ((Session["Usuario"] as Usuario).Rol.ToString() == PermisosStatic.pAccesoMenuWB) Response.Redirect("WebMaster_Menu.aspx");
            Response.Redirect("PaginaPerfilUsuario.aspx");
        }
    }
}