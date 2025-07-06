using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BE;
using BLL;

public partial class FormPeliculas : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CargarNombres();
        CargarPeliculas();
    }
    private void CargarPeliculas()
    {
        gvPeliculas.DataSource = null;
        GestorPeliculas gestor = new GestorPeliculas();
        List<Pelicula> peliculas = gestor.Peliculas();
        gvPeliculas.DataSource = peliculas;
        gvPeliculas.DataBind();
    }
    private void CargarNombres()
    {
        ddlPeliculas.Items.Clear();
        GestorPeliculas gestor = new GestorPeliculas();
        List<Pelicula> peliculas = gestor.Peliculas();
        foreach (Pelicula p in peliculas)
        {
            ddlPeliculas.Items.Add(p.Nombre);
        }
    }

    protected void btnBorrar_Click(object sender, EventArgs e)
    {
        GestorPeliculas gestor = new GestorPeliculas();
        gestor.BorrarPelicula(ddlPeliculas.Text);
        CargarPeliculas();
        CargarNombres();
    }
}