using System;

namespace Aoc2020
{
    class Day2
    {
        static bool EvalPassword1(Day2_Common.Policy policy)
        {
            int cntr = 0;
            
            foreach (char ch in policy.password)
            {
                if (ch == policy.letter)
                cntr++;
            }

            return cntr >= policy.a && cntr <= policy.b;
        }


       public static void Task1()
        {
            Console.WriteLine("AOC2020_Day2_Task1");

            var policies = Day2_Common.GetInput();

            int valid = 0;

            foreach (var p in policies)
            {
                if (EvalPassword1(p))
                    valid++;
            }

            Console.WriteLine("Valid passwords : {0}", valid);

        }

        static bool EvalPassword2(Day2_Common.Policy policy)
        {
            int cntr = 0;
            
            if (policy.password[policy.a-1] == policy.letter) cntr++;
            if (policy.password[policy.b-1] == policy.letter) cntr++;

            return cntr == 1;
        }
        
       public static void Task2()
        {
            Console.WriteLine("AOC2020_Day2_Task2");

            var policies = Day2_Common.GetInput();

            int valid = 0;

            foreach (var p in policies)
            {
                if (EvalPassword2(p))
                    valid++;
            }

            Console.WriteLine("Valid passwords : {0}", valid);

        }

    }
}