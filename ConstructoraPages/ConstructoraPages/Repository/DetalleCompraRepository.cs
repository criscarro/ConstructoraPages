using ConstructoraPages.Data;
using ConstructoraPages.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstructoraPages.Repository;

public class DetalleCompraRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<DetalleCompra> ObtenerTodos()
    {
        return _context.DetalleCompra
        .Include(d => d.Material)
        .Include(d => d.Compra)
            .ThenInclude(c => c.Obra)
        .ToList();
    }

    public DetalleCompra? ObtenerPorId(int id)
    {
        return _context.DetalleCompra
        .Include(d => d.Material)
        .Include(d => d.Compra)
            .ThenInclude(c => c.Obra)
        .FirstOrDefault(d => d.Id == id);
    }

    public DetalleCompra Crear(DetalleCompra detalleCompra)
    {
        _context.DetalleCompra.Add(detalleCompra);
        _context.SaveChanges();
        return detalleCompra;
    }

    public void Modificar(DetalleCompra detalleCompra)
    {
        _context.DetalleCompra.Update(detalleCompra);
        _context.SaveChanges();
    }

    public void Eliminar(DetalleCompra detalleCompra)
    {
        _context.DetalleCompra.Remove(detalleCompra);
        _context.SaveChanges();
    }
}
