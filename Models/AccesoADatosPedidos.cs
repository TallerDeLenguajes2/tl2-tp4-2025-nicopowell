using System.Text.Json;

public class AccesoADatosPedidos
{
    private readonly string archivoPedidos;

    public AccesoADatosPedidos(string archivoPedidos = "./pedidos.json")
    {
        this.archivoPedidos = archivoPedidos;
    }

    public List<Pedido> Obtener()
    {

        if (!File.Exists(archivoPedidos))
        {
            return new List<Pedido>();
        }

        string jsonPedidos = File.ReadAllText(archivoPedidos);

        if (string.IsNullOrWhiteSpace(jsonPedidos) || jsonPedidos.Trim() == "{}")
        {
            return new List<Pedido>();
        }

        return JsonSerializer.Deserialize<List<Pedido>>(jsonPedidos) ?? new List<Pedido>();

    }

    public void Guardar(List<Pedido> pedidos)
    {
        string jsonPedidos = JsonSerializer.Serialize(pedidos ?? new List<Pedido>());
        File.WriteAllText(archivoPedidos, jsonPedidos);

    }
}