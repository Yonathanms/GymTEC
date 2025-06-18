using Microsoft.EntityFrameworkCore; //el entity framework nos facilita la interacción con la base de datos relacionales mediante mapeo objeto-relacional (ORM)
using GymTEC.API.Models;

namespace GymTEC.API.Data
{
    // representa el contexto de la base de datos para GymTEC
    public class GymTECDbContext : DbContext // Se agrega la herencia de DbContext
    {
        public GymTECDbContext(DbContextOptions<GymTECDbContext> options)
            : base(options)
        {

        }

        //conjunto de entidades que se mapearán a la base de datos "Branches"
        public DbSet<Branch> Branches { get; set; } = null!; // Se inicializa con null!
    // representa el contexto de la base de datos para GymTEC
        public DbSet<Persona> Personas { get; set; } = default!;
    // representa el contexto de la base de datos para GymTEC
        public DbSet<Rol> Roles { get; set; } = default!;
    // representa el contexto de la base de datos para GymTEC
        public DbSet<Cliente> Cliente { get; set; } = default!;

        // esta configuración se utiliza para definir las relaciones entre las entidades y sus claves primarias y foráneas
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar la relación PK y FK entre Cliente y Persona
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.NumCedula);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Persona)
                .WithOne() // Si tienes en Persona: public Cliente Cliente { get; set; }, puedes poner .WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(c => c.NumCedula)
                .OnDelete(DeleteBehavior.Cascade); // o .Restrict, según la lógica de negocio


            // Configuraciones de las llaves foraneas y primarias entre la entidad Persona con la entidad Rol
            // Clave primaria compuesta
            modelBuilder.Entity<PersonaxRol>()
                .HasKey(pr => new { pr.NumCedula, pr.IdRol });

            // Relación con Persona
            modelBuilder.Entity<PersonaxRol>()
                .HasOne(pr => pr.Persona)
                .WithMany() // Si en Persona tienes una colección: .WithMany(p => p.PersonaxRoles)
                .HasForeignKey(pr => pr.NumCedula)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación con Rol
            modelBuilder.Entity<PersonaxRol>()
                .HasOne(pr => pr.Rol)
                .WithMany() // Si en Rol tienes una colección: .WithMany(r => r.PersonaxRoles)
                .HasForeignKey(pr => pr.IdRol)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);
        }
    // representa el contexto de la base de datos para GymTEC
public DbSet<GymTEC.API.Models.PersonaxRol> PersonaxRol { get; set; } = default!;

    }
}