using System.Text.Json;

public class AccesoADatosCadeteria : IAccesoADatos
{
    private readonly string archivoCadeteria;

    public AccesoADatosCadeteria(string archivoCadeteria = "./cadeteria.json")
    {
        this.archivoCadeteria = archivoCadeteria;
    }

    public Cadeteria LeerCadeteria()
    {

        if (!File.Exists(archivoCadeteria))
        {
            return new Cadeteria("Cadeteria Default", 0, new List<Cadete>());
        }

        string jsonCadeteria = File.ReadAllText(archivoCadeteria);
        var cadeteriaData = JsonSerializer.Deserialize<Cadeteria>(jsonCadeteria);

        return new Cadeteria(cadeteriaData.Nombre, cadeteriaData.Telefono, new List<Cadete>());

    }

    public void GuardarCadeteria(Cadeteria cadeteria)
    {
        var cadeteriaData = new
        {
            Nombre = cadeteria.Nombre,
            Telefono = cadeteria.Telefono
        };

        string jsonCadeteria = JsonSerializer.Serialize(cadeteriaData);
        File.WriteAllText(archivoCadeteria, jsonCadeteria);
    }

}