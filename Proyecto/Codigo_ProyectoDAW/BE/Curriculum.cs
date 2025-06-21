using BE;

public class Curriculum
{
    public int ID_CV { get; set; }
    public Usuario Usuario { get; set; }

    // Contenido del archivo en binario
    public byte[] ArchivoCV { get; set; }

    public Curriculum(int numCurriculum, Usuario pUsuario, byte[] archivoCV)
    {
        ID_CV = numCurriculum;
        Usuario = pUsuario;
        ArchivoCV = archivoCV;
    }

    public Curriculum() { }
}
