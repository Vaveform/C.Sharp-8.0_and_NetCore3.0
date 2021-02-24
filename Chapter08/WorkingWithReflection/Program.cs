using System;
using System.Reflection;
using System.Collections;
using static System.Console;
using System.Linq; // to use OrderByDescend

namespace WorkingWithReflection
{
    class Program
    {
        [Coder("Mark Price", "22 August 2019")]
        [Coder("Johni Rasmussen", "13 September 2019")]
        public static void DoStuff(){

        }
        static void Main(string[] args)
        {
            WriteLine("Assembly metadata:");
            Assembly assembly = Assembly.GetEntryAssembly();
            WriteLine($"    Full name: {assembly.FullName}");
            WriteLine($"    Full name: {assembly.Location}");

            var attributes = assembly.GetCustomAttributes();
            WriteLine($"    Attributes:");
            foreach(Attribute a in attributes)
            {
                WriteLine($"    {a.GetType()}");
            }

            var version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            WriteLine($"    Version: {version.InformationalVersion}");

            var company = assembly.GetCustomAttribute<AssemblyCompanyAttribute>();

            WriteLine($"    Company: {company.Company}");

            WriteLine();
            WriteLine($"* Types:");
            Type[] types = assembly.GetTypes();
            foreach(Type type in types)
            {
                WriteLine();
                WriteLine($"Type: {type.FullName}");
                MemberInfo[] members = type.GetMembers();
                foreach(MemberInfo member in members)
                {
                    var coders = member.GetCustomAttributes<CoderAttribute>().OrderByDescending(c => c.LastModified);
                    if(coders.Count() == 0){
                        WriteLine("{0}: {1} ({2})",
                            arg0: member.MemberType,
                            arg1: member.Name,
                            arg2: member.DeclaringType.Name);
                        
                        // // If we find CoderAttribute decorated types 
                        // foreach(CoderAttribute coder in coders){
                        //     WriteLine("-> Modified by {0} on {1}", coder.Coder, coder.LastModified.ToShortDateString());
                        // }
                    }
                }
            }
        }
    }
}
