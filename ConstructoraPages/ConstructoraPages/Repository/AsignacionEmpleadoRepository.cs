using ConstructoraPages.Data;
using ConstructoraPages.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstructoraPages.Repository;

public class AsignacionEmpleadoRepository(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public List<AsignacionEmpleado> ObtenerTodos()
    {
        return _context.AsignacionEmpleado
            .Include(a => a.Obra)
            .Include(a => a.Empleado)
            .ToList();
    }

    public AsignacionEmpleado? ObtenerPorId(int id)
    {
        return _context.AsignacionEmpleado
            .Include(a => a.Obra)
            .Include(a => a.Empleado)
            .FirstOrDefault(a => a.Id == id);
    }

    public AsignacionEmpleado Crear(AsignacionEmpleado asignacionEmpleado)
    {
        _context.AsignacionEmpleado.Add(asignacionEmpleado);
        _context.SaveChanges();
        return asignacionEmpleado;
    }

    public void Modificar(AsignacionEmpleado asignacionEmpleado)
    {
        _context.AsignacionEmpleado.Update(asignacionEmpleado);
        _context.SaveChanges();
    }

    public void Eliminar(AsignacionEmpleado asignacionEmpleado)
    {
        _context.AsignacionEmpleado.Remove(asignacionEmpleado);
        _context.SaveChanges();
    }
}