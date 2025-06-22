using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EvaluarCV : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GestorCurriculums gCurriculums = new GestorCurriculums();
        Curriculum cvMostrar = gCurriculums.ObtenerCurriculumFiltrado(Session["RubroSeleccionado"].ToString(), Session["IdiomaSeleccionado"].ToString());


        if (cvMostrar == null || cvMostrar.ArchivoCV == null)
        {
            Response.Redirect("LandingPage.aspx");
        }

        // Convertir archivo a base64
        string base64String = Convert.ToBase64String(cvMostrar.ArchivoCV);

        // Detectar si es imagen o PDF (por extensión o primer byte)
        // Aquí simple: si empieza con %PDF -> PDF, si no -> imagen (puedes mejorar)
        bool esPdf = false;
        byte[] archivo = cvMostrar.ArchivoCV;
        if (archivo.Length > 4 && archivo[0] == 0x25 && archivo[1] == 0x50) // %P de %PDF
            esPdf = true;

        if (esPdf)
        {
            // Usar embed en lugar de iframe para ocultar controles
            VisorCV.Text = $"<embed src='data:application/pdf;base64,{base64String}#toolbar=0&navpanes=0&scrollbar=0' type='application/pdf' style='width:100%; height:100%; border: none;' />";
        }
        else
        {
            // Mostrar imagen (asumimos png/jpg)
            VisorCV.Text = $"<img src='data:image;base64,{base64String}' style='max-width:100%; max-height:100%; object-fit: contain;' alt='CV imagen' />";
        }
    }

}
