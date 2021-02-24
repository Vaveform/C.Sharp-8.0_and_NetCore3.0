using System;
using System.Numerics;
using System.Linq;

namespace Exercise03.Shared
{
    public static class BigIntegerAndIntegerExtensions
    {
        private static string[] to_twenty = {"zero", "one", "two", "three", "four", "five", "six", "seven",
            "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen",
            "eighteen", "nineteen", "twenty"};
        private static string[] to_ninety = {"zero", "ten", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "ninety"};
        private static string[] ten_degrees = {"zero" , "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion"};
        public static string ToWords(this BigInteger big_number){
            int ten_degree = 0;
            string result = new string("");
            while(big_number != 0){
                string tmp = new string("");
                int three_digits = (int)(big_number % 1000);
                int tens = three_digits % 100;
                
                if(tens <= 20 && tens > 0){
                    tmp = to_twenty[tens] + " " + tmp;
                }
                else if(tens > 20){
                    tmp = to_ninety[tens / 10] + " " + to_twenty[tens % 10] + " " + tmp;
                }
                int hundreds = three_digits / 100;
                if(hundreds >= 1){
                    if(tens == 0){
                        tmp = to_twenty[hundreds] + " " + "hundred " + tmp;
                    }
                    else{
                        tmp = to_twenty[hundreds] + " " + "hundred and " + tmp;
                    }
                }
                if(ten_degree != 0 && three_digits >= 1){
                    result = tmp + ten_degrees[ten_degree] + ", " + result;
                }
                else{
                    result = tmp + result;
                }

                big_number /= 1000;
                ten_degree++;
            }
            return result.Trim(new char[]{',', ' ', '\n', '.'});
        }

        public static string ToWords(this int number){
            // int devisible = 1000;
            BigInteger converted = new BigInteger(number);
            return ToWords(converted).Trim(new char[]{',', ' ', '\n', '.'});
        }
    }
}
