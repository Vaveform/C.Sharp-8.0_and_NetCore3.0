using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using static System.Console;

namespace Shapes{
    public class Shape{
        public string Colour{get; set;}
        public virtual double Height{get;set;}
        public virtual double Width{get;set;}
        public virtual double Area{get;set;}
    }
    public class Rectangle : Shape{
        public override double Area{
            get => Height * Width;
        }
    }
    public class Square : Shape{
        public override double Area{
            get => Height * Width;
        }
    }
    public class Circle : Shape{
        public double Radius {get;set;}
        public override double Height
        {
            get => 2 * Radius;
        }
        public override double Width
        {
            get => 2 * Radius;
        }
        public override double Area{
            get => Math.PI * Math.Pow(Radius, 2);
        }
    }
}