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

public partial class AvisoErrorBD : System.Web.UI.Page, IObserver
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SingletonIntegridad.Instancia.BaseIntegra || Session["Rol"] == null)
        {
            Response.Redirect("LandingPage.aspx");
        }
        TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD((Session["Usuario"] as Usuario).Idioma.ToString());
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
            if (c.HasControls())
                RecorrerControles(c);
        }
    }
    protected void btnCerrarSesion_Click(object sender, EventArgs e)
    {
        var command = new LogoutCommand(Session["Usuario"] as Usuario);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Logout", command.Ejecutar(), true);
    }
}