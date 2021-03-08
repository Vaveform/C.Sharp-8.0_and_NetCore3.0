using Microsoft.EntityFrameworkCore;
using Northwind.Types;



namespace Northwind.Database{
    public class DbNorthwind : DbContext
    {
        private string db_name {get;set;} = "Northwind.db";
        public DbSet<Category> Categories{get;set;}
        public DbSet<Product> Products{get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, db_name);
            optionsBuilder.UseSqlite($"FileName={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Using Fluent API for instead of attributes
            modelBuilder.Entity<Category>()
                .Property(category => category.CategoryName)
                .IsRequired().HasMaxLength(15);
            
            modelBuilder.Entity<Category>()
                .Property(category => category.Description)
                .HasColumnType("ntext");

            modelBuilder.Entity<Product>()
                .Property(product => product.ProductName)
                .IsRequired().HasMaxLength(40);
            
            modelBuilder.Entity<Product>()
                .Property(product => product.Cost)
                .HasColumnName("UnitPrice")
                .HasColumnType("money");
            
            modelBuilder.Entity<Product>()
                .Property(product => product.Stock)
                .HasColumnName("UnitsInStock");

        }
    }
}