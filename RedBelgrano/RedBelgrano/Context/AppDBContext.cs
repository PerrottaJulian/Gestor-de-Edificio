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

        //Transacciones
        public DbSet<Transaccion> Transacciones { get; set; }
        public DbSet<TipoTransaccion> TipoTransaccion { get; set; }
        public DbSet<CategoriaTransaccion> CategoriaTransaccion { get; set; }


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

                t.HasOne(r => r.tipoResidente).WithMany(tr => tr.Residentes).HasForeignKey(r => r.tipoRId);
                t.HasOne(r => r.estadoResidente).WithMany(er => er.Residentes).HasForeignKey(r => r.estadoId);

            });

            modelBuilder.Entity<Transaccion>(t =>
            {
                t.HasKey(x => x.transaccionId);
                t.Property(x => x.transaccionId).UseIdentityColumn().ValueGeneratedOnAdd();

                t.Property(x => x.monto).IsRequired();
                t.Property(x => x.detalle).HasColumnType("nvarchar(max)");

                t.Property(x => x.administradorId).IsRequired();

                t.Property(x => x.fecha).HasDefaultValueSql("GETDATE()");

                t.HasOne(t => t.administrador).WithMany(u => u.Transacciones).HasForeignKey(t => t.administradorId);
                t.HasOne(t => t.tipoTransaccion).WithMany(tt => tt.Transacciones).HasForeignKey(t => t.tipoId);

            });
            

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Residente>().ToTable("Residente");
            modelBuilder.Entity<TipoResidente>().ToTable("TipoResidente").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<EstadoResidente>().ToTable("EstadoResidente").Metadata.SetIsTableExcludedFromMigrations(true);

        }


    }
}
