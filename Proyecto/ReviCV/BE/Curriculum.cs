using BE;

public class Curriculum
{
    public int ID_CV { get; set; }
    public Usuario Usuario { get; set; }
    public (int,string) Idioma { get; set; }
    public (int, string) Rubro { get; set; }

    // Contenido del archivo en binario
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
