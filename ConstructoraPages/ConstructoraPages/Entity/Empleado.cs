using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Entity
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public CargoEmpleado Cargo { get; set; }
        public double Salario { get; set; }
    }
}
