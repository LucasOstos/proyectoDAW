using SERVICIOS;
using SERVICIOS.Traducciones;
using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MenuAdmin : Page, IObserver
{
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
        gestorBitacora.GuardarLogBitacora("Logout", Session["username"].ToString());
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Rol"] == null) Response.Redirect("LandingPage.aspx");

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
        TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD(Session["Idioma"].ToString());
        Actualizar();
    }

    protected void btnVerPerfilUsuario_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaginaPerfilUsuario.aspx");
    }
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

}
