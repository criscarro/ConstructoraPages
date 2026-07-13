using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Entity.Enums;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class GastoService(GastoRepository gastoRepository, ObraRepository obraRepository)
{
    private readonly GastoRepository _gastoRepository = gastoRepository;
    private readonly ObraRepository _obraRepository = obraRepository;

    public List<Gasto> ObtenerTodos()
    {
        return _gastoRepository.ObtenerTodos();
    }

    public Gasto? ObtenerPorId(int id)
    {
        return _gastoRepository.ObtenerPorId(id);
    }

    public Gasto Crear(AltaGastoDto dto)
    {
        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        var gasto = new Gasto
        {
            Descripcion = dto.Descripcion,
            Fecha = dto.Fecha,
            Monto = dto.Monto,
            Tipo = dto.Tipo,
            Obra = obra
        };
        Validar(gasto);
        return _gastoRepository.Crear(gasto);
    }

    public Gasto? Modificar(int id, ModificarGastoDto dto)
    {
        var existente = _gastoRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        existente.Descripcion = dto.Descripcion;
        existente.Fecha = dto.Fecha;
        existente.Monto = dto.Monto;
        existente.Tipo = dto.Tipo;
        existente.Obra = obra;

        Validar(existente);
        _gastoRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var gasto = _gastoRepository.ObtenerPorId(id);
        if (gasto is null)
        {
            return false;
        }

        _gastoRepository.Eliminar(gasto);
        return true;
    }

    private static void Validar(Gasto gasto)
    {
        if (string.IsNullOrWhiteSpace(gasto.Descripcion))
            throw new ArgumentException("La descripción del gasto es obligatoria.");

        if (gasto.Monto <= 0)
            throw new ArgumentException("El monto del gasto debe ser mayor a cero.");

        if (!Enum.IsDefined(typeof(TipoGasto), gasto.Tipo))
            throw new ArgumentException("El tipo de gasto no es válido.");
    }
}
