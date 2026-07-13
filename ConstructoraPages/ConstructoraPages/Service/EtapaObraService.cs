using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Entity.Enums;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class EtapaObraService(EtapaObraRepository etapaObraRepository, ObraRepository obraRepository)
{
    private readonly EtapaObraRepository _etapaObraRepository = etapaObraRepository;
    private readonly ObraRepository _obraRepository = obraRepository;

    public List<EtapaObra> ObtenerTodos()
    {
        return _etapaObraRepository.ObtenerTodos();
    }

    public EtapaObra? ObtenerPorId(int id)
    {
        return _etapaObraRepository.ObtenerPorId(id);
    }

    public EtapaObra Crear(AltaEtapaObraDto dto)
    {
        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        var etapaObra = new EtapaObra
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Estado = dto.Estado,
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin,
            Obra = obra
        };
        Validar(etapaObra);
        return _etapaObraRepository.Crear(etapaObra);
    }

    public EtapaObra? Modificar(int id, ModificarEtapaObraDto dto)
    {
        var existente = _etapaObraRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        existente.Nombre = dto.Nombre;
        existente.Descripcion = dto.Descripcion;
        existente.Estado = dto.Estado;
        existente.FechaInicio = dto.FechaInicio;
        existente.FechaFin = dto.FechaFin;
        existente.Obra = obra;

        Validar(existente);
        _etapaObraRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var etapaObra = _etapaObraRepository.ObtenerPorId(id);
        if (etapaObra is null)
        {
            return false;
        }

        _etapaObraRepository.Eliminar(etapaObra);
        return true;
    }

    private static void Validar(EtapaObra etapaObra)
    {
        if (string.IsNullOrWhiteSpace(etapaObra.Nombre))
            throw new ArgumentException("El nombre de la etapa es obligatorio.");

        if (!Enum.IsDefined(typeof(EstadoObra), etapaObra.Estado))
            throw new ArgumentException("El estado de la etapa no es válido.");

        if (etapaObra.FechaFin.HasValue && etapaObra.FechaFin.Value < etapaObra.FechaInicio)
            throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");
    }
}
