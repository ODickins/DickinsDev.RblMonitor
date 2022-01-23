using DickinsDev.RblMonitor.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DickinsDev.RblMonitor.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Nameserver> Nameservers { get; set; }
        public DbSet<DNSBL> DNSBLs { get; set; }
        public DbSet<EmailTarget> EmailTargets { get; set; }
        public DbSet<IPMonitor> IPMonitors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=database.db");

    }
}
