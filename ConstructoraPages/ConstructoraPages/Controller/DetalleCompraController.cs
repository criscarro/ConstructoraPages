using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class DetalleCompraController(DetalleCompraService detalleCompraService) : ControllerBase
    {
        private readonly DetalleCompraService _detalleCompraService = detalleCompraService;

        [HttpGet]
        public ActionResult<List<DetalleCompra>> ObtenerTodos()
        {
            return Ok(_detalleCompraService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<DetalleCompra> ObtenerPorId(int id)
        {
            var detalleCompra = _detalleCompraService.ObtenerPorId(id);
            return detalleCompra is null ? NotFound() : Ok(detalleCompra);
        }

        [HttpPost]
        public ActionResult<DetalleCompra> Crear(AltaDetalleCompraDto dto)
        {
            try
            {
                var detalleCompraCreado = _detalleCompraService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = detalleCompraCreado.Id }, detalleCompraCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<DetalleCompra> Modificar(int id, ModificarDetalleCompraDto dto)
        {
            try
            {
                var detalleCompraModificado = _detalleCompraService.Modificar(id, dto);
                return detalleCompraModificado is null ? NotFound() : Ok(detalleCompraModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _detalleCompraService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
