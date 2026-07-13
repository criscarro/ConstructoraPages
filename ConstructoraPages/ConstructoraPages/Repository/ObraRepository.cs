using ConstructoraPages.Data;
using ConstructoraPages.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstructoraPages.Repository;

public class ObraRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<Obra> ObtenerTodos()
    {
        return _context.Obra.Include(o => o.Clientes).ToList();
    }

    public Obra? ObtenerPorId(int id)
    {
        return _context.Obra.Include(o => o.Clientes).FirstOrDefault(o => o.Id == id);
    }

    public Obra Crear(Obra obra)
    {
        _context.Obra.Add(obra);
        _context.SaveChanges();
        return obra;
    }

    public void Modificar(Obra obra)
    {
        _context.Obra.Update(obra);
        _context.SaveChanges();
    }

    public void Eliminar(Obra obra)
    {
        _context.Obra.Remove(obra);
        _context.SaveChanges();
    }
}
