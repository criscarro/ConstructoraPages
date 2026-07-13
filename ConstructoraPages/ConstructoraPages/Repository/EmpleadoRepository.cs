using ConstructoraPages.Data;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Repository;

public class EmpleadoRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<Empleado> ObtenerTodos()
    {
        return _context.Empleado.ToList();
    }

    public Empleado? ObtenerPorId(int id)
    {
        return _context.Empleado.Find(id);
    }

    public Empleado Crear(Empleado empleado)
    {
        _context.Empleado.Add(empleado);
        _context.SaveChanges();
        return empleado;
    }

    public void Modificar(Empleado empleado)
    {
        _context.Empleado.Update(empleado);
        _context.SaveChanges();
    }

    public void Eliminar(Empleado empleado)
    {
        _context.Empleado.Remove(empleado);
        _context.SaveChanges();
    }
}
