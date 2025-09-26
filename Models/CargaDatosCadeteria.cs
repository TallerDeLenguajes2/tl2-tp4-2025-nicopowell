namespace Sistema
{
    public class CargaDatosCadeteria
    {
        public static Cadeteria LeerArchivoCadeteria(string nombreArchivo)
        {
            try
            {
                using (StreamReader sr = new(nombreArchivo))
                {
                    string linea = sr.ReadLine(); // suponiendo que solo hay 1 línea
                    if (linea != null)
                    {
                        string[] datos = linea.Split(",");
                        if (long.TryParse(datos[1], out long telefono))
                        {
                            return new Cadeteria(datos[0], telefono, new List<Cadete>());
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return null;
        }
    }
}