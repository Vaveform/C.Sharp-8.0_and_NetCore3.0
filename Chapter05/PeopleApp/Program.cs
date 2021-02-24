using System;
using Packt.Shared;
using static System.Console;

namespace PeopleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var Bob = new Person(); 
            Bob.Name = "Bob Smith";
            Bob.DateOfBirth = new DateTime(1965, 12, 22);
            Bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;
            Bob.BucketList = WondersOfTheAncientWorld.HangingGardensOfBabylon | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;
            // or
            // Bob.BucketList = (WondersOfTheAncientWorld)18;
            WriteLine(
                format: "{0} was born on {1:dddd, d MMMM yyyy}",
                arg0 : Bob.Name,
                arg1 : Bob.DateOfBirth);
            var alice = new Person
            {
                Name = "Alice Jones",
                DateOfBirth = new DateTime(1998, 3, 7)    
            };

            WriteLine(
                format: "{0} was born on {1:dd MMM yy}",
                arg0 : alice.Name,
                arg1 : alice.DateOfBirth
            );

            WriteLine(
                "{0}'s favorite wonder is {1}. It's integer is {2}.",
                arg0: Bob.Name,
                arg1: Bob.FavoriteAncientWonder,
                arg2: (int)Bob.FavoriteAncientWonder
            );

            WriteLine($"{Bob.Name}'s bucket list is {Bob.BucketList}");


            Bob.Children.Add(new Person { Name = "Alfred"});
            Bob.Children.Add(new Person { Name = "Zoe"});

            Console.WriteLine($"{Bob.Name} has {Bob.Children.Count} children:");

            foreach(var Child in Bob.Children){
                WriteLine($"    {Child.Name}");
            }

            BankAccount.InterestRate = 0.012M;

            // var jonesAccount = new BankAccount();
            // jonesAccount.AccountName = "Mrs. Jones";
            // jonesAccount.Balance = 2400;
            BankAccount jonesAccount = new BankAccount
            {
                AccountName = "Mrs. Jones",
                Balance = 2400
            };

            WriteLine(format: "{0} earned {1:C} interest.",
                arg0: jonesAccount.AccountName,
                arg1: jonesAccount.Balance * BankAccount.InterestRate
            );

            // var gerrierAccount = new BankAccount();
            // gerrierAccount.AccountName = "Ms. Gerrier";
            // gerrierAccount.Balance = 98;
            BankAccount gerrierAccount = new BankAccount
            {
                AccountName = "Ms. Gerrier",
                Balance = 98
            };

            WriteLine(format: "{0} earned {1:C} interest.",
                arg0: gerrierAccount.AccountName,
                arg1: gerrierAccount.Balance * BankAccount.InterestRate
            );


            WriteLine($"{Bob.Name} is a {Person.Species}");
            WriteLine($"{Bob.Name} was born on {Bob.HomePlanet}");

            var blankPerson = new Person();
            WriteLine(format:
                "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}",
                arg0: blankPerson.Name,
                arg1: blankPerson.HomePlanet,
                arg2: blankPerson.Instantiated
            );

            var gunny = new Person("Gunny", "Mars");
            WriteLine(format:
                "{0} of {1} was created at {2:hh:mm:ss} on a {2:dddd}",
                arg0: gunny.Name,
                arg1: gunny.HomePlanet,
                arg2: gunny.Instantiated
            );

            Bob.WriteToConsole();
            WriteLine(Bob.GetOrigin());


            (string, int) fruit = Bob.GetFruit();
            WriteLine($"{fruit.Item1}, {fruit.Item2} there are.");

            var fruitNamed = Bob.GetNamedFruit();
            WriteLine($"There are {fruitNamed.Number} {fruitNamed.Name}.");
            

            // Using ValueTuple (not Tuple)
            var test = (11, 22, 34, 45, 15, 12);
            WriteLine(test.GetType());
            // ValueTuple mutable - Tuple immutable
            test.Item2 += 11;
            WriteLine(test);

            // Buldin tuple by future named "tuple name inference"
            var thing1 = ("Neville", 4);
            // C# 7.0
            WriteLine($"{thing1.Item1} has {thing1.Item2} children");

            // Name by last names of fields C# 7.1
            var thing2 = (Bob.Name, Bob.Children.Count);
            WriteLine($"{thing2.Name} has {thing2.Count} children");
            var sum = 4.5;
            var count = 3;
            var test3 = (sum, count);
            Console.WriteLine($"Sum of {test3.count} elements is {test3.sum}.");


            // Deconstruction example
            (string fruitName, int fruitNumber) = Bob.GetFruit();
            WriteLine($"Deconstructed: {fruitName}, {fruitNumber}");

            // Accessible to use tuple with large number of elements
            var t = (11, 22, 34, 45, 15, 12);
            WriteLine(t);

            // Should be 45
            WriteLine(t.Item4);

            //var a = 1;
            //var test2 = (a, b: 2, 3);
            //Console.WriteLine($"The 1st element is {test2.Item1} (same as {test2.a}).");
            //Console.WriteLine($"The 2nd element is {test2.Item2} (same as {test2.b}).");
            //Console.WriteLine($"The 3rd element is {test2.Item3}.");

            WriteLine(Bob.SayHello());
            WriteLine(Bob.SayHello("Emily"));

            WriteLine(Bob.OptionalParametrs());
            WriteLine(Bob.OptionalParametrs("Jump!", 98.5));

            // Optional parametrs often combined with naming parametrs
            WriteLine(Bob.OptionalParametrs(
                number: 52.7, command: "Hide!"
            ));

            // Can use optional parametrs, named parametrs and simple parametrs
            WriteLine(Bob.OptionalParametrs("Poke!" /* - Parametr*/, active: false /* - Named parametr and then optional parametr number*/));


            int a = 10;
            int b = 20;
            int c = 30;

            WriteLine($"Before: = {a}, b = {b}, c = {c}");

            Bob.PassingParametrs(a, ref b, out c);

            WriteLine($"After: = {a}, b = {b}, c = {c}");

            // In C# 7.0 we can simplify code
            int d = 10;
            int e = 20;

            WriteLine(
                $"Before: d = {d}, e = {e}, f doesn't exist yet!"
            );

            // simplified C# 7.0 syntax for the out parametr
            Bob.PassingParametrs(d, ref e, out int f);

            WriteLine($"After: d = {d}, e = {e}, f = {f}");

            var Sam = new Person{
                Name = "Sam",
                DateOfBirth = new DateTime(1972, 1, 27)
            };

            WriteLine(Sam.Origin);
            WriteLine(Sam.Greeting);
            WriteLine(Sam.Age);

            Sam.FavoriteIceCream = "Chocolate Fudge";

            WriteLine($"Sam's favorite ice-cream flavor is {Sam.FavoriteIceCream}.");

            Sam.FavoritePrimaryColor = "Red";

            WriteLine($"Sam's favorite primary color is {Sam.FavoritePrimaryColor}.");

            Sam.Children.Add(new Person {Name = "Charlie"});
            Sam.Children.Add(new Person {Name = "Ella"});

            WriteLine($"Sam's first child is {Sam.Children[0].Name}");
            WriteLine($"Sam's second child is {Sam.Children[1].Name}");
            WriteLine($"Sam's first child is {Sam[0].Name}");
            WriteLine($"Sam's second child is {Sam[1].Name}");
        }
    }
}
