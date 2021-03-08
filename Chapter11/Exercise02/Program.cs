using System;
using Northwind.Types;
using Northwind.Database;
using System.Xml.Serialization;
using System.IO;
using static System.Environment;
using static System.IO.Path;
using static System.Console;
using NuJson = System.Text.Json.JsonSerializer;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryFormatter binarySerializer = new BinaryFormatter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Category>));
            Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            using (var db = new DbNorthwind())
            {
                // Eager loading
                List<Category> categories = db.Categories.Include(c => c.Products).ToList();
                using(FileStream file = File.Create("CategoriesAndProducts.dat"))
                {
                    binarySerializer.Serialize(file, categories);
                }
                using(FileStream file = File.Create("CategoriesAndProducts.xml"))
                {
                    xmlSerializer.Serialize(file, categories);
                }
                using(StreamWriter file = File.CreateText(Combine(CurrentDirectory, "CategoriesAndProducts.json")))
                {
                    jsonSerializer.Serialize(file, categories);
                }


            }

            // Deserialed all formats to check
            using(FileStream file = File.Open("CategoriesAndProducts.xml", FileMode.Open)){
                var cats = xmlSerializer.Deserialize(file) as List<Category>;
                foreach(var cat in cats){
                    WriteLine($"Category ID {cat.CategoryID}, category Name {cat.CategoryName}, {cat.Products.Count}");
                }
            }

            using(FileStream file = File.Open("CategoriesAndProducts.dat", FileMode.Open)){
                var cats = binarySerializer.Deserialize(file) as List<Category>;
                foreach(var cat in cats){
                    WriteLine($"Category ID {cat.CategoryID}, category Name {cat.CategoryName}, {cat.Products.Count}");
                }
            }

            using(StreamReader file = File.OpenText("CategoriesAndProducts.json"))
            {
                var cats = jsonSerializer.Deserialize(file, typeof(List<Category>)) as List<Category>;
                foreach(var cat in cats){
                    WriteLine($"Category ID {cat.CategoryID}, category Name {cat.CategoryName}, {cat.Products.Count}");
                }
            }
        }
    }
}
