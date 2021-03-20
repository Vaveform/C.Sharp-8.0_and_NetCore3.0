using System;
using System.Collections.Generic;       // for IEnumerable<T>
using System.Linq;                      // for LINQ extension methods
using static System.Console;

namespace LinqWithSets
{
    class Program
    {
        static void Output(IEnumerable<string> cohort, string description = "")
        {
            if(!string.IsNullOrEmpty(description))
            {
                WriteLine(description);
            }
            Write(" ");
            WriteLine(string.Join(", ", cohort.ToArray()));
        }
        static void Main(string[] args)
        {
            var cohort1 = new string[]
                {"Rachel", "Gareth", "Jonathan", "George"};
            var cohort2 = new string[]
                {"Jack", "Stephen", "Daniel", "Jack", "Jared"};
            var cohort3 = new string[]
                {"Declan", "Jack", "Jack", "Jasmine", "Conor"};
            
            Output(cohort1);
            Output(cohort2);
            Output(cohort3);
            WriteLine();
            // Disitnct() - delete duplicates
            Output(cohort2.Distinct(), "cohort2.Disitnct():");
            WriteLine();
            Output(cohort2.Union(cohort3), "cohort2.Union(cohort3):");
            WriteLine();
            Output(cohort2.Concat(cohort3), "cohort2.Concat(cohort3):");
            WriteLine();
            Output(cohort2.Intersect(cohort3), "cohort2.Intersect(cohort3):");
            WriteLine();
            Output(cohort2.Except(cohort3), "cohort2.Except(cohort3):");
            WriteLine();
            Output(cohort1.Zip(cohort2, (c1, c2) => $"{c1} matched with {c2}"), "cohort1.Zip(cohort2):");


            // Creating tuple in zip (matches object from cohort1 and cohort2 to tuple)
            foreach(var item in cohort1.Zip(cohort2, (c1, c2) => (c1, c2))){
                WriteLine("{0} : {1}", item.Item1, item.Item2);
            }

            List<string> CompileToList(string first, string second)
            {
                return new List<string> {first, second};
            }

            string[] CompileToArray(string first, string second)
            {
                return new string[] {first, second};
            }

            // Creating list of string in linq zip (matches object from cohort1 and cohort2 to array string) by CompileToList
            foreach(var item in cohort1.Zip(cohort2, CompileToList)){
                WriteLine("{0} : {1}", item[0], item[1]);
            }

            // Creating array of string in linq zip (matches object from cohort1 and cohort2 to array string) by CompileToArray
            foreach(var item in cohort1.Zip(cohort2, CompileToArray)){
                WriteLine("{0} : {1}", item[0], item[1]);
            }


        }
    }
}
