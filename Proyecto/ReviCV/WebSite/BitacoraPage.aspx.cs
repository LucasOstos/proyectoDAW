using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENTIDADES;
using DAL;
using SERVICIOS;
using BLL;

public partial class BitacoraPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarBitacora();
            CargarUsuarios();
        }
    }

    private void CargarBitacora()
    {
        List<Bitacora> logs = GestorBitacora.Instancia.ObtenerLogs();
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
        return GestorBitacora.Instancia.FiltrosBitacora(desde, hasta, usuario, operacion);
    }

    protected void btnFiltrar_Click(object sender, EventArgs e)
    {
        DateTime? desde = string.IsNullOrEmpty(txtFechaDesde.Text) ? null : DateTime.Parse(txtFechaDesde.Text);
        DateTime? hasta = string.IsNullOrEmpty(txtFechaHasta.Text) ? null : DateTime.Parse(txtFechaHasta.Text);
        string usuario = ddlUsuario.SelectedValue;
        gvBitacora.DataSource = Filtros(desde, hasta, usuario, txtOperacion.Text);
        gvBitacora.DataBind();
    }
}