using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Security;
using System.Data;
using System.Diagnostics.Contracts;

namespace Productivity.API.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shared.Models.Entity.Productivity>()
                .HasOne(e => e.Culture).WithMany(x => x.Productivities).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shared.Models.Entity.Productivity>()
                .HasOne(e => e.Region).WithMany(x => x.Productivities).OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Token>().HasOne(e => e.Account).WithMany().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Token>().HasIndex(e => e.TokenStr).IsUnique();

            modelBuilder.Entity<Account>().HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<Account>().HasIndex(u => u.Login).IsUnique();
            modelBuilder.Entity<Account>().ToTable(e => e.HasCheckConstraint("CH_Email_Account", "Email like '%@%.%'"));

            modelBuilder.Entity<Culture>().HasIndex(u => u.Name).IsUnique();
            modelBuilder.Entity<Region>().HasIndex(u => u.Name).IsUnique();

            modelBuilder.Entity<Culture>().ToTable(e => e.HasCheckConstraint("CH_CostToPlant", "CostToPlant > 0"));
            modelBuilder.Entity<Culture>().ToTable(e => e.HasCheckConstraint("CH_PriceToSell", "PriceToSell > 0"));
            modelBuilder.Entity<Shared.Models.Entity.Productivity>()
                .ToTable(e => e.HasCheckConstraint("CH_ProductivityValue", "ProductivityValue > 0"));

            modelBuilder.Entity<Culture>().Property(p => p.PriceToSell).HasPrecision(15, 2);
            modelBuilder.Entity<Culture>().Property(p => p.CostToPlant).HasPrecision(15, 2);
            modelBuilder.Entity<Shared.Models.Entity.Productivity>()
                .Property(p => p.ProductivityValue).HasPrecision(15, 2);

            modelBuilder.Entity<Shared.Models.Entity.Productivity>().Property(p => p.Year).HasColumnType("smallint");

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = Guid.Parse("F0E290A9-9054-4AE7-AF3B-08DAD84FEB5B"),
                    Email = "admin@admin.com",
                    Login = "admin",
                    Password = HashProvider.MakeHash("admin"),
                });

        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Shared.Models.Entity.Productivity> Productivities { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Culture> Cultures { get; set; }
    }
}
