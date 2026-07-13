namespace ConstructoraPages.Dto_s
{
    public class AltaContratistaObraDto
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public double CostoPorTrabajo { get; set; }
        public int ContratistaId { get; set; }
        public int ObraId { get; set; }
    }
}
