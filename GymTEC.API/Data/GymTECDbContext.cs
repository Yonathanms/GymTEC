using Microsoft.EntityFrameworkCore; //el entity framework nos facilita la interacción con la base de datos relacionales mediante mapeo objeto-relacional (ORM)
using Microsoft.EntityFrameworkCore;
using GymTEC.API.Models;

namespace GymTEC.API.Data
{
    // Representa el contexto de la base de datos para GymTEC
    public class GymTECDbContext : DbContext
    {
        public GymTECDbContext(DbContextOptions<GymTECDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<Sucursal> Sucursal { get; set; } = default!;
        public DbSet<Persona> Personas { get; set; } = default!;
        public DbSet<Rol> Roles { get; set; } = default!;
        public DbSet<Cliente> Cliente { get; set; } = default!;
        public DbSet<PersonaxRol> PersonaxRol { get; set; } = default!;
        public DbSet<Administrador> Administrador { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapear la entidad Sucursal a la tabla "sucursal"
            modelBuilder.Entity<Sucursal>().ToTable("Sucursal");

            // Configurar la relación PK y FK entre Cliente y Persona
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.NumCedula);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Persona)
                .WithOne()
                .HasForeignKey<Cliente>(c => c.NumCedula)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuraciones de las llaves foráneas y primarias entre la entidad Persona con la entidad Rol
            // Clave primaria compuesta
            modelBuilder.Entity<PersonaxRol>()
                .HasKey(pr => new { pr.NumCedula, pr.IdRol });

            // Relación con Persona
            modelBuilder.Entity<PersonaxRol>()
                .HasOne(pr => pr.Persona)
                .WithMany()
                .HasForeignKey(pr => pr.NumCedula)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con Rol
            modelBuilder.Entity<PersonaxRol>()
                .HasOne(pr => pr.Rol)
                .WithMany()
                .HasForeignKey(pr => pr.IdRol)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}