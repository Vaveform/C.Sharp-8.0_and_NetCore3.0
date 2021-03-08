using Microsoft.EntityFrameworkCore;
using Northwind.Types;
using System;
using System.IO;


namespace Northwind.Database
{
    public class DbNorthwind : DbContext
    {
        static private string dbName{get;set;} = "Northwind.db"; 
        // table for Categories
        public DbSet<Category> Categories {get; set;}
        public DbSet<Product> Products{get;set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // if(Directory.GetFiles(dbName).Length == 0){
            //     throw new Exception($"{dbName} not exist in current directory");
            // }
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, dbName);
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API - not null for CategoryName
            modelBuilder.Entity<Category>()
                .Property(category => category.CategoryName)
                .IsRequired();

            // Fluent API - for property ProductName
            modelBuilder.Entity<Product>()
                .Property(product => product.ProductName)
                .IsRequired().HasMaxLength(40);
            
            // Fluent API - for property Cost
            modelBuilder.Entity<Product>()
                .Property(product => product.Cost)
                .HasColumnName("UnitPrice").HasColumnType("money");

            // Stock
            modelBuilder.Entity<Product>()
                .Property(product => product.Stock)
                .HasColumnName("UnitsInStock");
        }
    }
}