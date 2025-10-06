using Microsoft.AspNetCore.Mvc;
using Sistema;

[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;
    private AccesoADatosCadeteria ADCadeteria;
    private AccesoADatosCadetes ADCadetes;
    private AccesoADatosPedidos ADPedidos;

    public CadeteriaController()
    {
        ADCadeteria = new AccesoADatosCadeteria("./cadeteria.json");
        ADCadetes = new AccesoADatosCadetes("./cadetes.json");
        ADPedidos = new AccesoADatosPedidos("./pedidos.json");
        
        // Inicializar la cadetería según el TP5
        cadeteria = ADCadeteria.LeerCadeteria();
        cadeteria.AgregarListaCadetes(ADCadetes.Obtener());
        cadeteria.AgregarListaPedidos(ADPedidos.Obtener());
    }

    [HttpGet("Pedidos")]
    public IActionResult GetPedidos()
    {
        return Ok(cadeteria.ListadoPedidos);
    }

    [HttpGet("Cadetes")]
    public IActionResult GetCadetes()
    {
        return Ok(cadeteria.ListadoCadetes);
    }

    [HttpGet("Informe")]
    public IActionResult GetInforme()
    {
        return Ok(SistemaCadeteria.ObtenerInforme(cadeteria));
    }

    [HttpPost("AltaPedido")]
    public IActionResult AgregarPedido(Pedido pedido)
    {
        // Validaciones
        if (string.IsNullOrEmpty(pedido.Obs)) 
            return BadRequest("Las observaciones no pueden ser vacias");

        cadeteria.AltaPedido(pedido);
        ADPedidos.Guardar(cadeteria.ListaPedidos()); // Guardar después de agregar
        return Created();
    }

    [HttpPut("AsignarPedido")]
    public IActionResult AsignarPedido(int idPedido, int idCadete)
    {
        bool asignacionExitosa = cadeteria.AsignarCadeteAPedido(idCadete, idPedido);

        if (asignacionExitosa)
        {
            ADPedidos.Guardar(cadeteria.ListaPedidos()); // Guardar después de asignar
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
        ADPedidos.Guardar(cadeteria.ListaPedidos()); // Guardar después de cambiar estado
        return Ok(pedido);
    }

    [HttpPut("CambiarCadetePedido")]
    public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    {
        var pedido = cadeteria.ListadoPedidos.FirstOrDefault(p => p.Nro == idPedido);
        if (pedido == null)
        {
            return NotFound("No se encontró el pedido");
        }

        var nuevoCadete = cadeteria.ListadoCadetes.FirstOrDefault(c => c.Id == idNuevoCadete);
        if (nuevoCadete == null)
        {
            return NotFound("No se encontró el cadete");
        }

        if (pedido.CadeteAsignado == null)
        {
            return BadRequest("El pedido no está asignado a ningún cadete. Use AsignarPedido en su lugar.");
        }

        if (pedido.CadeteAsignado.Id == idNuevoCadete)
        {
            return BadRequest("El pedido ya está asignado a este cadete");
        }

        if (pedido.Estado == "Completado")
        {
            return BadRequest("No se puede cambiar el cadete de un pedido completado");
        }

        pedido.CadeteAsignado = nuevoCadete;
        ADPedidos.Guardar(cadeteria.ListaPedidos()); // Guardar después de cambiar cadete
        
        return Ok(new { 
            mensaje = "Cadete cambiado correctamente", 
            pedidoId = pedido.Nro,
            nuevoCadeteId = nuevoCadete.Id,
            nuevoCadeteNombre = nuevoCadete.Nombre
        });
    }
}