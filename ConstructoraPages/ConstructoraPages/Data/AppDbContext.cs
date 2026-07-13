using ConstructoraPages.Entity;
using Microsoft.EntityFrameworkCore;

namespace ConstructoraPages.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    
        public DbSet<Obra> Obra { get; set; }
        public DbSet<EtapaObra> EtapaObra { get; set; }
        public DbSet<Contratista> Contratista { get; set; }
        public DbSet<ContratistaObra> ContratistaObra { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<CompraMaterial> CompraMaterial { get; set; }
        public DbSet<DetalleCompra> DetalleCompra { get; set; }
        public DbSet<Material> Material { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<AsignacionEmpleado> AsignacionEmpleado { get; set; }
        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Gasto> Gasto { get; set; }



    }
