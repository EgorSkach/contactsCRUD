using contactsCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace contactsCRUD
{
    public class AppDbContext : DbContext
    {
        public DbSet<Contacts> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (builder.IsConfigured)
                return;
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();
            builder.UseSqlServer(configuration.GetConnectionString("Default"));
        }
    }
}
