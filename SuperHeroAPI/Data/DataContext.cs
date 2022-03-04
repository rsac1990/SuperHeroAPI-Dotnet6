using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) { }

        public DbSet<SuperHero> SuperHeroes { get; set; } //usando code first migrations va el modelo seguido del nombre de la tabla por le general en plural

    }
}
