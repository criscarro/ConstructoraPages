using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Entity.Enums;
using ConstructoraPages.Repository;

using System.Text.RegularExpressions;

namespace ConstructoraPages.Service;

public class ContratistaService(ContratistaRepository contratistaRepository)
{
    private readonly ContratistaRepository _contratistaRepository = contratistaRepository;

    public List<Contratista> ObtenerTodos()
    {
        return _contratistaRepository.ObtenerTodos();
    }

    public Contratista? ObtenerPorId(int id)
    {
        return _contratistaRepository.ObtenerPorId(id);
    }

    public Contratista Crear(AltaContratistaDto dto)
    {
        var contratista = new Contratista
        {
            Nombre = dto.Nombre,
            Cedula = dto.Cedula,
            Telefono = dto.Telefono,
            Especialidad = dto.Especialidad
        };
        Validar(contratista);
        return _contratistaRepository.Crear(contratista);
    }

    public Contratista? Modificar(int id, ModificarContratistaDto dto)
    {
        var existente = _contratistaRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        existente.Nombre = dto.Nombre;
        existente.Cedula = dto.Cedula;
        existente.Telefono = dto.Telefono;
        existente.Especialidad = dto.Especialidad;

        Validar(existente);
        _contratistaRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var contratista = _contratistaRepository.ObtenerPorId(id);
        if (contratista is null)
        {
            return false;
        }

        _contratistaRepository.Eliminar(contratista);
        return true;
    }

    private static void Validar(Contratista contratista)
    {
        if (string.IsNullOrWhiteSpace(contratista.Nombre))
            throw new ArgumentException("El nombre del contratista es obligatorio.");

        if (string.IsNullOrWhiteSpace(contratista.Cedula))
            throw new ArgumentException("La cédula del contratista es obligatoria.");

        if (!Regex.IsMatch(contratista.Cedula, @"^\d{8}$"))
            throw new ArgumentException("La cédula debe tener exactamente 8 dígitos numéricos.");

        if (string.IsNullOrWhiteSpace(contratista.Telefono))
            throw new ArgumentException("El teléfono del contratista es obligatorio.");

        if (!Regex.IsMatch(contratista.Telefono, @"^\d+$"))
            throw new ArgumentException("El teléfono debe contener solo números.");

        if (!Enum.IsDefined(typeof(EspecialidadContratista), contratista.Especialidad))
            throw new ArgumentException("La especialidad del contratista no es válida.");
    }
}
