namespace Sistema
{
    public class SistemaCadeteria
    {
        public static string MostrarCadeteria(Cadeteria cadeteria)
        {
            string cadena = $"Nombre: {cadeteria.Nombre}, Telefono: {cadeteria.Telefono}";
            cadena += MostrarCadetes(cadeteria.ListadoCadetes);
            return cadena;
        }

        public static string MostrarCadetes(List<Cadete> cadetes)
        {
            string cadena = "";
            foreach (Cadete cadete in cadetes)
            {
                cadena += $"\n[Cadete {cadete.Id}, Nombre: {cadete.Nombre}, Direccion: {cadete.Direccion}, Telefono: {cadete.Telefono}]\n";
            }
            return cadena;
        }

        public static string MostrarPedidos(Cadeteria cadeteria)
        {
            string cadena = "";
            foreach (Pedido pedido in cadeteria.ListadoPedidos)
            {
                cadena += $"\n[Numero: {pedido.Nro}, Observaciones: {pedido.Obs}, Cliente: {pedido.Cliente.Nombre}, Estado: {pedido.Estado}, Cadete: {(pedido.CadeteAsignado != null ? pedido.CadeteAsignado.Nombre : "Sin asignar")}]";
            }
            return cadena;
        }

        public static List<Pedido> ObtenerPedidos(Cadeteria cadeteria)
        {
            return cadeteria.ListadoPedidos;
        }

        public static Pedido AltaPedido(int nro, string obs, string nombreCliente, string direccionCliente, long telefonoCliente, string datosReferenciaDireccionCliente)
        {
            Pedido pedido = new(nro, obs, "Pendiente", nombreCliente, direccionCliente, telefonoCliente, datosReferenciaDireccionCliente);

            return pedido;
        }

      
        public static bool CambiarEstadoPedido(Cadeteria cadeteria, int id)
        {
            var pedido = cadeteria.ListadoPedidos.Find(p => p.Nro == id);
            if (pedido != null)
            {
                if (pedido.Estado == "Pendiente")
                {
                    pedido.Estado = "Completado";
                }
                return true;
            }
            return false;
        }

        public static bool AsignarPedido(Cadeteria cadeteria, int numeroPedido, int idCadete)
        {
            var sinAsignar = cadeteria.ListadoPedidos.Where(p => p.CadeteAsignado == null).ToList();
            if (sinAsignar.Count == 0)
            {
                return false;
            }

            bool ok = cadeteria.AsignarCadeteAPedido(idCadete, numeroPedido);
            if (ok)
            {
                return true;
            }
            return false;
        }

        public static bool CambiarCadete(Cadeteria cadeteria, int idPedido, int idCadete)
        {
            var pedido = cadeteria.ListadoPedidos.Find(p => p.Nro == idPedido);
            if (pedido != null)
            {
                if (pedido.Estado != "Completado")
                {
                    bool ok = cadeteria.AsignarCadeteAPedido(idCadete, idPedido);
                    if (ok)
                    {
                        return true;
                    }
                    return false;

                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public static string ObtenerInforme(Cadeteria cadeteria)
        {
            string cadena = "";
            int pedidosEntregadosTotales = cadeteria.ListadoPedidos.Count(p => p.Estado == "Completado");
            int pedidosPendientesTotales = cadeteria.ListadoPedidos.Count(p => p.Estado != "Completado");

            foreach (Cadete cadete in cadeteria.ListadoCadetes)
            {
                int entregadosCadete = cadeteria.ListadoPedidos.Count(p => p.CadeteAsignado != null && p.CadeteAsignado.Id == cadete.Id && p.Estado == "Completado");
                int pendientesCadete = cadeteria.ListadoPedidos.Count(p => p.CadeteAsignado != null && p.CadeteAsignado.Id == cadete.Id && p.Estado != "Completado");

                cadena += $"\n- [Cadete {cadete.Nombre}, Monto ganado: ${cadeteria.JornalACobrar(cadete.Id)}, Envios entregados: {entregadosCadete}, Envios no entregados: {pendientesCadete}]";
            }

            cadena += $"\nEnvios entregados totales: {pedidosEntregadosTotales}";
            cadena += $"\nEnvios pendientes totales: {pedidosPendientesTotales}";
            double promedio = cadeteria.ListadoCadetes.Count > 0 ? (double)(pedidosEntregadosTotales + pedidosPendientesTotales) / cadeteria.ListadoCadetes.Count : 0;
            cadena += $"\nPromedio de envios por cadete: {promedio}";

            return cadena;
        }
    } 
}
