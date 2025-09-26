public class AccesoADatosCSV : IAccesoADatos
{
    public Cadeteria LeerCadeteria(string archivoCadeteria, string archivoCadetes)
    {
        Cadeteria cadeteria = null;

        try
        {
            using (StreamReader sr = new(archivoCadeteria))
            {
                string linea = sr.ReadLine();
                if (linea != null)
                {
                    string[] datos = linea.Split(",");
                    if (long.TryParse(datos[1], out long telefono))
                    {
                        cadeteria = new Cadeteria(datos[0], telefono, new List<Cadete>());
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            List<Cadete> cadetes = new();
            using (StreamReader sr = new(archivoCadetes))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] datos = linea.Split(',');
                    if (int.TryParse(datos[0], out int id) && long.TryParse(datos[3], out long telefono))
                    {
                        cadetes.Add(new Cadete(id, datos[1], datos[2], telefono));
                    }
                }
            }
            cadeteria.ListadoCadetes = cadetes;
        }
        catch (Exception)
        {

        }

        return cadeteria;
    }
}
