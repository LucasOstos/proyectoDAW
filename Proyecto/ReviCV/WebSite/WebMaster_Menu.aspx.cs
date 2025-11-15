using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using SERVICIOS.Traducciones;

public partial class WebMaster_Menu : System.Web.UI.Page, IObserver
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Rol"] == null) Response.Redirect("LandingPage.aspx");
        if (Session["Rol"].ToString() != "Webmaster") Response.Redirect("LandingPage.aspx");
        TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD(Session["Idioma"].ToString());
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
    protected void btnHome_Click(object sender, EventArgs e)
    {

    }

    protected void btnContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackUp_ReStore.aspx");
    }


    protected void btnFAQ_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verificador.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("BitacoraPage.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }

    protected void btnPerfil_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaginaPerfilUsuario.aspx");
    }
}