using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class MaterialController(MaterialService materialService) : ControllerBase
    {
        private readonly MaterialService _materialService = materialService;

        [HttpGet]
        public ActionResult<List<Material>> ObtenerTodos()
        {
            return Ok(_materialService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Material> ObtenerPorId(int id)
        {
            var material = _materialService.ObtenerPorId(id);
            return material is null ? NotFound() : Ok(material);
        }

        [HttpPost]
        public ActionResult<Material> Crear(AltaMaterialDto dto)
        {
            try
            {
                var materialCreado = _materialService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = materialCreado.Id }, materialCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Material> Modificar(int id, ModificarMaterialDto dto)
        {
            try
            {
                var materialModificado = _materialService.Modificar(id, dto);
                return materialModificado is null ? NotFound() : Ok(materialModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _materialService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
