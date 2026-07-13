using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Entity
{
    public class Contratista
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public EspecialidadContratista Especialidad { get; set; }
    }
}
