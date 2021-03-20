using System;
using System.Linq;
using static System.Console;

namespace LinqWithObjects
{
    class Program
    {
        static void LinqWithArrayOfStrings()
        {
            // We can use predicate type of delegate
            // delegates have special types: 
            // predicate (return bool and receive any type),
            // func(can receive up to 16 T type objects and return any type)
            // action - receive up to 16 T type objects and return void
            // Predicate with anonymous function:
            Predicate<string> statement = (string name) => {
                return name.Length > 4;
            };
            bool NameLongerThanFour(string name)
            {
                return name.Length > 4;
            }
            // statement = NameLongerThanFour;
            var names = new string[]{ "Michael", "Pam", "Jim", "Dwight", "Angela",
                "Kevin", "Toby", "Creed"};
            //var query = names.Where(new Func<string, bool>(statement));
            //var query = names.Where(new Func<string, bool>(NameLongerThanFour));

            // simplifying code: Func<string, bool> == method NameLongerThanFour for Where
            // var query = names.Where(NameLongerThanFour);

            // simplifying wtih lambda expression
            // var query = names.Where(name => name.Length > 4);

            // sorting using OrderBy
            // and then using ThenBy in the chain of LINQ
            // difference between OrderBy and ThenBy : OrderBy overwrites previous OrderBy
            // ThenBy does not overwrite previous OrderBy or ThenBy
            var query = names.Where(name => name.Length > 4).OrderBy(name => name.Length).ThenBy(name => name);
            foreach(string item in query)
            {
                WriteLine(item);
            }

        }
        static void LinqWithArrayOfException()
        {
            var errors = new Exception[]
            {
                new ArgumentException(),
                new SystemException(),
                new IndexOutOfRangeException(),
                new InvalidOperationException(),
                new NullReferenceException(),
                new InvalidCastException(),
                new OverflowException(),
                new DivideByZeroException(),
                new ApplicationException()
            };

            var numberErrors = errors.OfType<ArithmeticException>();

            foreach(var error in numberErrors)
            {
                WriteLine(error);
            }
        }
        static void Main(string[] args)
        {
            // LinqWithArrayOfStrings();
            LinqWithArrayOfException();
        }
    }
}
