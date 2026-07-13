using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Repository;

using System.Text.RegularExpressions;

namespace ConstructoraPages.Service;

public class ProveedorService(ProveedorRepository proveedorRepository)
{
    private readonly ProveedorRepository _proveedorRepository = proveedorRepository;

    public List<Proveedor> ObtenerTodos()
    {
        return _proveedorRepository.ObtenerTodos();
    }

    public Proveedor? ObtenerPorId(int id)
    {
        return _proveedorRepository.ObtenerPorId(id);
    }

    public Proveedor Crear(AltaProveedorDto dto)
    {
        var proveedor = new Proveedor
        {
            Nombre = dto.Nombre,
            Direccion = dto.Direccion,
            Telefono = dto.Telefono
        };
        Validar(proveedor);
        return _proveedorRepository.Crear(proveedor);
    }

    public Proveedor? Modificar(int id, ModificarProveedorDto dto)
    {
        var existente = _proveedorRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        existente.Nombre = dto.Nombre;
        existente.Direccion = dto.Direccion;
        existente.Telefono = dto.Telefono;

        Validar(existente);
        _proveedorRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var proveedor = _proveedorRepository.ObtenerPorId(id);
        if (proveedor is null)
        {
            return false;
        }

        _proveedorRepository.Eliminar(proveedor);
        return true;
    }

    private static void Validar(Proveedor proveedor)
    {
        if (string.IsNullOrWhiteSpace(proveedor.Nombre))
            throw new ArgumentException("El nombre del proveedor es obligatorio.");

        if (string.IsNullOrWhiteSpace(proveedor.Telefono))
            throw new ArgumentException("El teléfono del proveedor es obligatorio.");

        if (!Regex.IsMatch(proveedor.Telefono, @"^\d+$"))
            throw new ArgumentException("El teléfono debe contener solo números.");
    }
}
