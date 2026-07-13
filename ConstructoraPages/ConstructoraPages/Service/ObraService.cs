
using ConstructoraPages.Entity;
using ConstructoraPages.Entity.Enums;
using ConstructoraPages.Repository;

namespace ConstructoraPages.Service;

public class ObraService(ObraRepository obraRepository, ClienteRepository clienteRepository)
{
    private readonly ObraRepository _obraRepository = obraRepository;
    private readonly ClienteRepository _clienteRepository = clienteRepository;

    public List<Obra> ObtenerTodos()
    {
        return _obraRepository.ObtenerTodos();
    }

    public Obra? ObtenerPorId(int id)
    {
        return _obraRepository.ObtenerPorId(id);
    }

    public Obra Crear(Obra obra)
    {
        Validar(obra);
        return _obraRepository.Crear(obra);
    }

    public Obra? Modificar(int id, Obra obra)
    {
        var obraExistente = _obraRepository.ObtenerPorId(id);
        if (obraExistente is null)
        {
            return null;
        }

        Validar(obra);
        obraExistente.Nombre = obra.Nombre;
        obraExistente.Direccion = obra.Direccion;
        obraExistente.TipoObra = obra.TipoObra;
        obraExistente.IntervencionObra = obra.IntervencionObra;
        obraExistente.Estado = obra.Estado;
        obraExistente.FechaInicio = obra.FechaInicio;
        obraExistente.FechaFinal = obra.FechaFinal;
        obraExistente.Presupuesto = obra.Presupuesto;
        obraExistente.CostoActual = obra.CostoActual;

        _obraRepository.Modificar(obraExistente);
        return obraExistente;
    }

    public bool Eliminar(int id)
    {
        var obra = _obraRepository.ObtenerPorId(id);
        if (obra is null)
        {
            return false;
        }

        _obraRepository.Eliminar(obra);
        return true;
    }
    


    private static void Validar(Obra obra)
    {
        if (string.IsNullOrWhiteSpace(obra.Nombre))
        {
            throw new ArgumentException("El nombre de la obra es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(obra.Direccion))
        {
            throw new ArgumentException("La dirección de la obra es obligatoria.");
        }

        if (!Enum.IsDefined(typeof(TipoObra), obra.TipoObra))
        {
            throw new ArgumentException("El tipo de obra no es válido.");
        }

        if (!Enum.IsDefined(typeof(IntervencionObra), obra.IntervencionObra))
        {
            throw new ArgumentException("La intervención de la obra no es válida.");
        }

        if (!Enum.IsDefined(typeof(EstadoObra), obra.Estado))
        {
            throw new ArgumentException("El estado de la obra no es válido.");
        }

        if (obra.FechaFinal < obra.FechaInicio)
        {
            throw new ArgumentException("La fecha final no puede ser anterior a la fecha de inicio.");
        }

        if (obra.Presupuesto <= 0)
        {
            throw new ArgumentException("El presupuesto debe ser mayor a cero.");
        }

        if (obra.CostoActual < 0)
        {
            throw new ArgumentException("El costo actual no puede ser negativo.");
        }
    }

    public bool AsignarCliente(int obraId, int clienteId)
    {
        var obra = _obraRepository.ObtenerPorId(obraId);
        if (obra is null) return false;

        var cliente = _clienteRepository.ObtenerPorId(clienteId);
        if (cliente is null) return false;

        if (!obra.Clientes.Any(c => c.Id == clienteId))
        {
            obra.Clientes.Add(cliente);
            _obraRepository.Modificar(obra);
        }

        return true;
    }
}
