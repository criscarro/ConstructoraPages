using ConstructoraPages.Data;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Repository;

public class GastoRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<Gasto> ObtenerTodos()
    {
        return _context.Gasto.ToList();
    }

    public Gasto? ObtenerPorId(int id)
    {
        return _context.Gasto.Find(id);
    }

    public Gasto Crear(Gasto gasto)
    {
        _context.Gasto.Add(gasto);
        _context.SaveChanges();
        return gasto;
    }

    public void Modificar(Gasto gasto)
    {
        _context.Gasto.Update(gasto);
        _context.SaveChanges();
    }

    public void Eliminar(Gasto gasto)
    {
        _context.Gasto.Remove(gasto);
        _context.SaveChanges();
    }
}
