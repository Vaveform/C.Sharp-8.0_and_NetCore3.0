using System;
using System.Collections.Generic;
using static System.Console;

namespace Packt.Shared
{
    class Program
    {
        private static void Harry_Shout(object sender, EventArgs e){
            Person p = (Person)sender;
            WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
        }
        private static void Harry_Test(object sender, EventArgs e){
            Person p = (Person)sender;
            WriteLine($"Harry_Test, sender {p.Name}");
        }
        private static void Harry_Minus(object sender, EventArgs e){
            WriteLine("Gonep");
        }
        private static void c_ThresholdReached(object sender, ThresholdReachedArgs e){
            WriteLine("The threshold of {0} was reached at {1}.", e.Threshold,  e.TimeReached);
            Environment.Exit(0);
        }
        static void Main(string[] args)
        {
            var Harry = new Person { Name = "Harry" };
            var Mary = new Person { Name = "Mary" };
            var Jill = new Person { Name = "Jill" };

            // call instance method
            var baby1 = Mary.ProcreateWith(Harry);

            // call static method
            var baby2 = Person.Procreate(Harry, Jill);

            // call an operator
            var baby3 = Harry * Mary;

            WriteLine($"{Harry.Name} has {Harry.Children.Count} children");
            WriteLine($"{Mary.Name} has {Mary.Children.Count} children");
            WriteLine($"{Jill.Name} has {Jill.Children.Count} children");

            WriteLine(
                format: "{0}'s first child is named \"{1}\".",
                arg0: Harry.Name,
                arg1: Harry.Children[0].Name
            );
            
            WriteLine($"5! is {Person.Factorial(5)}");

            Harry.Shout += Harry_Shout;

            Harry.Poke();
            Harry.Poke();
            Harry.Poke();
            Harry.Poke();

            // Counter c = new Counter(new Random().Next(10));
            // c.ThresholdReached += c_ThresholdReached;
            // WriteLine("press 'a' key to increase total");
            // while (ReadKey(true).KeyChar == 'a')
            // {
            //     WriteLine("adding one");
            //     c.Add(1);
            // }

            Person[] people =
            {
                new Person { Name = "Simon" },
                new Person { Name = "Jenny" },
                new Person { Name = "Adam" },
                new Person { Name = "Richard" }
            };

            WriteLine("Initial list of people:");
            foreach(var person in people)
            {
                WriteLine($"{person.Name}");
            }

            WriteLine("Use Person's IComparable implementation to sort: ");
            Array.Sort(people);
            foreach (var person in people)
            {
                WriteLine($"{person.Name}");
            }

            WriteLine("Use PersonComparer's IComparer implementation to sort:");
            Array.Sort(people, new PersonComparer());
            foreach(var person in people)
            {
                WriteLine($"{person.Name}");
            }

            var t1 = new Thing();
            t1.Data = 42;
            WriteLine($"Thing with a string: {t1.Process(42)}");

            var t2 = new Thing();
            t2.Data = "apple";
            WriteLine($"Thing with a string: {t2.Process("apple")}");

            var gt1 = new GenericThing<int>();
            gt1.Data = 42;
            WriteLine($"GenericThing with an integer: {gt1.Process(42)}");

            var gt2 = new GenericThing<string>();
            gt2.Data = "apple";
            WriteLine($"GenericThing with a string: {gt2.Process("apple")}");

            string number1 = "4";
            WriteLine("{0} squared is {1}", arg0: number1, arg1: Squarer.Square<string>(number1));

            byte number2 = 3;
            // It is unnecessary to set generic type in generic method in <>
            WriteLine("{0} squared is {1}", arg0: number2, arg1: Squarer.Square(number2));

            var dv1 = new DisplacementVector(3, 5);
            var dv2 = new DisplacementVector(-2, 7);
            var dv3 = dv1 + dv2;
            WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({dv3.X}, {dv3.Y})");

            Employee John = new Employee
            {
                Name = "John Jones",
                DateOfBirth = new DateTime(1990, 7, 28)
            };
            John.WriteToConsole();

            John.EmployeeCode = "JJ001";
            John.HireDate = new DateTime(2014, 11, 23);
            WriteLine($"{John.Name} was hired on {John.HireDate:dd/MM/yy}");

            WriteLine(John.ToString());

            Employee aliceInEmployee = new Employee{ Name = "Alice", EmployeeCode = "AA123"};

            Person aliceInPerson = aliceInEmployee;

            aliceInEmployee.WriteToConsole();
            aliceInPerson.WriteToConsole();

            WriteLine(aliceInEmployee.ToString());
            WriteLine(aliceInPerson.ToString());

            // is - checking opportunity to convert
            if(aliceInPerson is Employee){
                WriteLine($"{nameof(aliceInPerson)} IS an Employee");

                Employee explicitAlice = (Employee)aliceInPerson;
                // safely do something with explicitAlice
            }
            
            // as - Cast type. If something goes wrong operator as return null
            Employee aliceAsEmployee = aliceInPerson as Employee;
            
            if(aliceAsEmployee != null)
            {
                WriteLine($"{nameof(aliceInPerson)} AS an Employee");
                // do something with aliceAsEmployee
            }

            try{
                John.TimeTravel(new DateTime(1999, 12, 31));
                John.TimeTravel(new DateTime(1950, 12, 25));
            }
            catch(PersonException ex)
            {
                WriteLine(ex.Message);
            }


            string email1 = "pamela@test.com";
            string email2 = "ian&test.com";

            WriteLine(
                "{0} is a valid e-mail address: {1}",
                arg0: email1,
                arg1: email1.IsValidEmail()
            );

            WriteLine(
                "{0} is a valid e-mail address: {1}",
                arg0: email2,
                arg1: email2.IsValidEmail()
            );

            email1.PrintHello();

        }
    }
    public class Counter
    {
        private int threshold;
        private int total;
        public Counter(int passedThreshold)
        {
            threshold = passedThreshold;
        }

        public void Add(int x){
            total+= x;
            if(total >= threshold){
                ThresholdReachedArgs args = new ThresholdReachedArgs();
                args.Threshold = threshold;
                args.TimeReached = DateTime.Now;
                OnThresholdReached(args);
            }
        }
        protected virtual void OnThresholdReached(ThresholdReachedArgs args){
            EventHandler<ThresholdReachedArgs> handler = ThresholdReached;
            if(handler != null){
                handler(this, args);
            }
        }
        public event EventHandler<ThresholdReachedArgs> ThresholdReached;
    }
    public class ThresholdReachedArgs : EventArgs
    {
        public int Threshold {get; set;}
        public DateTime TimeReached {get; set;}
    }
}
