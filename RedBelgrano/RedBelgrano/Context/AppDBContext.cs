using Microsoft.EntityFrameworkCore;
using RedBelgrano.Models;

namespace RedBelgrano.Context
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options ): base(options)
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        /*public DbSet<UsuarioAdmin> UsuariosAdmin { get; set; }
        public DbSet<UsuarioEncargado> UsuariosEncargados { get; set; }
        public DbSet<UsuarioResidente> UsuariosResidentes { get; set; }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Usuario>().UseTptMappingStrategy();

            modelBuilder.Entity<Usuario>(t =>
            {
                t.HasKey(x => x.usuarioId);
                t.Property(x => x.usuarioId)
                 .UseIdentityColumn()
                 .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            /*modelBuilder.Entity<UsuarioAdmin>().ToTable("UsuarioAdmin");
            modelBuilder.Entity<UsuarioEncargado>().ToTable("UsuarioEncargado");
            modelBuilder.Entity<UsuarioResidente>().ToTable("UsuarioResidente");*/
        }


    }
}
