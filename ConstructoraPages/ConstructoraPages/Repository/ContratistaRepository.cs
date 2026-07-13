using ConstructoraPages.Data;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Repository;

public class ContratistaRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<Contratista> ObtenerTodos()
    {
        return _context.Contratista.ToList();
    }

    public Contratista? ObtenerPorId(int id)
    {
        return _context.Contratista.Find(id);
    }

    public Contratista Crear(Contratista contratista)
    {
        _context.Contratista.Add(contratista);
        _context.SaveChanges();
        return contratista;
    }

    public void Modificar(Contratista contratista)
    {
        _context.Contratista.Update(contratista);
        _context.SaveChanges();
    }

    public void Eliminar(Contratista contratista)
    {
        _context.Contratista.Remove(contratista);
        _context.SaveChanges();
    }
}
