using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Dto_s
{
    public class AltaEtapaObraDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public EstadoObra Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int ObraId { get; set; }
    }
}
