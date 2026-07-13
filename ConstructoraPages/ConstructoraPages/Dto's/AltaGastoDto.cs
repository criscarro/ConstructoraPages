using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Dto_s
{
    public class AltaGastoDto
    {
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public double Monto { get; set; }
        public TipoGasto Tipo { get; set; }
        public int ObraId { get; set; }
    }
}
