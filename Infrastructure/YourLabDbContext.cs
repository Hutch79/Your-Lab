using Domain.DbObjects;
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
        public DbSet<RootDomain> Domains { get; set; }
        public DbSet<Subdomain> Subdomains { get; set; }
        public DbSet<DnsRecord> DnsRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(YourLabDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<string>().HaveMaxLength(256);
        }
    }


    public class YourLabDbContextFactory : IDesignTimeDbContextFactory<YourLabDbContext>
    {
        public YourLabDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<YourLabDbContext>();
            optionsBuilder.UseNpgsql();

            return new YourLabDbContext(optionsBuilder.Options);
        }
    }
}