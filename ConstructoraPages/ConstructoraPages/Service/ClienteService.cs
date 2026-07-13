using ConstructoraPages.Dto_s;
using ConstructoraPages.Entity;
using ConstructoraPages.Repository;

using System.Text.RegularExpressions;

namespace ConstructoraPages.Service;

public class ClienteService(ClienteRepository clienteRepository)
{
    private readonly ClienteRepository _clienteRepository = clienteRepository;

    public List<Cliente> ObtenerTodos()
    {
        return _clienteRepository.ObtenerTodos();
    }

    public Cliente? ObtenerPorId(int id)
    {
        return _clienteRepository.ObtenerPorId(id);
    }

    public Cliente Crear(AltaClienteDto dto)
    {
        var cliente = new Cliente
        {
            Nombre = dto.Nombre,
            Apellido = dto.Apellido,
            Cedula = dto.Cedula,
            Telefono = dto.Telefono
        };
        Validar(cliente);
        return _clienteRepository.Crear(cliente);
    }

    public Cliente? Modificar(int id, ModificarClienteDto dto)
    {
        var existente = _clienteRepository.ObtenerPorId(id);
        if (existente is null)
        {
            return null;
        }

        existente.Nombre = dto.Nombre;
        existente.Apellido = dto.Apellido;
        existente.Cedula = dto.Cedula;
        existente.Telefono = dto.Telefono;

        Validar(existente);
        _clienteRepository.Modificar(existente);
        return existente;
    }

    public bool Eliminar(int id)
    {
        var cliente = _clienteRepository.ObtenerPorId(id);
        if (cliente is null)
        {
            return false;
        }

        _clienteRepository.Eliminar(cliente);
        return true;
    }

    private static void Validar(Cliente cliente)
    {
        if (string.IsNullOrWhiteSpace(cliente.Nombre))
            throw new ArgumentException("El nombre del cliente es obligatorio.");

        if (string.IsNullOrWhiteSpace(cliente.Apellido))
            throw new ArgumentException("El apellido del cliente es obligatorio.");

        if (string.IsNullOrWhiteSpace(cliente.Cedula))
            throw new ArgumentException("La cédula del cliente es obligatoria.");

        if (!Regex.IsMatch(cliente.Cedula, @"^\d{8}$"))
            throw new ArgumentException("La cédula debe tener exactamente 8 dígitos numéricos.");

        if (string.IsNullOrWhiteSpace(cliente.Telefono))
            throw new ArgumentException("El teléfono del cliente es obligatorio.");

        if (!Regex.IsMatch(cliente.Telefono, @"^\d+$"))
            throw new ArgumentException("El teléfono debe contener solo números.");
    }
}
