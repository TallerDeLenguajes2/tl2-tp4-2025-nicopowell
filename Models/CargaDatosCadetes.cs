public class CargaDatosCadetes
{
    public static List<Cadete> LeerArchivoCadetes(string nombreArchivo)
    {
        List<Cadete> cadetes = new();
        try
        {
            using (StreamReader sr = new(nombreArchivo))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');
                    if (int.TryParse(datos[0], out int id) && long.TryParse(datos[3], out long telefono))
                    {
                        Cadete cadete = new(id, datos[1], datos[2], telefono);
                        cadetes.Add(cadete);
                    }
                }
            }
        }
        catch (Exception )
        {
        }

        return cadetes;
    }

}
