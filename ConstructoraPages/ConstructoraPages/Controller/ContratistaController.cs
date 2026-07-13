using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ContratistaController(ContratistaService contratistaService) : ControllerBase
    {
        private readonly ContratistaService _contratistaService = contratistaService;

        [HttpGet]
        public ActionResult<List<Contratista>> ObtenerTodos()
        {
            return Ok(_contratistaService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Contratista> ObtenerPorId(int id)
        {
            var contratista = _contratistaService.ObtenerPorId(id);
            return contratista is null ? NotFound() : Ok(contratista);
        }

        [HttpPost]
        public ActionResult<Contratista> Crear(AltaContratistaDto dto)
        {
            try
            {
                var contratistaCreado = _contratistaService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = contratistaCreado.Id }, contratistaCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Contratista> Modificar(int id, ModificarContratistaDto dto)
        {
            try
            {
                var contratistaModificado = _contratistaService.Modificar(id, dto);
                return contratistaModificado is null ? NotFound() : Ok(contratistaModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _contratistaService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
