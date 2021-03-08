using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Northwind.Database;
using Northwind.Types;
using System.Xml.Serialization;
using System.IO;
using static System.Environment;
using static System.IO.Path;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Exercise02_Extended
{
    class Program
    {
        static void Main(string[] args)
        {
            Write("Input format of serialization (json/xml/binary):");
            string serializationFormat = ReadLine();
            using(DbNorthwind db = new DbNorthwind()){
                List<Category> categories = db.Categories.ToList();
                List<Product> products = db.Products.ToList();
                switch(serializationFormat){
                    case("json"):
                        Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                        using(StreamWriter file = File.CreateText("Categories.json"))
                        {
                            jsonSerializer.Serialize(file, categories);
                        }
                        using(StreamWriter file = File.CreateText("Products.json"))
                        {
                            jsonSerializer.Serialize(file, products);
                        }
                        break;
                    case("xml"):
                        XmlSerializer xmlSerializerCategories = new XmlSerializer(typeof(List<Category>));
                        XmlSerializer xmlSerializerProducts = new XmlSerializer(typeof(List<Product>));
                        using(FileStream file = File.Create("Categories.xml"))
                        {
                            xmlSerializerCategories.Serialize(file, categories);
                        }
                        using(FileStream file = File.Create("Products.xml"))
                        {
                            xmlSerializerProducts.Serialize(file, products);
                        }
                        break;
                    case("binary"):
                        BinaryFormatter binarySerializer = new BinaryFormatter();
                        using(FileStream file = File.Create("Categories.dat"))
                        {
                            binarySerializer.Serialize(file, categories);
                        }
                        using(FileStream file = File.Create("Products.dat"))
                        {
                            binarySerializer.Serialize(file, products);
                        }
                        break;
                    default:
                        WriteLine("Unknown format of serialization");
                        break;
                }
            }

        }
    }
}
