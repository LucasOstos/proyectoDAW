using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

public partial class CargarCVs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

            GestorUsuario gUsuarios = new GestorUsuario();
            DropDownList1.Items.Clear();

        var nombres = gUsuarios.ObtenerTodosNombresUsuarios()
                   .OrderBy(c => c)
                   .ToList();


        foreach (string nombre in nombres)
            {
                DropDownList1.Items.Add(new ListItem(nombre));
            }
    }

}