using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProveedorController(ProveedorService proveedorService) : ControllerBase
    {
        private readonly ProveedorService _proveedorService = proveedorService;

        [HttpGet]
        public ActionResult<List<Proveedor>> ObtenerTodos()
        {
            return Ok(_proveedorService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Proveedor> ObtenerPorId(int id)
        {
            var proveedor = _proveedorService.ObtenerPorId(id);
            return proveedor is null ? NotFound() : Ok(proveedor);
        }

        [HttpPost]
        public ActionResult<Proveedor> Crear(AltaProveedorDto dto)
        {
            try
            {
                var proveedorCreado = _proveedorService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = proveedorCreado.Id }, proveedorCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Proveedor> Modificar(int id, ModificarProveedorDto dto)
        {
            try
            {
                var proveedorModificado = _proveedorService.Modificar(id, dto);
                return proveedorModificado is null ? NotFound() : Ok(proveedorModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _proveedorService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
