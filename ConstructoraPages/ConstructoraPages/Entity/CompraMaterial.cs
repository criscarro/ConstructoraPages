namespace ConstructoraPages.Entity
{
    public class CompraMaterial
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public double MontoTotal { get; set; }
        
        public Proveedor Proveedor { get; set; } = null!;
        public Obra Obra { get; set; } = null!;
    }
}
