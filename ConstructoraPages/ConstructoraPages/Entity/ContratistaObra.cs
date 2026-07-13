namespace ConstructoraPages.Entity
{
    public class ContratistaObra
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public double CostoPorTrabajo { get; set; }
        
        public Contratista Contratista { get; set; } = null!;
        public Obra Obra { get; set; } = null!;
    }
}
