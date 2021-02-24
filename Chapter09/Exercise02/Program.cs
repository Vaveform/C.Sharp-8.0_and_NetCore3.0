using System;
using System.Collections.Generic;
using Shapes;
using System.IO;
using System.Xml.Serialization;
using static System.Environment;
using static System.IO.Path;
using static System.Console;

namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a list of Shape to serialize
            var listOfShapes = new List<Shape>
            {
                new Circle { Colour = "Red", Radius = 2.5},
                new Rectangle { Colour = "Blue", Height = 20.0, Width = 10.0},
                new Circle { Colour = "Green", Radius = 8.0 },
                new Circle { Colour = "Purple", Radius = 12.3 },
                new Rectangle { Colour = "Blue", Height = 45.0, Width = 18.0 }
            };
            //listOfShapes[0].Area = 143;
            XmlSerializer serializer = new XmlSerializer(typeof(List<Shape>), new[] { typeof(Square), typeof(Rectangle), typeof(Circle)});
            string FilePath = Combine(CurrentDirectory, "Shapes.xml");

            using(FileStream stream = File.Create(FilePath)){
                serializer.Serialize(stream, listOfShapes);
            }

            // Display a file
            WriteLine(File.ReadAllText(FilePath));
            WriteLine("Loading shapes from XML:");
            using(FileStream xmlLoad = File.Open(FilePath, FileMode.Open)){
                List<Shape> shapes = serializer.Deserialize(xmlLoad) as List<Shape>;
                foreach (Shape item in shapes)
                {
                    WriteLine("{0} is {1} and has an area of {2:N2}",
                        item.GetType().Name, item.Colour, item.Area);
                }
            }


        }
    }
}
