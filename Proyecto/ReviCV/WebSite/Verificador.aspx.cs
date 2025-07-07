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
        lblMensaje.Visible = false;
        if (Application["ErroresBD"].ToString() != "")
        {
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = Application["ErroresBD"].ToString().Replace("\n", "<br />");
            lblMensaje.Visible = true;
        }
    }

    protected void btnRecalcular_Click(object sender, EventArgs e)
    {
        GestorIntegridad gestorIntegridad = new GestorIntegridad();
        gestorIntegridad.RecalcularTodasLasTablas();

        Application["ErroresBD"] = ""; // Limpiar errores anteriores

        lblMensaje.ForeColor = System.Drawing.Color.Green;
        lblMensaje.Text = "Integridad de las tablas recalculada correctamente.";
        lblMensaje.Visible = true;
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

    protected void btnVerificar_Click(object sender, EventArgs e)
    {
        GestorIntegridad gestorIntegridad = new GestorIntegridad();
        string resultados = gestorIntegridad.VerificarIntegridadTodasLasTablas();

        if (!string.IsNullOrWhiteSpace(resultados))
        {
            Application["ErroresBD"] = resultados;
            lblMensaje.ForeColor = System.Drawing.Color.Red;
            lblMensaje.Text = resultados.Replace("\n", "<br />");
            lblMensaje.Visible = true;
        }
        else
        {
            lblMensaje.ForeColor = System.Drawing.Color.Green;
            lblMensaje.Text = "La base de datos no presenta problemas de integridad.";
            lblMensaje.Visible = true;
        }
    }


}