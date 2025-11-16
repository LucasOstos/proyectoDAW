using SERVICIOS.Traducciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public  class CurriculumBasicoFactory : CurriculumFactory
{
    private readonly EventHandler _onEliminar;
    private readonly CommandEventHandler _onVerResenas;

    public CurriculumBasicoFactory(EventHandler onEliminar, CommandEventHandler onVerResenas)
    {
        _onEliminar = onEliminar;
        _onVerResenas = onVerResenas;
    }

    public override Panel CrearPanel(Curriculum cv)
    {
        var contenedor = new Panel { CssClass = "curriculum-item" };

        var lbl = new Label
        {
            CssClass = "curriculum-titulo",
            Text = $"{cv.Nombre} ({cv.Idioma.Item2} - {cv.Rubro.Item2})"
        };
        contenedor.Controls.Add(lbl);

        var btnEliminar = new LinkButton
        {
            Text = "X",
            CommandArgument = cv.ID_CV.ToString(),
            OnClientClick = "return confirm('¿Eliminar este CV?');",
            Style =
            {
                ["margin-right"] = "10px",
                ["font-size"] = "20px",
                ["color"] = "red",
                ["text-decoration"] = "none",
                ["font-weight"] = "bold"
            }
        };
        if (_onEliminar != null) btnEliminar.Click += _onEliminar;
        contenedor.Controls.Add(btnEliminar);

        var btnVerResenias = new LinkButton
        {
            Text = TraductorDAL.TranslatorInstance.Traducir("VerReseñas"),
            CssClass = "btn btn-guardar",
            CommandArgument = cv.ID_CV.ToString()
        };
        if (_onVerResenas != null) btnVerResenias.Command += _onVerResenas;
        contenedor.Controls.Add(btnVerResenias);

        return contenedor;
    }
}