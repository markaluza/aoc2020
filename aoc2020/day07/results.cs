using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day7
    {

        public static void Task1()
        {
            Console.WriteLine("AOC2020_Day7_Task1");

            var Rules = Day7_Common.GetRules();

            List<string> validclrs = new List<string> { "shiny gold" };

            for(int i =0; i < validclrs.Count; i++)
            {
                foreach(var rule in Rules)
                {
                    if (validclrs.Contains(rule.Key)) continue;

                    if (rule.Value.ContainsKey(validclrs[i]))
                        validclrs.Add(rule.Key);

                }
            }

            Console.WriteLine("Cnt :  {0}", validclrs.Count-1);

        }

        
        static int SumBags(Dictionary<string, Dictionary<string, int>> dict, string bagname)
        {
            int sum = 1;
            foreach(var bag in dict[bagname])
            {
                sum += bag.Value * SumBags(dict, bag.Key);
            }
            return sum;
            
        }

        public static void Task2()
        {
            Console.WriteLine("AOC2020_Day7_Task2");
  
            var Rules = Day7_Common.GetRules();

            int sum = SumBags(Rules, "shiny gold" ) - 1;

            Console.WriteLine("Cnt :  {0}", sum);   

        }        


    }
}