using BLL;
using ENTIDADES;
using SERVICIOS.Permisos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VerResenias : System.Web.UI.Page
{
    int idCVActual;
    Curriculum cvMostrar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AccesoHelper.ValidarAcceso(Session["Rol"] as PermisoCompuesto))
        {
            Response.Redirect("LandingPage.aspx");
            return;
        }

        if (Application["EstadoBD"] is bool bdOk && !bdOk)
        {
            Response.Redirect("AvisoErrorBD.aspx");
            return;
        }

        if (!IsPostBack)
        {
            CargarCV();
            CargarOpiniones(idCVActual);
        }
    }


    private void CargarCV()
    {
        if (Request.QueryString["id"] != null)
        {
            idCVActual = int.Parse(Request.QueryString["id"]);
            GestorCurriculum gCurriculums = new GestorCurriculum();
            cvMostrar = gCurriculums.ObtenerCurriculumPorID(idCVActual);
            Session["CurriculumLeido"] = cvMostrar;
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
        else
        {
            Response.Redirect("LandingPage.aspx");
        }
    }

    private void CargarOpiniones(int idCV)
    {
        GestorResena gestorResena = new GestorResena();
        List<Resena> lista = gestorResena.ObtenerReseniasDeCVPorIDdeCV(idCV);
        lista = lista.OrderByDescending(r => r.ID_Resena).ToList();

        if (lista != null && lista.Count > 0)
        {
            Random rnd = new Random();

            var listaVM = lista.Select(r => new ResenaVM
            {
                IdOpinion = r.ID_Resena,
                NombreUsuario = r.UsuarioReseñador,
                Contenido = r.Contenido,
                Diseno = r.Diseno,
                Claridad = r.Claridad,
                Relevancia = r.Relevancia,
                Comentario = r.Comentarios,
                FotoUsuario = GenerarFotoRandom(rnd)
            }).ToList();

            rptOpiniones.DataSource = listaVM;
            rptOpiniones.DataBind();
            pnlSinOpiniones.Visible = false;
        }
        else
        {
            pnlSinOpiniones.Visible = true;
        }
    }

    private string GenerarFotoRandom(Random rnd)
    {
        int num = rnd.Next(1, 100);
        bool mujer = rnd.Next(0, 2) == 0;

        return mujer
            ? $"https://randomuser.me/api/portraits/women/{num}.jpg"
            : $"https://randomuser.me/api/portraits/men/{num}.jpg";
    }



    protected string GenerarEstrellas(int calificacion)
    {
        string html = "";

        for (int i = 1; i <= 5; i++)
        {
            if (i <= calificacion)
            {
                html += "<span class='estrella'>★</span>";
            }
            else
            {
                html += "<span class='estrella vacia'>★</span>";
            }
        }

        return html;
    }

   
    protected void imgUserIcon_Click(object sender, EventArgs e)
    {
        Response.Redirect("PaginaPerfilUsuario.aspx");
    }
}