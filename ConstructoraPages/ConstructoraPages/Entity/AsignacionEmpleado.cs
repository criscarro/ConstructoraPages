namespace ConstructoraPages.Entity
{
    public class AsignacionEmpleado
    {
        public int Id { get; set; }
        public DateTime FechaAsignacion { get; set; }
        
        public Empleado Empleado { get; set; } = null!;
        public Obra Obra { get; set; } = null!;
    }
}
