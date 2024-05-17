using Microsoft.EntityFrameworkCore;
using Project_TPMyAPI.Models;


namespace Project_TPMyAPI.Data
{
    public class AppDbContext : DbContext
    {
        private IConfiguration _config;

        public AppDbContext(IConfiguration config, DbContextOptions<AppDbContext> options) : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("SQLConnection"));
        }

        public DbSet<UsersEF> UsersEF { get; set; }
        public DbSet<GamesEF> GamesEF { get; set; }

    }
}
