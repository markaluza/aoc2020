using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day4
    {


       public static void Task1()
        {
            Console.WriteLine("AOC2020_Day4_Task1");

            var Passports = Day4_Common.GetPassports();
            
            int valid = 0;
            foreach(var pass in Passports)
            {
                if (pass.IsValid())
                    valid++;
            }
  
            Console.WriteLine("Valid passports {0} out of {1}", valid, Passports.Count);

        }

       public static void Task2()
        {
            Console.WriteLine("AOC2020_Day4_Task2");


            var Passports = Day4_Common.GetPassports();
            
            int valid = 0; 
            foreach(var pass in Passports)
            {
                if (pass.IsValid2())
                    valid++;
            }
  
            Console.WriteLine("Valid passports {0}", valid);


        }        


    }
}