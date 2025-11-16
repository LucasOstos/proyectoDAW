using BLL;
using ENTIDADES;
using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class BitacoraPage : System.Web.UI.Page, IObserver
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AccesoHelper.ValidarAcceso(Session["Rol"] as PermisoCompuesto, PermisosStatic.pAccesoBitacora))
        {
            Response.Redirect("LandingPage.aspx");
            return;
        }

        if (!IsPostBack)
        {
            CargarUsuarios();
            TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD((Session["Usuario"] as Usuario).Idioma.ToString());
        }

        CargarBitacora();
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
            else if (c is DataControlFieldCell cell)
            {
                foreach (Control h in cell.Controls)
                {
                    if (h is HtmlGenericControl span && span.Attributes["data-key"] != null)
                    {
                        string clave = span.Attributes["data-key"];
                        span.InnerHtml = TraductorDAL.TranslatorInstance.Traducir(clave);
                    }
                }
            }
            if (c.HasControls())
                RecorrerControles(c);
        }
    }
    private void CargarBitacora()
    {
        GestorBitacora gestorBitacora = new GestorBitacora();
        List<Bitacora> logs = gestorBitacora.ObtenerLogs();
        gvBitacora.DataSource = logs;
        gvBitacora.DataBind();
    }
    private void CargarUsuarios()
    {
        ddlUsuario.Items.Insert(0, new ListItem("Seleccione un usuario", ""));
        ddlUsuario.Items[0].Attributes.Add("disabled", "true");
        ddlUsuario.Items[0].Selected = true;
        GestorUsuario gestor = new GestorUsuario();
        foreach(string nombreUsuario in gestor.ObtenerTodosNombresUsuarios())
        {
            ddlUsuario.Items.Add(nombreUsuario);
        }
    }
    private List<Bitacora> Filtros(DateTime? desde, DateTime? hasta, string usuario, string operacion)
    {
        GestorBitacora gestorBitacora = new GestorBitacora();
        return gestorBitacora.FiltrosBitacora(desde, hasta, usuario, operacion);
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        DateTime? desde = string.IsNullOrEmpty(txtFechaDesde.Text) ? null : DateTime.Parse(txtFechaDesde.Text);
        DateTime? hasta = string.IsNullOrEmpty(txtFechaHasta.Text) ? null : DateTime.Parse(txtFechaHasta.Text);
        string usuario = ddlUsuario.SelectedValue;
        gvBitacora.DataSource = Filtros(desde, hasta, usuario, txtOperacion.Text);
        gvBitacora.DataBind();
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("WebMaster_Menu.aspx");
    }

    protected void btnContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackUp_ReStore.aspx");
    }

    protected void btnFAQ_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verificador.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        GestorBitacora gestorBitacora = new GestorBitacora();
        gestorBitacora.GuardarLogBitacora("Logout", (Session["Usuario"] as Usuario).NombreUsuario);
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackUp_ReStore.aspx");
    }


    protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
    {
        txtFechaDesde.Text = "";
        txtFechaHasta.Text = "";
        ddlUsuario.SelectedValue = ddlUsuario.Items[0].Value;
        txtOperacion.Text = "";
        CargarBitacora();
    }
    
    protected void btnPerfil_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaginaPerfilUsuario.aspx");

    }
}