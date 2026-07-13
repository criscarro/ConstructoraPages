
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ObraController(ObraService obraService) : ControllerBase
    {
        private readonly ObraService _obraService = obraService;

        [HttpGet]
        public ActionResult<List<Obra>> ObtenerTodos()
        {
            return Ok(_obraService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Obra> ObtenerPorId(int id)
        {
            var obra = _obraService.ObtenerPorId(id);
            return obra is null ? NotFound() : Ok(obra);
        }

        [HttpPost]
        public ActionResult<Obra> Crear(Obra obra)
        {
            try
            {
                var obraCreada = _obraService.Crear(obra);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = obraCreada.Id }, obraCreada);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPost("{obraId}/clientes/{clienteId}")]
        public IActionResult AsignarCliente(int obraId, int clienteId)
        {
            var resultado = _obraService.AsignarCliente(obraId, clienteId);
            return resultado ? NoContent() : NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<Obra> Modificar(int id, Obra obra)
        {
            try
            {
                var obraModificada = _obraService.Modificar(id, obra);
                return obraModificada is null ? NotFound() : Ok(obraModificada);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _obraService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
