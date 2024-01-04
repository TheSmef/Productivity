using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Productivity.Shared.Models.Entity;
using Productivity.Shared.Security;
using Productivity.Shared.Utility.Constants;
using System.Data;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace Productivity.MailService.Data.Context
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
            modelBuilder.Entity<Mail>().Property(u => u.Subject).IsRequired(false);
        }

        public DbSet<Mail> Mails { get; set; }
    }
}
