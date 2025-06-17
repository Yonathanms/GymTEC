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
    }
}