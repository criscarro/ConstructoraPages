using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class CompraMaterialController(CompraMaterialService compraMaterialService) : ControllerBase
    {
        private readonly CompraMaterialService _compraMaterialService = compraMaterialService;

        [HttpGet]
        public ActionResult<List<CompraMaterial>> ObtenerTodos()
        {
            return Ok(_compraMaterialService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<CompraMaterial> ObtenerPorId(int id)
        {
            var compraMaterial = _compraMaterialService.ObtenerPorId(id);
            return compraMaterial is null ? NotFound() : Ok(compraMaterial);
        }

        [HttpPost]
        public ActionResult<CompraMaterial> Crear(AltaCompraMaterialDto dto)
        {
            try
            {
                var compraMaterialCreado = _compraMaterialService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = compraMaterialCreado.Id }, compraMaterialCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<CompraMaterial> Modificar(int id, ModificarCompraMaterialDto dto)
        {
            try
            {
                var compraMaterialModificado = _compraMaterialService.Modificar(id, dto);
                return compraMaterialModificado is null ? NotFound() : Ok(compraMaterialModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _compraMaterialService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
