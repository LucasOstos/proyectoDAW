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
}