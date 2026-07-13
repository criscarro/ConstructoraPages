using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ContratistaObraController(ContratistaObraService contratistaObraService) : ControllerBase
    {
        private readonly ContratistaObraService _contratistaObraService = contratistaObraService;

        [HttpGet]
        public ActionResult<List<ContratistaObra>> ObtenerTodos()
        {
            return Ok(_contratistaObraService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<ContratistaObra> ObtenerPorId(int id)
        {
            var contratistaObra = _contratistaObraService.ObtenerPorId(id);
            return contratistaObra is null ? NotFound() : Ok(contratistaObra);
        }

        [HttpPost]
        public ActionResult<ContratistaObra> Crear(AltaContratistaObraDto dto)
        {
            try
            {
                var contratistaObraCreado = _contratistaObraService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = contratistaObraCreado.Id }, contratistaObraCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ContratistaObra> Modificar(int id, ModificarContratistaObraDto dto)
        {
            try
            {
                var contratistaObraModificado = _contratistaObraService.Modificar(id, dto);
                return contratistaObraModificado is null ? NotFound() : Ok(contratistaObraModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _contratistaObraService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
