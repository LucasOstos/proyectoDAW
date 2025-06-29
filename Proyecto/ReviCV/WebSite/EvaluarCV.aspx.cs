using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ENTIDADES;

public partial class EvaluarCV : System.Web.UI.Page
{
    Curriculum cvMostrar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GestorCurriculum gCurriculums = new GestorCurriculum();
            cvMostrar = gCurriculums.ObtenerCurriculumFiltrado(Session["RubroSeleccionado"].ToString(), Session["IdiomaSeleccionado"].ToString());
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
    }


    protected void imgUserIcon_Click(object sender, ImageClickEventArgs e)
    {
        if (Session["username"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {
            Response.Redirect("PanelUsuario.aspx");
        }
    }

    protected void enviar_Click(object sender, EventArgs e)
    {
        Resena resena = new Resena();
        resena.Comentarios = txtComentarios.Text;
        resena.ID_CV = (Session["CurriculumLeido"] as Curriculum).ID_CV;
        resena.UsuarioReseñador = Session["username"].ToString();

        // Leer las calificaciones desde el formulario
        resena.Contenido = LeerValorRadio("contenido");
        resena.Diseno = LeerValorRadio("diseno");
        resena.Claridad = LeerValorRadio("claridad");
        resena.Relevancia = LeerValorRadio("relevancia");

        GestorResena gestorResenas = new GestorResena();
        gestorResenas.GuardarResena(resena);

        //JS puro
        //ClientScript.RegisterStartupScript(
        //    this.GetType(),
        //    "alerta",
        //    "alert('¡La reseña fue enviada con éxito!'); window.location='LandingPage.aspx';",
        //    true
        //);

        //Con SweetAlert https://sweetalert2.github.io/

        string script = @"
        document.addEventListener('DOMContentLoaded', function() {
            if (typeof Swal !== 'undefined') {
                Swal.fire({
                    title: '¡Gracias!',
                    text: 'Tu reseña fue enviada con éxito.',
                    icon: 'success',
                    confirmButtonText: 'Ir al inicio',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {
                        container: 'swal-container-fix'
                    }
                }).then(() => {
                    window.location.href = 'LandingPage.aspx';
                });
            } else {
                window.location.href = 'LandingPage.aspx';
            }
        });";

        ScriptManager.RegisterStartupScript(
            this,
            this.GetType(),
            "SwalSuccess",
            script,
            true
        );
    }

    private int LeerValorRadio(string nombreCampo)
    {
        string valor = Request.Form[nombreCampo];
        return int.TryParse(valor, out int resultado) ? resultado : 0;
    }
}
