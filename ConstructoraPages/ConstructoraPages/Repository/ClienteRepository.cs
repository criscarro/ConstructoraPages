using ConstructoraPages.Data;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Repository;

public class ClienteRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<Cliente> ObtenerTodos()
    {
        return _context.Cliente.ToList();
    }

    public Cliente? ObtenerPorId(int id)
    {
        return _context.Cliente.Find(id);
    }

    public Cliente Crear(Cliente cliente)
    {
        _context.Cliente.Add(cliente);
        _context.SaveChanges();
        return cliente;
    }

    public void Modificar(Cliente cliente)
    {
        _context.Cliente.Update(cliente);
        _context.SaveChanges();
    }

    public void Eliminar(Cliente cliente)
    {
        _context.Cliente.Remove(cliente);
        _context.SaveChanges();
    }
}
