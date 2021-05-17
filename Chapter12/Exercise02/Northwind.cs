using Microsoft.EntityFrameworkCore;
using Northwind.Types;

namespace Exercise02
{
    public class DbNorthwind : DbContext
    {
        private static string db_name {get; set;} = "Northwind.db";
        public DbSet<Customer> Customers{get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, db_name);
            optionsBuilder.UseSqlite($"FileName={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().Property(customer => customer.CustomerID)
                .IsRequired()
                .HasColumnType("nchar")
                .HasMaxLength(5);


            modelBuilder.Entity<Customer>().Property(customer => customer.CompanyName)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(40);

            modelBuilder.Entity<Customer>().Property(customer => customer.City)
                .HasColumnType("nvarchar")
                .HasMaxLength(15);

            modelBuilder.Entity<Customer>().Property(customer => customer.Country)
                .HasColumnType("nvarchar")
                .HasMaxLength(15);
            

        }

    }
}