using System;
using static System.Console;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LinqWithEFCore
{
    class Program
    {
        static void FilterAndSort(){
            using(var db = new Northwind())
            {
                var query = db.Products.Where(product => product.UnitPrice < 10)
                // IQueryable<Product>
                    .OrderByDescending(product => product.UnitPrice);
                
                WriteLine("Products that cost less than $10:");
                foreach(var item in query)
                {
                    WriteLine("{0}: {1} costs {2:$#,##0.00}",
                        item.ProductID, item.ProductName, item.UnitPrice);
                }
                WriteLine();
            }
        }
        static void Main(string[] args)
        {
            FilterAndSort();
        }
    }
}
