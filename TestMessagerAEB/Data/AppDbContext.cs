using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestMessagerAEB.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration configuration;

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("defaultConnection"));
        }

        public DbSet<Message> messages { get; set; }
    }   
}
