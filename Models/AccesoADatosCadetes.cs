using System.Text.Json;

public class AccesoADatosCadetes
{
    private readonly string archivoCadetes;

    public AccesoADatosCadetes(string archivoCadetes = "./cadetes.json")
    {
        this.archivoCadetes = archivoCadetes;
    }

    public List<Cadete> Obtener()
    {
        if (!File.Exists(archivoCadetes))
        {
            return new List<Cadete>();
        }

        string jsonCadetes = File.ReadAllText(archivoCadetes);

        if (string.IsNullOrWhiteSpace(jsonCadetes) || jsonCadetes.Trim() == "{}")
        {
            return new List<Cadete>();
        }

        return JsonSerializer.Deserialize<List<Cadete>>(jsonCadetes) ?? new List<Cadete>();
    }
}