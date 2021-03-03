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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DbNorthwind())
            {
                BinaryFormatter binarySerializer = new BinaryFormatter();
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Category));
                Newtonsoft.Json.JsonSerializer jsonSerializer = new Newtonsoft.Json.JsonSerializer();
                using(FileStream file = File.Create("Categories"))
                {
                    foreach(var item in db.Categories)
                    {
                        binarySerializer.Serialize(file, item);
                        xmlSerializer.Serialize(file, item);
                        // jsonSerializer.Serialize(file, item);
                    }
                }

            }
        }
    }
}
