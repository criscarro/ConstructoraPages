using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class MaterialService(MaterialRepository materialRepository)
{
    private readonly MaterialRepository _materialRepository = materialRepository;

    public List<Material> ObtenerTodos()
    {
        return _materialRepository.ObtenerTodos();
    }

    public Material? ObtenerPorId(int id)
    {
        return _materialRepository.ObtenerPorId(id);
    }

    public Material Crear(AltaMaterialDto dto)
    {
        var material = new Material
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Stock = dto.Stock,
            PrecioUnitario = dto.PrecioUnitario
        };
        Validar(material);
        return _materialRepository.Crear(material);
    }

    public Material? Modificar(int id, ModificarMaterialDto dto)
    {
        var existente = _materialRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        existente.Nombre = dto.Nombre;
        existente.Descripcion = dto.Descripcion;
        existente.Stock = dto.Stock;
        existente.PrecioUnitario = dto.PrecioUnitario;

        Validar(existente);
        _materialRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var material = _materialRepository.ObtenerPorId(id);
        if (material is null)
        {
            return false;
        }

        _materialRepository.Eliminar(material);
        return true;
    }

    private static void Validar(Material material)
    {
        if (string.IsNullOrWhiteSpace(material.Nombre))
            throw new ArgumentException("El nombre del material es obligatorio.");

        if (material.Stock < 0)
            throw new ArgumentException("El stock no puede ser negativo.");

        if (material.PrecioUnitario <= 0)
            throw new ArgumentException("El precio unitario debe ser mayor a cero.");
    }
}
