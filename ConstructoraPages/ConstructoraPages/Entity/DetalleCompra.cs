namespace ConstructoraPages.Entity
{
    public class DetalleCompra
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        
        public CompraMaterial Compra { get; set; } = null!;
        public Material Material { get; set; } = null!;
    }
}
