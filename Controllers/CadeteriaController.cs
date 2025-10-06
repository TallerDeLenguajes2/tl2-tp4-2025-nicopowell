using Microsoft.AspNetCore.Mvc;
using Sistema;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private AccesoADatosJSON _datosCadeteria;
    public CadeteriaController()
    {
        _datosCadeteria = new AccesoADatosJSON("./cadeteria.json", "./cadetes.json", "./pedidos.json");
    }

    [HttpGet("Pedidos")]
    public IActionResult GetPedidos()
    {
        var cadeteria = _datosCadeteria.LeerCadeteria();
        return Ok(cadeteria.ListadoPedidos);
    }

    [HttpGet("Cadetes")]
    public IActionResult GetCadetes()
    {
        var cadeteria = _datosCadeteria.LeerCadeteria();
        return Ok(cadeteria.ListadoCadetes);
    }

    [HttpGet("Informe")]
    public IActionResult GetInforme()
    {
        var cadeteria = _datosCadeteria.LeerCadeteria();
        return Ok(SistemaCadeteria.ObtenerInforme(cadeteria));
    }

    [HttpPost("AltaPedido")]
    public IActionResult AgregarPedido(Pedido pedido)
    {
        // Validaciones
        if (string.IsNullOrEmpty(pedido.Obs)) return BadRequest("Las observaciones no pueden ser vacias");

        var cadeteria = _datosCadeteria.LeerCadeteria();
        cadeteria.AltaPedido(pedido);
        _datosCadeteria.GuardarCadeteria(cadeteria);
        return Created();
    }

    [HttpPut("AsignarPedido")]
    public IActionResult AsignarPedido(int idPedido, int idCadete)
    {
        var cadeteria = _datosCadeteria.LeerCadeteria();

        bool asignacionExitosa = cadeteria.AsignarCadeteAPedido(idCadete, idPedido);

        if (asignacionExitosa)
        {
            _datosCadeteria.GuardarCadeteria(cadeteria);
            var pedido = cadeteria.ListadoPedidos.Find(p => p.Nro == idPedido);
            return Ok(pedido);
        }
        else
        {
            return BadRequest("No se pudo asignar el pedido");
        }
    }

    [HttpPut("CambiarEstadoPedido")]
    public IActionResult CambiarEstadoPedido(int idPedido)
    {
        var cadeteria = _datosCadeteria.LeerCadeteria();

        var pedido = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);

        if (pedido == null)
        {
            return NotFound("No se encontro el pedido");
        }

        if (pedido.CadeteAsignado == null)
        {
            return BadRequest("No se puede cambiar el estado del pedido no asignado");
        }

        pedido.Estado = pedido.Estado == "Pendiente" ? "Completado" : "Pendiente";

        _datosCadeteria.GuardarCadeteria(cadeteria);
        return Ok(pedido);
    }

    [HttpPut("CambiarCadetePedido")]
    public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var cadeteria = _datosCadeteria.LeerCadeteria();

        // Buscar el pedido
        var pedido = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedido == null)
        {
            return NotFound("No se encontró el pedido");
        }

        // Buscar el nuevo cadete
        var nuevoCadete = cadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idNuevoCadete);
        if (nuevoCadete == null)
        {
            return NotFound("No se encontró el cadete");
        }

        // Validar que el pedido esté asignado (tiene un cadete actual)
        if (pedido.CadeteAsignado == null)
        {
            return BadRequest("El pedido no está asignado a ningún cadete. Use AsignarPedido en su lugar.");
        }

        // Validar que no se esté asignando al mismo cadete
        if (pedido.CadeteAsignado.Id == idNuevoCadete)
        {
            return BadRequest("El pedido ya está asignado a este cadete");
        }

        // Validar que el pedido no esté completado
        if (pedido.Estado == "Completado")
        {
            return BadRequest("No se puede cambiar el cadete de un pedido completado");
        }

        // Cambiar el cadete
        pedido.CadeteAsignado = nuevoCadete;

        _datosCadeteria.GuardarCadeteria(cadeteria);

        return Ok(new
        {
            mensaje = "Cadete cambiado correctamente",
            pedidoId = pedido.Nro,
            cadeteAnteriorId = pedido.CadeteAsignado.Id, // Sería bueno guardar el anterior antes de cambiar
            nuevoCadeteId = nuevoCadete.Id,
            nuevoCadeteNombre = nuevoCadete.Nombre
        });
    }
}