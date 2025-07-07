using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENTIDADES;
using SERVICIOS;
using BLL;

public partial class BitacoraPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Rol"].ToString() != "Webmaster") Response.Redirect("LandingPage.aspx");
        if (!IsPostBack)
        {
            CargarBitacora();
            CargarUsuarios();
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
        ddlUsuario.SelectedValue = null;
        txtOperacion.Text = "";
    }
    
    protected void btnPerfil_Click(object sender, EventArgs e)
    {
        Response.Redirect("PanelUsuario.aspx");

    }
}