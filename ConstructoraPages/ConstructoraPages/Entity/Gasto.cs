using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Entity
{
    public class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public TipoGasto Tipo { get; set; }

        public Obra Obra { get; set; } = null!;
    }
}
