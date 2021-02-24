using System;
using static System.Console;

namespace WorkingWithText
{
    class Program
    {
        static void Main(string[] args)
        {
            string city = "London";

            // Get a length of string
            WriteLine($"{city} is {city.Length} characters long.");

            // Indexing
            WriteLine($"First char is {city[0]} and third is {city[2]}.");

            // Spliting string
            string cities = "Paris,Berling,Madrid,New York";

            string[] citiesArray = cities.Split(',');

            foreach(string item in citiesArray){
                WriteLine(item);
            }

            // Getting part of a string
            string fullName = "Alan Jones";

            int indexOfTheSpace = fullName.IndexOf(' ');

            string firstName = fullName.Substring(startIndex: 0, length: indexOfTheSpace);

            string lastName = fullName.Substring(startIndex: indexOfTheSpace + 1);

            WriteLine($"{lastName}, {firstName}");

            // Checking a string for content
            string company = "Microsoft";
            bool startsWithM = company.StartsWith("M");
            bool containsN = company.Contains("N");
            WriteLine($"Starts with M: {startsWithM}, contains an N: {containsN}");

            
            string recombined = string.Join(" => ", citiesArray);
            WriteLine(recombined);

            string fruit = "Apples";
            decimal price = 0.39M;
            DateTime when = DateTime.Today;

            WriteLine($"{fruit} cost {price:C} on {when:dddd}.");

            WriteLine(string.Format("{0} cost {1:C} on {2:dddd}.", fruit, price, when));

        }
    }
}
