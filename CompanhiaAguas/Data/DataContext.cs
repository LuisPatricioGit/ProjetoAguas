using CompanhiaAguas.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanhiaAguas.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<ConsumptionTable> ConsumptionTables { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<ContractType> ContractTypes { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        
        public DbSet<Meter> Meters { get; set; }

        public DbSet<SupplyPoint> SupplyPoints { get; set; }

        public DbSet<Tier> Tiers { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
    }
}
