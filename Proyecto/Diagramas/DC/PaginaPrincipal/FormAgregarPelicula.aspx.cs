using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

public partial class FormAgregarPelicula : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btnAgregar_Click(object sender, EventArgs e)
    {
        Page.Validate("grupoAlta");
        if (Page.IsValid)
        {
            GestorPeliculas gestor = new GestorPeliculas();
            Pelicula pelicula = new Pelicula(tbNombre.Text, tbCategoria.Text, tbAnio.Text, tbDirector.Text);
            gestor.AgregarPelicula(pelicula);
            Limpiar();
        }
    }

    private void Limpiar()
    {
        tbNombre.Text = "";
        tbCategoria.Text = "";
        tbAnio.Text = "";
        tbDirector.Text = "";
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Limpiar();
    }
}