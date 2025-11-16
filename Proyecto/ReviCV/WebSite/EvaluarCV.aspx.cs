using BLL;
using ENTIDADES;
using SERVICIOS;
using SERVICIOS.Permisos;
using SERVICIOS.Traducciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using PdfiumViewer;
using System.Drawing.Imaging;
using System.IO;

public partial class EvaluarCV : System.Web.UI.Page, IObserver
{
    Curriculum cvMostrar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AccesoHelper.ValidarAcceso(Session["Rol"] as PermisoCompuesto, PermisosStatic.pEvaluarCV))
        {
            Response.Redirect("LandingPage.aspx");
            return;
        }

        if (Application["EstadoBD"].Equals(false))
        {
            Response.Redirect("AvisoErrorBD.aspx");
            return;
        }

        if (!IsPostBack)
        {
            InicializarPagina();
        }
    }


    private void InicializarPagina()
    {
        GestorCurriculum gCurriculums = new GestorCurriculum();
        cvMostrar = gCurriculums.ObtenerCurriculumFiltrado(Session["RubroSeleccionado"].ToString(), Session["IdiomaSeleccionado"].ToString());

        Session["CurriculumLeido"] = cvMostrar;

        if (cvMostrar == null || cvMostrar.ArchivoCV == null)
        {
            Response.Redirect("LandingPage.aspx");
            return;
        }

        RenderizarCV(cvMostrar.ArchivoCV);

        TraductorDAL.TranslatorInstance.CargarTraduccionesDesdeBD((Session["Usuario"] as Usuario).Idioma.ToString());
        Actualizar();

        string fraseComentario = TraductorDAL.TranslatorInstance.Traducir("pComentarioTxt");
        string frasePlaceholder = TraductorDAL.TranslatorInstance.Traducir("txtComentarioPlaceholder");

        pComentario.InnerHtml = $"{fraseComentario} <strong>{cvMostrar.Usuario.ToUpper()}</strong>!";
        txtComentarios.Attributes["placeholder"] = $"{frasePlaceholder} {cvMostrar.Usuario.ToUpper()}?";
    }

    private void RenderizarCV(byte[] archivo)
    {
        string base64String;
        bool esPdf = archivo.Length > 4 && archivo[0] == 0x25 && archivo[1] == 0x50; // "%P"

        if (esPdf)
        {
            using (var ms = new MemoryStream(archivo))
            using (var pdfDoc = PdfDocument.Load(ms)) // Carga el PDF
            using (var bitmap = pdfDoc.Render(0, 1200, 1200, true)) // Renderiza primera página
            using (var imgStream = new MemoryStream())
            {
                bitmap.Save(imgStream, ImageFormat.Png); // Convierte a PNG
                base64String = Convert.ToBase64String(imgStream.ToArray());
            }

            VisorCV.Text = $"<img src='data:image/png;base64,{base64String}' style='max-width:100%; max-height:100%; object-fit:contain;' alt='CV PDF' />";
        }
        else
        {
            base64String = Convert.ToBase64String(archivo);
            VisorCV.Text = $"<img src='data:image;base64,{base64String}' style='max-width:100%; max-height:100%; object-fit:contain;' alt='CV imagen' />";
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
        if (Session["Usuario"] == null)
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
        resena.UsuarioReseñador = (Session["Usuario"] as Usuario).NombreUsuario.ToString();

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
