using System;
using System.Text.RegularExpressions;
using static System.Console;


namespace Exercise02
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex reg_expr_instance = null;
            WriteLine("The default regular expression checks for at least one digit.");
            ConsoleKeyInfo keyinfo = new ConsoleKeyInfo('a', ConsoleKey.Enter, false, false, false);
            while(keyinfo.Key != ConsoleKey.Escape){
                Write("Enter a regular expression (or press ENTER to use the default):");
                string expression = ReadLine();
                if(expression == ""){
                    reg_expr_instance = new Regex(@"\d");
                    expression = @"\d";
                }
                else{
                    reg_expr_instance = new Regex(expression);
                }
                Write("Enter some input: ");
                string input = ReadLine();
                WriteLine("{0} matches {1}? {2}", 
                    arg0: input, arg1: expression, arg2: reg_expr_instance.IsMatch(input)
                );
                WriteLine("Press ESC to end or any key to try again.");
                keyinfo = ReadKey(true);
            }
        }
    }
}
