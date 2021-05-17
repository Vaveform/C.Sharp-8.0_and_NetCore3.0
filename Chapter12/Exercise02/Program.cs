using System;
using Northwind.Types;
using System.Linq;
using static System.Console;

namespace Exercise02
{
    class Program
    {
        static void PrintUniqueCustomerCities(){
            using(var db = new DbNorthwind()){
                var queryCities = db.Customers
                    .OrderBy(customer => customer.City)
                    .Select(customer => customer.City)
                    .Distinct().ToArray();
                int NumberOfCities = queryCities.Count();
                WriteLine("Available {0} cities:", NumberOfCities);
                for(int i = 0; i < NumberOfCities; i++){
                    if(i != NumberOfCities - 1){
                        Write("{0}, ", queryCities[i]);
                    }
                    else{
                        WriteLine("{0} ", queryCities[i]);
                    }
                }
            }
        }

        static void PrintCustomersCompanyByCity(string city){
            using(var db = new DbNorthwind()){
                var queryCustomerName = from customer in db.Customers 
                    where customer.City == city 
                    orderby customer.City 
                    select customer.CompanyName;
                queryCustomerName.Distinct();
                WriteLine("There are {0} customers in {1}:", queryCustomerName.Count(), city);
                foreach(var company in queryCustomerName){
                    WriteLine(company);
                }
            }
        }
        static void Main(string[] args)
        {
            PrintUniqueCustomerCities();
            Write("Enter the name of a city: ");
            string preferredCity = ReadLine();
            PrintCustomersCompanyByCity(preferredCity);
        }
    }
}
