using SERVICIOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PruebaDigito : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GestorIntegridad gestorIntegridad = new GestorIntegridad();
        var resultados = gestorIntegridad.VerificarIntegridadTodasLasTablas();

        var mensaje = "";

        foreach (var resultado in resultados)
        {
            if (!resultado.Value) // si la verificación falló
            {
                mensaje += $"La tabla {resultado.Key} tiene errores de integridad.<br/>";
            }
        }
        Label1.ForeColor = System.Drawing.Color.Red;

        if (string.IsNullOrEmpty(mensaje))
        {
            mensaje = "Todas las tablas están correctas.";
            Label1.ForeColor = System.Drawing.Color.Green;
        }

        Label1.Text = mensaje;

    }
}