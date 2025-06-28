using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using BE;
using System.Runtime.CompilerServices;

public partial class CargarCVs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            CargarRubros();
            CargarIdiomas();

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

    private void CargarIdiomas()
    {
        GestorCurriculums gCurriculums = new GestorCurriculums();
        var idiomas = gCurriculums.ObtenerIdiomas();

        ddlIdioma.DataSource = idiomas;
        ddlIdioma.DataTextField = "Value";  // Nombre que se muestra
        ddlIdioma.DataValueField = "Key";   // ID que se usa internamente
        ddlIdioma.DataBind();

        ddlIdioma.Items.Insert(0, new ListItem("¿En qué idioma está tu CV?", ""));
        ddlIdioma.Items[0].Attributes.Add("disabled", "true");
        ddlIdioma.Items[0].Selected = true;
    }

    private void CargarRubros()
    {
        GestorCurriculums gCurriculums = new GestorCurriculums();
        var rubros = gCurriculums.ObtenerRubros();

        ddlRubro.DataSource = rubros;
        ddlRubro.DataTextField = "Value";  // Nombre que se muestra
        ddlRubro.DataValueField = "Key";   // ID que se usa internamente
        ddlRubro.DataBind();

        ddlRubro.Items.Insert(0, new ListItem("¿De que rubro es tu CV?", ""));
        ddlRubro.Items[0].Attributes.Add("disabled", "true");
        ddlRubro.Items[0].Selected = true;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        HttpFileCollection archivos = Request.Files;

        for (int i = 0; i < archivos.Count; i++)
        {
            HttpPostedFile archivo = archivos[i];

            if (archivo != null && archivo.ContentLength > 0)
            {
                string extension = Path.GetExtension(archivo.FileName).ToLower();
                if (extension == ".pdf" || extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                {
                    Curriculum cvActual = new Curriculum();
                    GestorUsuario gUsuarios = new GestorUsuario();
                    cvActual.Usuario = gUsuarios.ObtenerUsuario(DropDownList1.SelectedItem.Text.ToString());
                    using (var ms = new MemoryStream())
                    {
                        archivo.InputStream.CopyTo(ms);
                        cvActual.ArchivoCV = ms.ToArray();
                    }
                    cvActual.Idioma = (int.Parse(ddlIdioma.SelectedValue), ddlIdioma.SelectedItem.Text);
                    cvActual.Rubro = (int.Parse(ddlRubro.SelectedValue), ddlRubro.SelectedItem.Text);

                    GestorCurriculums gCurriculum = new GestorCurriculums();
                    gCurriculum.GuardarCurriculum(cvActual);

                    Confirmacion.Text = $"El CV se ha subido correctamente a la cuenta del usuario {DropDownList1.SelectedItem.Text.ToString()}";
                }
            }
        }
    }


    protected void ButtonMostrarCV_Click(object sender, EventArgs e)
    {
        int idCV;
        if (!int.TryParse(TextBoxNumeroCV.Text, out idCV))
        {
            Confirmacion.Text = "Ingrese un número válido";
            return;
        }

        GestorCurriculums gCurriculum = new GestorCurriculums();
        Curriculum cvMostrar = gCurriculum.ObtenerCurriculumPorID(idCV);

        if (cvMostrar == null || cvMostrar.ArchivoCV == null)
        {
            Confirmacion.Text = "CV no encontrado";
            LiteralVisorCV.Text = "<p style='color:red;'>CV no encontrado</p>";
            return;
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
            LiteralVisorCV.Text = $"<embed src='data:application/pdf;base64,{base64String}#toolbar=0&navpanes=0&scrollbar=0' type='application/pdf' style='width:100%; height:100%; border: none;' />";
        }
        else
        {
            // Mostrar imagen (asumimos png/jpg)
            LiteralVisorCV.Text = $"<img src='data:image;base64,{base64String}' style='max-width:100%; max-height:100%; object-fit: contain;' alt='CV imagen' />";
        }


        Confirmacion.Text = $"Mostrando CV número {idCV}";
    }

}