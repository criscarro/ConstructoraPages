using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Entity.Enums;
using ConstructoraPages.Repository;

using System.Text.RegularExpressions;

namespace ConstructoraPages.Service;

public class EmpleadoService(EmpleadoRepository empleadoRepository)
{
    private readonly EmpleadoRepository _empleadoRepository = empleadoRepository;

    public List<Empleado> ObtenerTodos()
    {
        return _empleadoRepository.ObtenerTodos();
    }

    public Empleado? ObtenerPorId(int id)
    {
        return _empleadoRepository.ObtenerPorId(id);
    }

    public Empleado Crear(AltaEmpleadoDto dto)
    {
        var empleado = new Empleado
        {
            Nombre = dto.Nombre,
            Cedula = dto.Cedula,
            Telefono = dto.Telefono,
            Cargo = dto.Cargo,
            Salario = dto.Salario
        };
        Validar(empleado);
        return _empleadoRepository.Crear(empleado);
    }

    public Empleado? Modificar(int id, ModificarEmpleadoDto dto)
    {
        var existente = _empleadoRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        existente.Nombre = dto.Nombre;
        existente.Cedula = dto.Cedula;
        existente.Telefono = dto.Telefono;
        existente.Cargo = dto.Cargo;
        existente.Salario = dto.Salario;

        Validar(existente);
        _empleadoRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var empleado = _empleadoRepository.ObtenerPorId(id);
        if (empleado is null)
        {
            return false;
        }

        _empleadoRepository.Eliminar(empleado);
        return true;
    }

    private static void Validar(Empleado empleado)
    {
        if (string.IsNullOrWhiteSpace(empleado.Nombre))
            throw new ArgumentException("El nombre del empleado es obligatorio.");

        if (string.IsNullOrWhiteSpace(empleado.Cedula))
            throw new ArgumentException("La cédula del empleado es obligatoria.");

        if (!Regex.IsMatch(empleado.Cedula, @"^\d{8}$"))
            throw new ArgumentException("La cédula debe tener exactamente 8 dígitos numéricos.");

        if (string.IsNullOrWhiteSpace(empleado.Telefono))
            throw new ArgumentException("El teléfono del empleado es obligatorio.");

        if (!Regex.IsMatch(empleado.Telefono, @"^\d+$"))
            throw new ArgumentException("El teléfono debe contener solo números.");

        if (!Enum.IsDefined(typeof(CargoEmpleado), empleado.Cargo))
            throw new ArgumentException("El cargo del empleado no es válido.");

        if (empleado.Salario <= 0)
            throw new ArgumentException("El salario debe ser mayor a cero.");
    }
}
