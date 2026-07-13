using ConstructoraPages.Data;
using ConstructoraPages.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstructoraPages.Repository;

public class EtapaObraRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<EtapaObra> ObtenerTodos()
    {
        return _context.EtapaObra.Include(e => e.Obra).ToList();
    }

    public EtapaObra? ObtenerPorId(int id)
    {
        return _context.EtapaObra.Include(e => e.Obra).FirstOrDefault(e => e.Id == id);
    }

    public EtapaObra Crear(EtapaObra etapaObra)
    {
        _context.EtapaObra.Add(etapaObra);
        _context.SaveChanges();
        return etapaObra;
    }

    public void Modificar(EtapaObra etapaObra)
    {
        _context.EtapaObra.Update(etapaObra);
        _context.SaveChanges();
    }

    public void Eliminar(EtapaObra etapaObra)
    {
        _context.EtapaObra.Remove(etapaObra);
        _context.SaveChanges();
    }
}
