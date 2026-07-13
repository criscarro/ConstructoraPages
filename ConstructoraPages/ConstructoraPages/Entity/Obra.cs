using ConstructoraPages.Entity.Enums;

namespace ConstructoraPages.Entity
{
    public class Obra
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public IntervencionObra IntervencionObra { get; set; } = IntervencionObra.ObraNueva;
        public TipoObra TipoObra { get; set; } = TipoObra.Residencial;
        public EstadoObra Estado { get; set; } = EstadoObra.Pendiente;  
        public DateTime FechaInicio { get; set; } = DateTime.MinValue;
        public DateTime FechaFinal { get; set; } = DateTime.MinValue;
        public double Presupuesto { get; set; }
        public double CostoActual { get; set; }

        public List<Cliente> Clientes { get; set; } = new();
    }
}
