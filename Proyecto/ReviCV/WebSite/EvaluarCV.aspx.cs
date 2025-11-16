using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BLL;
using ENTIDADES;
using SERVICIOS.Traducciones;

public partial class EvaluarCV : System.Web.UI.Page, IObserver
{
    Curriculum cvMostrar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Rol"] == null) Response.Redirect("LandingPage.aspx");
        if (Application["EstadoBD"].Equals(false)) Response.Redirect("AvisoErrorBD.aspx");
        TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD(Session["Idioma"].ToString());
        Actualizar();
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
            string fraseComentario = TraductorDAL.TranslatorInstance.Traducir("pComentarioTxt");
            string frasePlaceholder = TraductorDAL.TranslatorInstance.Traducir("txtComentarioPlaceholder");
            pComentario.InnerHtml = $"{fraseComentario} <strong>{cvMostrar.Usuario.ToUpper()}</strong>!";
            txtComentarios.Attributes["placeholder"] = $"{frasePlaceholder} {cvMostrar.Usuario.ToUpper()}?";
        }
    }
    public void Actualizar()
    {
        RecorrerControles(this);
    }
    void RecorrerControles(Control controlPadre)
    {
        foreach (Control c in controlPadre.Controls)
        {
            if (c is Label lbl && lbl.Attributes["data-key"] != null)
            {
                string clave = lbl.Attributes["data-key"];
                lbl.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is Button btn && btn.Attributes["data-key"] != null)
            {
                string clave = btn.Attributes["data-key"];
                btn.Text = TraductorDAL.TranslatorInstance.Traducir(clave);
            }
            else if (c is HtmlGenericControl html && html.Attributes["data-key"] != null)
            {
                string clave = html.Attributes["data-key"];

                string htmlAnterior = html.InnerHtml;

                string icono = "";
                if (htmlAnterior.Contains("</i>"))
                {
                    int finIcono = htmlAnterior.IndexOf("</i>") + 4;
                    icono = htmlAnterior.Substring(0, finIcono);
                }
                string traduccion = TraductorDAL.TranslatorInstance.Traducir(clave);
                html.InnerHtml = icono + traduccion;
            }            
            if (c.HasControls())
                RecorrerControles(c);
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
            Response.Redirect("PaginaPerfilUsuario.aspx");
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

        string title =  TraductorDAL.TranslatorInstance.Traducir("alertGracias");
        string text = TraductorDAL.TranslatorInstance.Traducir("alertResenaExitosa");
        string confirmButtonText = TraductorDAL.TranslatorInstance.Traducir("alertIrInicio");
        string script = $@"
        document.addEventListener('DOMContentLoaded', function() {{
                if (typeof Swal !== 'undefined') {{
                    Swal.fire({{
                    title: '{title}',
                    text: '{text}',
                    icon: 'success',
                    confirmButtonText: '{confirmButtonText}',
                    backdrop: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    customClass: {{
                        container: 'swal-container-fix'
                        }}
                    }}).then(() => {{
                        window.location.href = 'LandingPage.aspx';
                    }});
                }} else {{
                        window.location.href = 'LandingPage.aspx';
                        }}
                }});";
        //string script = @"
        //document.addEventListener('DOMContentLoaded', function() {
        //    if (typeof Swal !== 'undefined') {
        //        Swal.fire({
        //            title: '¡Gracias!',
        //            text: 'Tu reseña fue enviada con éxito.',
        //            icon: 'success',
        //            confirmButtonText: 'Ir al inicio',
        //            backdrop: true,
        //            allowOutsideClick: false,
        //            allowEscapeKey: false,
        //            customClass: {
        //                container: 'swal-container-fix'
        //            }
        //        }).then(() => {
        //            window.location.href = 'LandingPage.aspx';
        //        });
        //    } else {
        //        window.location.href = 'LandingPage.aspx';
        //    }
        //});";

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
