using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Entity
{
    public class EtapaObra
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public EstadoObra Estado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        
        public Obra Obra { get; set; } = null!;
    }
}
