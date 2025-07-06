using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SERVICIOS;

public partial class PanelUsuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["EstadoBD"].Equals(false)) Response.Redirect("AvisoErrorBD.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {        
        Session.Clear();
        Response.Redirect("LandingPage.aspx");
    }
}