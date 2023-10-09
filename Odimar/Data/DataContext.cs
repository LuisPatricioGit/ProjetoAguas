using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Odimar.Data.Entities;

namespace Odimar.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailTemp> OrderDetailTemps { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Parish> Parishes { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<County>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<OrderDetailTemp>()
               .Property(p => p.Price)
               .HasColumnType("decimal(18,2)");


            modelBuilder.Entity<OrderDetail>()
              .Property(p => p.Price)
              .HasColumnType("decimal(18,2)");


            base.OnModelCreating(modelBuilder);
        }



        //Habilita a regra de apagar em cascata chama-se cascate delete rule
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    var cascadeFKs = modelBuilder.Model
        //        .GetEntityTypes()
        //        .SelectMany(t => t.GetForeignKeys())
        //        .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        //    foreach (var fk in cascadeFKs)
        //    {
        //        fk.DeleteBehavior = DeleteBehavior.Restrict;
        //    }
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
