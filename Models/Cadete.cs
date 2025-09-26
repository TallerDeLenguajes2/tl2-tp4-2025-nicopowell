public class Cadete
{
    private int id;
    private string nombre;
    private string direccion;
    private long telefono;

    public Cadete(int id, string nombre, string direccion, long telefono)
    {
        this.id = id;
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
    }

    public int Id { get => id; }
    public string Direccion { get => direccion; }
    public long Telefono { get => telefono; }
    public string Nombre { get => nombre; }
}