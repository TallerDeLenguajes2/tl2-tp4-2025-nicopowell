public class Pedido
{
    private int nro;
    private string obs;
    private Cliente cliente;
    private string estado;
    private Cadete cadeteAsignado;

    public int Nro { get => nro; }
    public string Obs { get => obs; }
    public Cliente Cliente { get => cliente; }
    public string Estado { get => estado; set => estado = value; }
    public Cadete CadeteAsignado { get => cadeteAsignado; set => cadeteAsignado = value; }

    public Pedido(int nro, string obs, string estado, string nombreCliente, string direccionCliente, long telefonoCliente, string datosReferenciaDireccionCliente)
    {
        this.nro = nro;
        this.obs = obs;
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