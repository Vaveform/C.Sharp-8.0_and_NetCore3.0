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
        }
    }
}