using Domain;
using Domain.DbTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    public class YourLabDbContext : DbContext
    {
        public YourLabDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Domains> Domains { get; set; }
        public DbSet<Subdomains> Subdomains { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YourLabDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);
        }
    }

    public class YourLabDbContextFactory : IDesignTimeDbContextFactory<YourLabDbContext>
    {
        public YourLabDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourLabDbContext>();
            optionsBuilder.UseSqlServer();

            return new YourLabDbContext(optionsBuilder.Options);
        }
    }
}
