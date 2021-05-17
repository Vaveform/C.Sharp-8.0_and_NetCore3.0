using System;
using static System.Console;
using System.Collections.Generic;
using Packt.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml.Linq;

namespace LinqWithEFCore
{
    class Program
    {
        static void FilterAndSort(){
            using(var db = new Northwind())
            {
                var query = db.Products.ProcessSequence().Where(product => product.UnitPrice < 10)
                // IQueryable<Product>
                    .OrderByDescending(product => product.UnitPrice)
                    // IOrderQueryable<Product>
                    .Select(product => new // anonymous type with projection initializers - инициализатор с проекций - в анонимный инициализатор передаются параметры
                        {
                            product.ProductID,
                            product.ProductName,
                            product.UnitPrice
                        }
                    );
                
                WriteLine("Products that cost less than $10:");
                foreach(var item in query)
                {
                    WriteLine("{0}: {1} costs {2:$#,##0.00}",
                        item.ProductID, item.ProductName, item.UnitPrice);
                }
                WriteLine();
            }
        }
        static void JoinCategoriesAndProducts(){
            using(var db = new Northwind()){
                // join every product to its category to return 77 matches
                var queryJoin = db.Categories.Join(
                    // outer - this - db.Categories, inner - products of database
                    inner: db.Products,
                    // lambda which select key of outer sequence (from db.Categories)
                    outerKeySelector: category => category.CategoryID,
                    // lambda which select key of inner sequence (from db.Products)
                    innerKeySelector: product => product.CategoryID,
                    // lambda which building IQueryable with anonymous type (anonymous type - reference type)
                    // and has parametrs which are sequences.
                    resultSelector: (c, p) => new { c.CategoryName, p.ProductName, p.ProductID }
                ).OrderBy(cp => cp.CategoryName); // here we order IQueryable sequence by field of anonymous type

                foreach(var item in queryJoin)
                {
                    WriteLine("{0}: {1} is in {2}.",
                        arg0: item.ProductID,
                        arg1: item.ProductName,
                        arg2: item.CategoryName
                    );
                }
            }
        }
        static void GroupJoinCategoriesAndProducts()
        {
            using(var db = new Northwind()){
                // group all products by their category to return 8 matches
                // AsEnumerable is needed because not all LINQ extension methods
                // can be converted from expression trees into other query syntax like SQL.
                // AsEnumerable forces query proccessing to use LINQ to EF Core only to bring data into the application
                // and then use LINQ to Objects to execute more complex queries.
                // if AsEnumerable do not call, applicatopn throw exception NotImplementationException
                var queryGroup = db.Categories.AsEnumerable().GroupJoin(    
                    inner: db.Products,
                    outerKeySelector: category => category.CategoryID,
                    innerKeySelector: product => product.CategoryID,
                    resultSelector: (c, matchingProducts) => new {
                        c.CategoryName,
                        Products = matchingProducts.OrderBy(p => p.ProductName)
                    }
                );

                foreach(var item in queryGroup){
                    WriteLine("{0} has {1} products.",
                        arg0: item.CategoryName,
                        arg1: item.Products.Count()
                    );
                    foreach(var product in item.Products){
                        WriteLine($"    {product.ProductName}");
                    }
                }
            }
        }
        static void AggregateProducts()
        {
            using(var db = new Northwind()){
                WriteLine("{0,-25} {1,10}",
                    arg0: "Product count:",
                    arg1: db.Products.Count()
                );

                WriteLine("{0,-25} {1,10:$#,##0.00}",
                    arg0: "Highest product price:",
                    arg1: db.Products.Max(p => p.UnitPrice)
                );

                WriteLine("{0,-25} {1,10:N0}",
                    arg0: "Sum of units in stock:",
                    arg1: db.Products.Sum(p => p.UnitsInStock)
                );
                
                WriteLine("{0,-25} {1,10:N0}",
                    arg0: "Sum of units on order:",
                    arg1: db.Products.Sum(p => p.UnitsOnOrder)
                );

                WriteLine("{0,-25} {1,10:$#,##0.00}",
                    arg0: "Average unit price:",
                    arg1: db.Products.Average(p => p.UnitPrice)
                );

                WriteLine("{0,-25} {1,10:$#,##0.00}",
                    arg0: "Value of units in stock:",
                    // In Entity Framework Core 3.0 and later, LINQ operations that cannot be translated to
                    // SQL are no longer automatically evaluated on the client side, so you must explicitly
                    // call AsEnumerable to force further processing of the query on the client.
                    // If you want to calculate somthing in LINQ method: some arphmentic operations you
                    // should call AsEnumerable() before
                    arg1: db.Products.AsEnumerable().Sum(p => p.UnitPrice * p.UnitsInStock)
                );
            }

        }

        static void CustomExtensionMethods()
        {
            using (var db = new Northwind())
            {
                WriteLine("Mean units in stock: {0:N0}",
                    db.Products.Average(p => p.UnitsInStock));

                WriteLine("Mean unit price: {0:$#,##0.00}",
                    db.Products.Average(p => p.UnitPrice));

                WriteLine("Median units in stock: {0:N0}",
                    db.Products.Median(p => p.UnitsInStock));

                WriteLine("Median unit price: {0:$#,##0.00}",
                    db.Products.Median(p => p.UnitPrice));

                WriteLine("Mode units in stock: {0:N0}",
                    db.Products.Mode(p => p.UnitsInStock));

                WriteLine("Mode unit price: {0:$#,##0.00}",
                    db.Products.Mode(p => p.UnitPrice));
            }
        }

        static void OutputProductsAsXml()
        {
            using(var db = new Northwind())
            {
                var productsForXml = db.Products.ToArray();
                // var xmlComprehension = new XElement("products",
                //     from p in productsForXml
                //     select new XElement("product",
                //         new XAttribute("id", p.ProductID),
                //         new XAttribute("price", p.UnitPrice),
                //         new XElement("name", p.ProductName)
                //     )
                // );

                var xml = new XElement("products",
                    productsForXml.Select(product => new XElement("product",
                        new XAttribute("id", product.ProductID),
                        new XAttribute("price", product.UnitPrice),
                        new XElement("name", product.ProductName)
                    ))
                );

                WriteLine(xml.ToString()); 
            }
        }

        static void ProcessSettings()
        {
            XDocument doc = XDocument.Load("settings.xml");
            // Descedants return IEnumerable of subnodes for
            // current xml document or element 
            var appSettings = doc.Descendants("appSettings")
                .Descendants("add")
                .Select(node => new {
                    Key = node.Attribute("key").Value,
                    Value = node.Attribute("value").Value
                }).ToArray();
            
            foreach(var item in appSettings)
            {
                WriteLine($"{item.Key}: {item.Value}");
            }
        }

        static void Main(string[] args)
        {
            // FilterAndSort();
            // //JoinCategoriesAndProducts();
            // //GroupJoinCategoriesAndProducts();
            // //AggregateProducts();



            // var names = new string[] { "Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin", "Toby", "Creed"};

            // // Comprehension syntax of LINQ
            // var comprehension_query = from name in names
            //     where name.Length > 4
            //     orderby name.Length, name
            //     select name;


            // // LINQ syntax
            // var query = names
            //     .Where(name => name.Length > 4)
            //     .OrderBy(name => name.Length)
            //     .ThenBy(name => name);

            // // Comprehension syntax has not equivalent of Skip and Take for example
            // // We can combine to syntax
            // var combined_query = (from name in names where name.Length > 4 select name).Skip(80).Take(10);

            // CustomExtensionMethods();

            // OutputProductsAsXml();

            ProcessSettings();

        }
    }
}
