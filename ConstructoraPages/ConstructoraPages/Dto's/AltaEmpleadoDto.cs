using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Dto_s
{
    public class AltaEmpleadoDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public CargoEmpleado Cargo { get; set; }
        public double Salario { get; set; }
    }
}
