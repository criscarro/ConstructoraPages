namespace ConstructoraPages.Entity
{
    public class Material
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Stock { get; set; }
        public double PrecioUnitario { get; set; }
    }
}
