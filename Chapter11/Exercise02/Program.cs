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

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryFormatter binarySerializer = new BinaryFormatter();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Category));
            Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
            using (var db = new DbNorthwind())
            {
                using(FileStream file = File.Create("Categories.dat"))
                {
                    binarySerializer.Serialize(file, db.Categories.ToList());
                    // foreach(var item in db.Categories.ToList())
                    // {
                    //     binarySerializer.Serialize(file, item);
                    //     //xmlSerializer.Serialize(file, item);
                    //     // jsonSerializer.Serialize(file, item);
                    // }
                }

            }

            using(FileStream file = File.Open("Categories.dat", FileMode.Open)){
                var cats = (List<Category>)binarySerializer.Deserialize(file);
                foreach(var cat in cats){
                    WriteLine($"Category ID {cat.CategoryID}, category Name {cat.CategoryName}");
                }
            }
        }
    }
}
