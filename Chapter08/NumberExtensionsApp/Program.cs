using System;
using System.Numerics;
using static System.Console;

namespace Exercise03.Shared
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 180000000;
            BigInteger t = BigInteger.Parse("18456002032011000007");
            WriteLine(number.ToWords());
            WriteLine(t.ToWords());
        }
    }
}
