using ConstructoraPages.Entity.Enums;
using ConstructoraPages.Entity;

namespace ConstructoraPages.Dto_s
{
    public class AltaObraDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;

        public IntervencionObra IntervencionObra { get; set; } = IntervencionObra.ObraNueva;
        public TipoObra TipoObra { get; set; } = TipoObra.Residencial;
        public EstadoObra Estado { get; set; } = EstadoObra.Pendiente;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public double Presupuesto { get; set; }
        public double CostoActual { get; set; }
    }
}
