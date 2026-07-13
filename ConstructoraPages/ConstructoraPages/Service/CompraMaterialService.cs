using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class CompraMaterialService(
    CompraMaterialRepository compraMaterialRepository,
    ProveedorRepository proveedorRepository,
    ObraRepository obraRepository)
{
    private readonly CompraMaterialRepository _compraMaterialRepository = compraMaterialRepository;
    private readonly ProveedorRepository _proveedorRepository = proveedorRepository;
    private readonly ObraRepository _obraRepository = obraRepository;

    public List<CompraMaterial> ObtenerTodos()
    {
        return _compraMaterialRepository.ObtenerTodos();
    }

    public CompraMaterial? ObtenerPorId(int id)
    {
        return _compraMaterialRepository.ObtenerPorId(id);
    }

    public CompraMaterial Crear(AltaCompraMaterialDto dto)
    {
        var proveedor = _proveedorRepository.ObtenerPorId(dto.ProveedorId) ?? 
            throw new ArgumentException("El proveedor especificado no existe.");

        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        var compraMaterial = new CompraMaterial
        {
            Fecha = dto.Fecha,
            MontoTotal = dto.MontoTotal,
            Proveedor = proveedor,
            Obra = obra
        };
        Validar(compraMaterial);
        return _compraMaterialRepository.Crear(compraMaterial);
    }

    public CompraMaterial? Modificar(int id, ModificarCompraMaterialDto dto)
    {
        var existente = _compraMaterialRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        var proveedor = _proveedorRepository.ObtenerPorId(dto.ProveedorId) ?? 
            throw new ArgumentException("El proveedor especificado no existe.");

        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        existente.Fecha = dto.Fecha;
        existente.MontoTotal = dto.MontoTotal;
        existente.Proveedor = proveedor;
        existente.Obra = obra;

        Validar(existente);
        _compraMaterialRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var compraMaterial = _compraMaterialRepository.ObtenerPorId(id);
        if (compraMaterial is null)
        {
            return false;
        }

        _compraMaterialRepository.Eliminar(compraMaterial);
        return true;
    }

    private static void Validar(CompraMaterial compraMaterial)
    {
        if (compraMaterial.MontoTotal <= 0)
            throw new ArgumentException("El monto total debe ser mayor a cero.");
    }
}
