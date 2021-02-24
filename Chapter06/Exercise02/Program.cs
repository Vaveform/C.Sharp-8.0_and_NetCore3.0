using System;
using System.Collections.Generic;
using static System.Console;
using Shapes;

namespace Exercise02
{
    class Base{
        private int secure{get; set;}
        public Base(){
            secure = 0;
        }
        virtual public int GetSecure(){
            return secure;
        }
        virtual public Base UpSecure(){
            secure++;
            return this;
        }

    }
    class Derived : Base{
        public Derived() : base(){}
        public int test_number {get; set;}

    }
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Rectangle(3, 4.5);
            WriteLine($"Rectangle H: {r.Height}, W: {r.Width}, Area: {r.Area}");

            var s = new Square(5);
            WriteLine($"Square H: {s.Height}, W: {s.Width}, Area: {s.Area}");

            var c = new Circle(3.5);
            WriteLine($"Circle H: {c.Height}, W: {c.Width}, Area: {c.Area}");

            Base b = new Base();
            Derived d = new Derived();

            b.UpSecure().UpSecure();
            d.UpSecure();

            WriteLine($"Base b private field secure: {b.GetSecure()}");
            WriteLine($"Derived d derived private field secure: {d.GetSecure()}");
        }
    }
}
