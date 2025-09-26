public class Cadeteria
{
    private string nombre;
    private long telefono;
    private List<Cadete> listadoCadetes;
    private List<Pedido> listadoPedidos;


    public Cadeteria(string nombre, long telefono, List<Cadete> listadoCadetes)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        this.listadoCadetes = listadoCadetes;
        this.listadoPedidos = new();
    }

    public List<Cadete> ListadoCadetes { get => listadoCadetes; set => listadoCadetes = value; }
    public List<Pedido> ListadoPedidos { get => listadoPedidos; set => listadoPedidos = value; }
    public string Nombre { get => nombre; }
    public long Telefono { get => telefono; }

    public double JornalACobrar(int idCadete)
    {
        return 500 * this.ListadoPedidos.Count(p => p.CadeteAsignado != null && p.CadeteAsignado.Id == idCadete && p.Estado == "Completado");
    }
    
    public bool AsignarCadeteAPedido(int idCadete, int idPedido)
    {
        var cadete = listadoCadetes.FirstOrDefault(c => c.Id == idCadete);
        var pedido = listadoPedidos.FirstOrDefault(p => p.Nro == idPedido);

        if (cadete == null || pedido == null) return false;
        if (pedido.Estado == "Completado") return false;

        pedido.CadeteAsignado = cadete;
        return true;
    }
}