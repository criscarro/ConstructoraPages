namespace ConstructoraPages.Dto_s
{
    public class AltaMaterialDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Stock { get; set; }
        public double PrecioUnitario { get; set; }
    }
}
