using ConstructoraPages.Data;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Repository;

public class ContratistaObraRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<ContratistaObra> ObtenerTodos()
    {
        return _context.ContratistaObra.ToList();
    }

    public ContratistaObra? ObtenerPorId(int id)
    {
        return _context.ContratistaObra.Find(id);
    }

    public ContratistaObra Crear(ContratistaObra contratistaObra)
    {
        _context.ContratistaObra.Add(contratistaObra);
        _context.SaveChanges();
        return contratistaObra;
    }

    public void Modificar(ContratistaObra contratistaObra)
    {
        _context.ContratistaObra.Update(contratistaObra);
        _context.SaveChanges();
    }

    public void Eliminar(ContratistaObra contratistaObra)
    {
        _context.ContratistaObra.Remove(contratistaObra);
        _context.SaveChanges();
    }
}
