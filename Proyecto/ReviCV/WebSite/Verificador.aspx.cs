using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERVICIOS;

public partial class Verificador : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRecalcular_Click(object sender, EventArgs e)
    {
    //    GestorIntegridad gestorIntegridad = new GestorIntegridad();
    //    //var resultados = gestorIntegridad.VerificarIntegridadTodasLasTablas();

    //    var mensaje = "";

    //    foreach (var resultado in resultados)
    //    {
    //        if (!resultado.Value) // si la verificación falló
    //        {
    //            mensaje += $"La tabla {resultado.Key} tiene errores de integridad.<br/>";
    //        }
    //    }
    //    Label1.ForeColor = System.Drawing.Color.Red;

    //    if (string.IsNullOrEmpty(mensaje))
    //    {
    //        mensaje = "Todas las tablas están correctas.";
    //        Label1.ForeColor = System.Drawing.Color.Green;
    //    }
    //    Label1.Text = mensaje;
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {
        Response.Redirect("WebMaster_Menu.aspx");
    }

    protected void btnContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackUp_ReStore.aspx");
    }

    protected void btnAbout_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verificador.aspx");
    }

    protected void btnFAQ_Click(object sender, EventArgs e)
    {

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
}