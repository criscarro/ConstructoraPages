using ConstructoraPages.Data;
using ConstructoraPages.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstructoraPages.Repository;

public class CompraMaterialRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<CompraMaterial> ObtenerTodos()
    {
        return _context.CompraMaterial
            .Include(c => c.Obra)
            .Include(c => c.Proveedor)
            .ToList();
    }

    public CompraMaterial? ObtenerPorId(int id)
    {
        return _context.CompraMaterial
            .Include(c => c.Obra)
            .Include(c => c.Proveedor)
            .FirstOrDefault(c => c.Id == id);
    }

    public CompraMaterial Crear(CompraMaterial compraMaterial)
    {
        _context.CompraMaterial.Add(compraMaterial);
        _context.SaveChanges();
        return compraMaterial;
    }

    public void Modificar(CompraMaterial compraMaterial)
    {
        _context.CompraMaterial.Update(compraMaterial);
        _context.SaveChanges();
    }

    public void Eliminar(CompraMaterial compraMaterial)
    {
        _context.CompraMaterial.Remove(compraMaterial);
        _context.SaveChanges();
    }
}