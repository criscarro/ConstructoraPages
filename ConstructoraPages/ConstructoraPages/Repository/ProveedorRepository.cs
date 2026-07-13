using ConstructoraPages.Data;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Repository;

public class ProveedorRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<Proveedor> ObtenerTodos()
    {
        return _context.Proveedor.ToList();
    }

    public Proveedor? ObtenerPorId(int id)
    {
        return _context.Proveedor.Find(id);
    }

    public Proveedor Crear(Proveedor proveedor)
    {
        _context.Proveedor.Add(proveedor);
        _context.SaveChanges();
        return proveedor;
    }

    public void Modificar(Proveedor proveedor)
    {
        _context.Proveedor.Update(proveedor);
        _context.SaveChanges();
    }

    public void Eliminar(Proveedor proveedor)
    {
        _context.Proveedor.Remove(proveedor);
        _context.SaveChanges();
    }
}
