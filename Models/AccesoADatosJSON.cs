using System.Text.Json;

public class AccesoADatosJSON : IAccesoADatos
{
    public Cadeteria LeerCadeteria(string archivoCadeteria, string archivoCadetes)
    {
        try
        {
            // Leer cadetería
            string jsonCadeteria = File.ReadAllText(archivoCadeteria);
            Cadeteria cadeteria = JsonSerializer.Deserialize<Cadeteria>(jsonCadeteria);

            // Leer cadetes
            string jsonCadetes = File.ReadAllText(archivoCadetes);
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(jsonCadetes);

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
}
