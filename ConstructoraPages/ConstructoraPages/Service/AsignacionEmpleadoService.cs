using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class AsignacionEmpleadoService(
    AsignacionEmpleadoRepository asignacionEmpleadoRepository, 
    EmpleadoRepository empleadoRepository, 
    ObraRepository obraRepository)
{
    private readonly AsignacionEmpleadoRepository _asignacionEmpleadoRepository = asignacionEmpleadoRepository;
    private readonly EmpleadoRepository _empleadoRepository = empleadoRepository;
    private readonly ObraRepository _obraRepository = obraRepository;

    public List<AsignacionEmpleado> ObtenerTodos()
    {
        return _asignacionEmpleadoRepository.ObtenerTodos();
    }

    public AsignacionEmpleado? ObtenerPorId(int id)
    {
        return _asignacionEmpleadoRepository.ObtenerPorId(id);
    }

    public AsignacionEmpleado Crear(AltaAsignacionEmpleadoDto dto)
    {
        var empleado = _empleadoRepository.ObtenerPorId(dto.EmpleadoId) ?? 
            throw new ArgumentException("El empleado especificado no existe.");
            
        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        var asignacionEmpleado = new AsignacionEmpleado
        {
            FechaAsignacion = dto.FechaAsignacion,
            Empleado = empleado,
            Obra = obra
        };
        Validar(asignacionEmpleado);
        return _asignacionEmpleadoRepository.Crear(asignacionEmpleado);
    }

    public AsignacionEmpleado? Modificar(int id, ModificarAsignacionEmpleadoDto dto)
    {
        var existente = _asignacionEmpleadoRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        var empleado = _empleadoRepository.ObtenerPorId(dto.EmpleadoId) ?? 
            throw new ArgumentException("El empleado especificado no existe.");
            
        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        existente.FechaAsignacion = dto.FechaAsignacion;
        existente.Empleado = empleado;
        existente.Obra = obra;

        Validar(existente);
        _asignacionEmpleadoRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var asignacionEmpleado = _asignacionEmpleadoRepository.ObtenerPorId(id);
        if (asignacionEmpleado is null)
        {
            return false;
        }

        _asignacionEmpleadoRepository.Eliminar(asignacionEmpleado);
        return true;
    }

    private static void Validar(AsignacionEmpleado asignacionEmpleado)
    {
        // Validaciones en las propiedades directas (si las hay)
    }
}
