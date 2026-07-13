using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Dto_s
{
    public class AltaContratistaDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public EspecialidadContratista Especialidad { get; set; }
    }
}
