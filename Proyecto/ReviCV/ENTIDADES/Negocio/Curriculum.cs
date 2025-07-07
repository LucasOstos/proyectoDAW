using ENTIDADES;

public class Curriculum
{
    //Los curriculums tienen un ID, un idioma y un rubro, y pertenecen a un usuario
    public int ID_CV { get; set; }
    public Usuario Usuario { get; set; }
    public (int,string) Idioma { get; set; }
    public (int, string) Rubro { get; set; }
    public string Nombre { get; set; }

    // Contenido del archivo en binario para mostrarlo posteriormente como una imagen
    public byte[] ArchivoCV { get; set; }

    public Curriculum(int numCurriculum, Usuario pUsuario, (int, string) pIdioma, (int, string) pRubro, byte[] archivoCV)
    {
        ID_CV = numCurriculum;
        Usuario = pUsuario;
        Idioma = pIdioma;
        Rubro = pRubro;
        ArchivoCV = archivoCV;
    }

    public Curriculum() { }
}
