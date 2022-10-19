using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DashboardAPI.Context
{

    public class DashboardContext : DbContext
    {
        public DashboardContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public DbSet<Models.Product> Product { get; set; }
        public DbSet<Models.Order> Order { get; set; }
        public DbSet<Models.Demand> Demand { get; set; }
        public DbSet<Models.Team> Team { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite(connectionString: Configuration.GetConnectionString("Connection"));
            }
    }
}
