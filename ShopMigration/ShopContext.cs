using Microsoft.EntityFrameworkCore;
using ShopMigration.Entities;

namespace ShopMigration
{
    public class ShopContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<ProductCategory> ProductsCategoriesList { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<OrderDetails> OrderDetailsList { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseSqlServer("Data Source=.;Initial Catalog=Shop;Integrated Security=true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>(b =>
            {
                b.HasOne(pc => pc.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(pc => pc.CategoryId);

                b.HasOne(pc => pc.Product)
                    .WithMany(p => p.ProductCategories)
                    .HasForeignKey(pc => pc.ProductId);
            });

            modelBuilder.Entity<Category>(b =>
            {
                b.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                b.Property(p => p.Price).HasPrecision(10, 2);
            });

            modelBuilder.Entity<OrderDetails>(b =>
            {
                b.HasOne(od => od.Order)
                    .WithMany(o => o.OrderDetailsList)
                    .HasForeignKey(od => od.OrderId);

                b.HasOne(od => od.Product)
                    .WithMany(p => p.OrderDetailsList)
                    .HasForeignKey(od => od.ProductId);
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.Property(od => od.Date).HasDefaultValueSql("GETUTCDATE()");

                b.HasOne(od => od.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId);
            });

            modelBuilder.Entity<Customer>(b =>
            {
                b.Property(c => c.FirstName)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(c => c.LastName)
                    .HasMaxLength(50)
                    .IsRequired();

                b.Property(c => c.Phone)
                    .HasMaxLength(15)
                    .IsRequired();

                b.Property(c => c.Email)
                    .HasMaxLength(100)
                    .IsRequired();
            });
        }
    }
}