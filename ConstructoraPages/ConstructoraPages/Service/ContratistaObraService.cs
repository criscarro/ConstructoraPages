using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class ContratistaObraService(
    ContratistaObraRepository contratistaObraRepository,
    ContratistaRepository contratistaRepository,
    ObraRepository obraRepository)
{
    private readonly ContratistaObraRepository _contratistaObraRepository = contratistaObraRepository;
    private readonly ContratistaRepository _contratistaRepository = contratistaRepository;
    private readonly ObraRepository _obraRepository = obraRepository;

    public List<ContratistaObra> ObtenerTodos()
    {
        return _contratistaObraRepository.ObtenerTodos();
    }

    public ContratistaObra? ObtenerPorId(int id)
    {
        return _contratistaObraRepository.ObtenerPorId(id);
    }

    public ContratistaObra Crear(AltaContratistaObraDto dto)
    {
        var contratista = _contratistaRepository.ObtenerPorId(dto.ContratistaId) ?? 
            throw new ArgumentException("El contratista especificado no existe.");

        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        var contratistaObra = new ContratistaObra
        {
            FechaInicio = dto.FechaInicio,
            FechaFin = dto.FechaFin,
            CostoPorTrabajo = dto.CostoPorTrabajo,
            Contratista = contratista,
            Obra = obra
        };
        Validar(contratistaObra);
        return _contratistaObraRepository.Crear(contratistaObra);
    }

    public ContratistaObra? Modificar(int id, ModificarContratistaObraDto dto)
    {
        var existente = _contratistaObraRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        var contratista = _contratistaRepository.ObtenerPorId(dto.ContratistaId) ?? 
            throw new ArgumentException("El contratista especificado no existe.");

        var obra = _obraRepository.ObtenerPorId(dto.ObraId) ?? 
            throw new ArgumentException("La obra especificada no existe.");

        existente.FechaInicio = dto.FechaInicio;
        existente.FechaFin = dto.FechaFin;
        existente.CostoPorTrabajo = dto.CostoPorTrabajo;
        existente.Contratista = contratista;
        existente.Obra = obra;

        Validar(existente);
        _contratistaObraRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var contratistaObra = _contratistaObraRepository.ObtenerPorId(id);
        if (contratistaObra is null)
        {
            return false;
        }

        _contratistaObraRepository.Eliminar(contratistaObra);
        return true;
    }

    private static void Validar(ContratistaObra contratistaObra)
    {
        if (contratistaObra.FechaFin.HasValue && contratistaObra.FechaFin.Value < contratistaObra.FechaInicio)
            throw new ArgumentException("La fecha de fin no puede ser anterior a la fecha de inicio.");

        if (contratistaObra.CostoPorTrabajo <= 0)
            throw new ArgumentException("El costo por trabajo debe ser mayor a cero.");
    }
}
