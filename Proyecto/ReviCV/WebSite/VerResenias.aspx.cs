using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VerResenias : System.Web.UI.Page
{
    Curriculum cvMostrar;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Rol"] == null) Response.Redirect("LandingPage.aspx");
        if (Application["EstadoBD"].Equals(false)) Response.Redirect("AvisoErrorBD.aspx");
        if (!IsPostBack)
        {
            CargarCV();
            CargarOpiniones();
        }
    }

    private void CargarCV()
    {
        if (Request.QueryString["id"] != null)
        {
            int idCV = int.Parse(Request.QueryString["id"]);
            GestorCurriculum gCurriculums = new GestorCurriculum();
            cvMostrar = gCurriculums.ObtenerCurriculumPorID(idCV);
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

    private void CargarOpiniones()
    {
        // Crear datos de ejemplo hardcodeados
        DataTable dtOpiniones = CrearOpinionesEjemplo();

        if (dtOpiniones.Rows.Count > 0)
        {
            rptOpiniones.DataSource = dtOpiniones;
            rptOpiniones.DataBind();
            pnlSinOpiniones.Visible = false;
        }
        else
        {
            pnlSinOpiniones.Visible = true;
        }
    }

    private DataTable CrearOpinionesEjemplo()
    {
        DataTable dt = new DataTable();

        // Definir las columnas
        dt.Columns.Add("IdOpinion", typeof(int));
        dt.Columns.Add("NombreUsuario", typeof(string));
        dt.Columns.Add("FotoUsuario", typeof(string));
        dt.Columns.Add("Contenido", typeof(int));
        dt.Columns.Add("Diseno", typeof(int));
        dt.Columns.Add("Claridad", typeof(int));
        dt.Columns.Add("Relevancia", typeof(int));
        dt.Columns.Add("Comentario", typeof(string));

        // Agregar opiniones de ejemplo
        dt.Rows.Add(1, "William Rogers", "https://randomuser.me/api/portraits/men/32.jpg",
            5, 3, 5, 5,
            "Buen enfoque en educación y trato infantil. Podrías reforzar logros concretos y usar un diseño más moderno para destacar.");

        dt.Rows.Add(2, "Selena Watson", "https://randomuser.me/api/portraits/women/44.jpg",
            3, 1, 5, 5,
            "El CV muestra compromiso, pero falta claridad en logros y datos específicos. El diseño es funcional, aunque algo básico y saturado.");

        dt.Rows.Add(3, "Hayao Miyagi", "https://randomuser.me/api/portraits/men/85.jpg",
            1, 1, 1, 1,
            "¡Es el peor currículum que ví en mi vida!");

        dt.Rows.Add(4, "Kevin Davis", "https://randomuser.me/api/portraits/men/67.jpg",
            3, 2, 4, 4,
            "El CV muestra compromiso, pero falta claridad en logros y datos específicos. El diseño es funcional, aunque algo básico y saturado.");

        dt.Rows.Add(5, "María González", "https://randomuser.me/api/portraits/women/65.jpg",
            4, 4, 3, 5,
            "Muy buena estructura y organización. Me gusta cómo destacas tu experiencia docente. Quizás podrías agregar más detalles cuantitativos sobre tus logros.");

        dt.Rows.Add(6, "Carlos Mendoza", "https://randomuser.me/api/portraits/men/22.jpg",
            5, 5, 5, 4,
            "Excelente CV, muy profesional. La sección de habilidades está muy bien detallada y el diseño es limpio y moderno. ¡Felicitaciones!");

        return dt;
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

    protected void btnLike_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string idOpinion = btn.CommandArgument;

        // Mostrar mensaje de confirmación
        ScriptManager.RegisterStartupScript(this, GetType(), "like" + idOpinion,
            "Swal.fire({icon: 'success', title: '¡Me gusta registrado!', showConfirmButton: false, timer: 1500});",
            true);

        // Aquí en el futuro agregarías la lógica para guardar en BD
    }

    protected void btnDislike_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string idOpinion = btn.CommandArgument;

        // Mostrar mensaje de confirmación
        ScriptManager.RegisterStartupScript(this, GetType(), "dislike" + idOpinion,
            "Swal.fire({icon: 'info', title: 'No me gusta registrado', showConfirmButton: false, timer: 1500});",
            true);

        // Aquí en el futuro agregarías la lógica para guardar en BD
    }

    protected void imgUserIcon_Click(object sender, ImageClickEventArgs e)
    {
        // Redirigir al perfil de usuario
        Response.Redirect("PerfilUsuario.aspx");
    }
}