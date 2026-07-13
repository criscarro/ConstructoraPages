using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class AsignacionEmpleadoController(AsignacionEmpleadoService asignacionEmpleadoService) : ControllerBase
    {
        private readonly AsignacionEmpleadoService _asignacionEmpleadoService = asignacionEmpleadoService;

        [HttpGet]
        public ActionResult<List<AsignacionEmpleado>> ObtenerTodos()
        {
            return Ok(_asignacionEmpleadoService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<AsignacionEmpleado> ObtenerPorId(int id)
        {
            var asignacionEmpleado = _asignacionEmpleadoService.ObtenerPorId(id);
            return asignacionEmpleado is null ? NotFound() : Ok(asignacionEmpleado);
        }

        [HttpPost]
        public ActionResult<AsignacionEmpleado> Crear(AltaAsignacionEmpleadoDto dto)
        {
            try
            {
                var asignacionEmpleadoCreado = _asignacionEmpleadoService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = asignacionEmpleadoCreado.Id }, asignacionEmpleadoCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<AsignacionEmpleado> Modificar(int id, ModificarAsignacionEmpleadoDto dto)
        {
            try
            {
                var asignacionEmpleadoModificado = _asignacionEmpleadoService.Modificar(id, dto);
                return asignacionEmpleadoModificado is null ? NotFound() : Ok(asignacionEmpleadoModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _asignacionEmpleadoService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
