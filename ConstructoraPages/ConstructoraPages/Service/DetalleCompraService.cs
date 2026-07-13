using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class DetalleCompraService(
    DetalleCompraRepository detalleCompraRepository,
    CompraMaterialRepository compraMaterialRepository,
    MaterialRepository materialRepository)
{
    private readonly DetalleCompraRepository _detalleCompraRepository = detalleCompraRepository;
    private readonly CompraMaterialRepository _compraMaterialRepository = compraMaterialRepository;
    private readonly MaterialRepository _materialRepository = materialRepository;

    public List<DetalleCompra> ObtenerTodos()
    {
        return _detalleCompraRepository.ObtenerTodos();
    }

    public DetalleCompra? ObtenerPorId(int id)
    {
        return _detalleCompraRepository.ObtenerPorId(id);
    }

    public DetalleCompra Crear(AltaDetalleCompraDto dto)
    {
        var compra = _compraMaterialRepository.ObtenerPorId(dto.CompraId) ?? 
            throw new ArgumentException("La compra especificada no existe.");

        var material = _materialRepository.ObtenerPorId(dto.MaterialId) ?? 
            throw new ArgumentException("El material especificado no existe.");

        var detalleCompra = new DetalleCompra
        {
            Cantidad = dto.Cantidad,
            Precio = dto.Precio,
            Compra = compra,
            Material = material
        };
        Validar(detalleCompra);
        return _detalleCompraRepository.Crear(detalleCompra);
    }

    public DetalleCompra? Modificar(int id, ModificarDetalleCompraDto dto)
    {
        var existente = _detalleCompraRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        var compra = _compraMaterialRepository.ObtenerPorId(dto.CompraId) ?? 
            throw new ArgumentException("La compra especificada no existe.");

        var material = _materialRepository.ObtenerPorId(dto.MaterialId) ?? 
            throw new ArgumentException("El material especificado no existe.");

        existente.Cantidad = dto.Cantidad;
        existente.Precio = dto.Precio;
        existente.Compra = compra;
        existente.Material = material;

        Validar(existente);
        _detalleCompraRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var detalleCompra = _detalleCompraRepository.ObtenerPorId(id);
        if (detalleCompra is null)
        {
            return false;
        }

        _detalleCompraRepository.Eliminar(detalleCompra);
        return true;
    }

    private static void Validar(DetalleCompra detalleCompra)
    {
        if (detalleCompra.Cantidad <= 0)
            throw new ArgumentException("La cantidad debe ser mayor a cero.");

        if (detalleCompra.Precio <= 0)
            throw new ArgumentException("El precio debe ser mayor a cero.");
    }
}
