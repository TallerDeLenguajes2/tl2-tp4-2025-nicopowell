using System.Text.Json;

public class AccesoADatosJSON : IAccesoADatos

{
    private readonly string archivoCadeteria;
    private readonly string archivoCadetes;
    private readonly string archivoPedidos;

    public AccesoADatosJSON(string archivoCadeteria, string archivoCadetes, string archivoPedidos)
    {
        this.archivoCadeteria = archivoCadeteria;
        this.archivoCadetes = archivoCadetes;
        this.archivoPedidos = archivoPedidos;
    }

    public Cadeteria LeerCadeteria()
    {
        try
        {
            // Leer cadetería
            string jsonCadeteria = File.ReadAllText(archivoCadeteria);
            Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonCadeteria);

            // Leer cadetes
            string jsonCadetes = File.ReadAllText(archivoCadetes);
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonCadetes);

            // Leer pedidos
            string jsonPedidos = File.ReadAllText(archivoPedidos);
            List<Pedido> pedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonPedidos);

            cadeteria.ListadoCadetes = cadetes;

            if (cadeteria.ListadoPedidos == null)
                cadeteria.ListadoPedidos = new List<Pedido>();

            return cadeteria;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void GuardarCadeteria(Cadeteria cadeteria)
    {
        string jsonCadeteria = JsonSerializer.Serialize<Cadeteria>(cadeteria);
        string jsonCadetes = JsonSerializer.Serialize<List<Cadete>>(cadeteria.ListadoCadetes);
        string jsonPedidos = JsonSerializer.Serialize<List<Pedido>>(cadeteria.ListadoPedidos);
    }
}
