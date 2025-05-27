using Microsoft.EntityFrameworkCore;
using RedBelgrano.Models;
using RedBelgrano.Models.EnumModels;

namespace RedBelgrano.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options ): base(options)
        {

        }

        // Usuarios
        public DbSet<Usuario> Usuarios { get; set; }

        // Residentes
        public DbSet<Residente> Residentes { get; set; }
        public DbSet<TipoResidente> TipoResidente { get; set; }
        public DbSet<EstadoResidente> EstadoResidente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(t =>
            {
                t.HasKey(x => x.usuarioId);
                t.Property(x => x.usuarioId)
                 .UseIdentityColumn()
                 .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Residente>(t =>
            {
                t.HasKey(x => x.residenteId);
                t.Property(x => x.residenteId).UseIdentityColumn().ValueGeneratedOnAdd();

                t.Property(x => x.nombre).IsRequired().HasMaxLength(20);
                t.Property(x => x.apellido).IsRequired().HasMaxLength(20);
                t.Property(x => x.dni).IsRequired();

                t.Property(x => x.email).IsRequired();
                t.Property(x => x.telefono).IsRequired();

                t.Property(x => x.piso).IsRequired();
                t.Property(x => x.departamento).IsRequired();
               
                t.Property(x => x.tipoRId).IsRequired();
                t.Property(x => x.estadoId).IsRequired();

                t.Property(x => x.fechaIngreso).HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Residente>().ToTable("Residente");
            modelBuilder.Entity<TipoResidente>().ToTable("TipoResidente").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<EstadoResidente>().ToTable("EstadoResidente").Metadata.SetIsTableExcludedFromMigrations(true);

        }


    }
}
