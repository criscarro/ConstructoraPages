using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Service;
using Microsoft.AspNetCore.Mvc;

namespace ConstructoraPages.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleadoController(EmpleadoService empleadoService) : ControllerBase
    {
        private readonly EmpleadoService _empleadoService = empleadoService;

        [HttpGet]
        public ActionResult<List<Empleado>> ObtenerTodos()
        {
            return Ok(_empleadoService.ObtenerTodos());
        }

        [HttpGet("{id}")]
        public ActionResult<Empleado> ObtenerPorId(int id)
        {
            var empleado = _empleadoService.ObtenerPorId(id);
            return empleado is null ? NotFound() : Ok(empleado);
        }

        [HttpPost]
        public ActionResult<Empleado> Crear(AltaEmpleadoDto dto)
        {
            try
            {
                var empleadoCreado = _empleadoService.Crear(dto);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = empleadoCreado.Id }, empleadoCreado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Empleado> Modificar(int id, ModificarEmpleadoDto dto)
        {
            try
            {
                var empleadoModificado = _empleadoService.Modificar(id, dto);
                return empleadoModificado is null ? NotFound() : Ok(empleadoModificado);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(new { mensaje = exception.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return _empleadoService.Eliminar(id) ? NoContent() : NotFound();
        }
    }
}
