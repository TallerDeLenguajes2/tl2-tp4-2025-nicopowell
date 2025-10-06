public class Pedido
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private string estado;
    private Cadete cadeteAsignado;

    public int Nro { get => nro; set => nro = value; }
    public string Obs { get => obs; set => obs = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }
    public string Estado { get => estado; set => estado = value; }
    public Cadete CadeteAsignado { get => cadeteAsignado; set => cadeteAsignado = value; }

    public Pedido()
    {
        Estado = "Pendiente";
        CadeteAsignado = null;
    }
    public Pedido(int nro, string obs, string estado, string nombreCliente, string direccionCliente, long telefonoCliente, string datosReferenciaDireccionCliente)
    {
        this.nro = nro;
        this.Obs = obs;
        this.cliente = new(nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);
        this.Estado = estado;
        this.cadeteAsignado = null;
    }
    public string VerDireccionCliente()
    {
        return Cliente.Direccion;
    }

    public Cliente VerDatosCliente()
    {
        return Cliente;
    }
}