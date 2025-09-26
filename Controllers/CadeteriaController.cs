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

    // [HttpPut]
    // public IActionResult AsignarPedido(int idPedido, int NuevoEstado)
    // {
    //     return Ok();
    // }

    // public IActionResult CambiarCadetePedido(int idPedido, int idNuevoCadete)
    // {
    //     return Ok();
    // }
}