using BE;
using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BitacoraPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarBitacora();
        }
    }

    private void CargarBitacora()
    {
        List<Bitacora> logs = GestorBitacora.Instancia.ObtenerLogs();
        gvBitacora.DataSource = logs;
        gvBitacora.DataBind();
    }
}