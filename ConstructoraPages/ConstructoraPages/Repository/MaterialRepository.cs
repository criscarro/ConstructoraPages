using ConstructoraPages.Data;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Repository;

public class MaterialRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<Material> ObtenerTodos()
    {
        return _context.Material.ToList();
    }

    public Material? ObtenerPorId(int id)
    {
        return _context.Material.Find(id);
    }

    public Material Crear(Material material)
    {
        _context.Material.Add(material);
        _context.SaveChanges();
        return material;
    }

    public void Modificar(Material material)
    {
        _context.Material.Update(material);
        _context.SaveChanges();
    }

    public void Eliminar(Material material)
    {
        _context.Material.Remove(material);
        _context.SaveChanges();
    }
}
