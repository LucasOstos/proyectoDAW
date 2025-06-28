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
        List<Bitacora> listaLogs = new List<Bitacora>();

        string query = "SELECT * FROM Bitacora WHERE 1=1";

        if (desde.HasValue)
            query += " AND Fecha >= @Desde";

        if (hasta.HasValue)
            query += " AND Fecha <= @Hasta";

        if (!string.IsNullOrEmpty(usuario))
            query += " AND Usuario LIKE @Usuario";

        if (!string.IsNullOrEmpty(operacion))
            query += " AND Operacion LIKE @Operacion";

        using (SqlCommand cmd = new SqlCommand(query, Conexion.Instancia.ReturnConexion()))
        {
            if (desde.HasValue)
                cmd.Parameters.AddWithValue("@Desde", desde.Value);

            if (hasta.HasValue)
                cmd.Parameters.AddWithValue("@Hasta", hasta.Value);

            if (!string.IsNullOrEmpty(usuario))
                cmd.Parameters.AddWithValue("@Usuario", "%" + usuario + "%");

            if (!string.IsNullOrEmpty(operacion))
                cmd.Parameters.AddWithValue("@Operacion", "%" + operacion + "%");

            Conexion.Instancia.AbrirConexion();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    Bitacora log = new Bitacora(DateTime.Parse(dr["Fecha"].ToString()), dr["Operacion"].ToString(), dr["Usuario"].ToString());
                    listaLogs.Add(log);
                }
            }
        }
        return listaLogs;
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