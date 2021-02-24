using System;
using System.Collections.Generic;


namespace Packt.Shared{
    public class ThingOfDefaults{
        public int Population;
        public DateTime When;
        public string Name;
        public List<Person> People;
        public ThingOfDefaults(){
            // Old default initialization way
            Population = default(int);
            When = default(DateTime);
            Name = default(string);
            People = default(List<Person>);
            // From C# 7.1
            Population = default;
            When = default;
            Name = default;
            People = default;
        }
    }
}