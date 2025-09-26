public class Cliente
{
    private string nombre;
    private string direccion;
    private long telefono;
    private string datosReferenciaDireccion;

    public Cliente(string nombre, string direccion, long telefono, string datosReferenciaDireccion)
    {
        this.nombre = nombre;
        this.direccion = direccion;
        this.telefono = telefono;
        this.datosReferenciaDireccion = datosReferenciaDireccion;
    }

    public string Direccion => direccion;

    public string Nombre => nombre;
    public long Telefono => telefono;
    public string DatosReferenciaDireccion => datosReferenciaDireccion;
}