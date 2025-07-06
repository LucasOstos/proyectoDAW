using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebMaster_Menu : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Rol"].ToString() != "Webmaster") Response.Redirect("LandingPage.aspx");
    }

    protected void btnHome_Click(object sender, EventArgs e)
    {

    }

    protected void btnContact_Click(object sender, EventArgs e)
    {
        Response.Redirect("BackUp_ReStore.aspx");
    }

    protected void btnAbout_Click(object sender, EventArgs e)
    {

    }

    protected void btnFAQ_Click(object sender, EventArgs e)
    {
        Response.Redirect("Verificador.aspx");
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