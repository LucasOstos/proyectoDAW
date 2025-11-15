using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ResenaVM
/// </summary>
public class ResenaVM
{
    public int IdOpinion { get; set; }
    public string NombreUsuario { get; set; }
    public int Contenido { get; set; }
    public int Diseno { get; set; }
    public int Claridad { get; set; }
    public int Relevancia { get; set; }
    public string Comentario { get; set; }
    public string FotoUsuario { get; set; }
}
