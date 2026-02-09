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

        //Publicaciones
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<CategoriaPublicacion> CategoriaPublicacion { get; set; }

        // Tickets
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<EstadoTicket> EstadoTicket { get; set; }
        public DbSet<CategoriaTicket> CategoriaTicket { get; set; }


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

            // =========================
            // Publicacion
            // =========================
            modelBuilder.Entity<Publicacion>(t =>
            {
                t.ToTable("Publicaciones");

                t.HasKey(p => p.PublicacionId);
                t.Property(p => p.PublicacionId).UseIdentityColumn().ValueGeneratedOnAdd();

                t.Property(p => p.Titulo)
                      .IsRequired()
                      .HasMaxLength(150);

                t.Property(p => p.Contenido)
                      .IsRequired();

                t.Property(p => p.FechaCreacion)
                      .IsRequired()
                      .HasDefaultValueSql("GETDATE()");

                t.Property(p => p.Habilitado)
                      .IsRequired();

                t.Property(p => p.UsuarioId)
                      .IsRequired();

                // Relación Publicacion -> CategoriaPublicacion
                t.HasOne(p => p.CategoriaPublicacion)
                      .WithMany(c => c.Publicaciones)
                      .HasForeignKey(p => p.CategoriaPublicacionId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación Publicacion -> Usuario
                t.HasOne(p => p.Usuario)
                      .WithMany(u => u.Publicaciones)
                      .HasForeignKey(p => p.UsuarioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ======================
            // Ticket
            // ======================
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();

                entity.Property(t => t.Titulo)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(t => t.Contenido)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(t => t.FechaCreacion)
                    .IsRequired();

                // Relación con Usuario (Emisor)
                entity.HasOne(t => t.Emisor)
                    .WithMany(u => u.Tickets)
                    .HasForeignKey(t => t.EmisorId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación con EstadoTicket
                entity.HasOne(t => t.EstadoTicket)
                    .WithMany(e => e.Tickets)
                    .HasForeignKey(t => t.EstadoTicketId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Relación con CategoriaTicket
                entity.HasOne(t => t.CategoriaTicket)
                    .WithMany(c => c.Tickets)
                    .HasForeignKey(t => t.CategoriaTicketId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ======================
            // EstadoTicket
            // ======================
            modelBuilder.Entity<EstadoTicket>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();


                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // ======================
            // CategoriaTicket
            // ======================
            modelBuilder.Entity<CategoriaTicket>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(t => t.Id).ValueGeneratedOnAdd();


                entity.Property(c => c.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);
            });



            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Residente>().ToTable("Residentes");
            modelBuilder.Entity<TipoResidente>().ToTable("TipoResidente").Metadata.SetIsTableExcludedFromMigrations(true);
            modelBuilder.Entity<EstadoResidente>().ToTable("EstadoResidente").Metadata.SetIsTableExcludedFromMigrations(true);

        }


    }
}
