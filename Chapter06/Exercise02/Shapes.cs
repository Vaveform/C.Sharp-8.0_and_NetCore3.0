using System;
using System.Collections.Generic;
using static System.Console;

namespace Shapes{
    public class Shape{
        protected virtual double CalculateShapeArea(){
            return Height * Width;
        }
        public double Height{get;set;}
        public double Width{get;set;}
        public double Area{get;set;}
        public Shape(double height, double width){
            Height = height;
            Width = width;
            Area = CalculateShapeArea();
        }
        public Shape(){
            Height = 0;
            Width = 0;
            Area = CalculateShapeArea();
        }
    }
    public class Rectangle : Shape{
        public Rectangle(double height, double width) : base(height, width){}
    }
    public class Square : Shape{
        public Square(double side) : base(side, side){}
    }
    public class Circle : Shape{
        public double Radius {get;set;}
        protected override double CalculateShapeArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
        public Circle(double radius){
            Radius = radius;
            Height = Width = 2 * Radius;
            Area = CalculateShapeArea();
        } 
    }
}