using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class EtapaObraController(EtapaObraService etapaObraService) : ControllerBase
    {
        private readonly EtapaObraService _etapaObraService = etapaObraService;

        [HttpGet]
        public ActionResult<List<EtapaObra>> ObtenerTodos()
        {
            return Ok(_etapaObraService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<EtapaObra> ObtenerPorId(int id)
        {
            var etapaObra = _etapaObraService.ObtenerPorId(id);
            return etapaObra is null ? NotFound() : Ok(etapaObra);
        }

        [HttpPost]
        public ActionResult<EtapaObra> Crear(AltaEtapaObraDto dto)
        {
            try
            {
                var etapaObraCreado = _etapaObraService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = etapaObraCreado.Id }, etapaObraCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<EtapaObra> Modificar(int id, ModificarEtapaObraDto dto)
        {
            try
            {
                var etapaObraModificado = _etapaObraService.Modificar(id, dto);
                return etapaObraModificado is null ? NotFound() : Ok(etapaObraModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _etapaObraService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
