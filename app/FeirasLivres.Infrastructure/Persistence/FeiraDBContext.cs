using Microsoft.EntityFrameworkCore;

namespace FeirasLivres.Infrastructure.Persistence
{
    public class FeiraDBContext : DbContext
    {
        public FeiraDBContext(DbContextOptions<FeiraDBContext> options) : base(options) { }

        public DbSet<Core.Entities.Feira> Feiras { get; set; }
        public DbSet<Core.Entities.Distrito> Distritos { get; set; }
        public DbSet<Core.Entities.Subprefeitura> Subprefeituras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Core.Entities.Configurations.FeiraConfiguration());
            modelBuilder.ApplyConfiguration(new Core.Entities.Configurations.DistritoConfiguration());
            modelBuilder.ApplyConfiguration(new Core.Entities.Configurations.SubprefeituraConfiguration());
        }
    }
}