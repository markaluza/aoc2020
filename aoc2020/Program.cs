using System;
using System.Collections.Generic;
using System.Diagnostics;
namespace Aoc2020
{

    class Program
    {

        static List<int> GetInput()
        {
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\input_1.i");

            List<int> numbers = new List<int>();

            while ((line = file.ReadLine()) != null)
            {
                //System.Console.WriteLine(line);
                int number = int.Parse(line);
                numbers.Add(number);

            }
            return numbers;
        }

        static void p_1a()
        {
            Console.WriteLine("AOC2020_1a");

            var numbers = GetInput();

            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    if (numbers[i] + numbers[j] == 2020)
                    {
                        Console.WriteLine("Result : {0}, {1} -> {2}", numbers[i], numbers[j], numbers[i] * numbers[j]);
                        return;
                    }
                }
            }
            Console.WriteLine("Values were not found !!!!!");
        }

        static void p_1b()
        {
            Console.WriteLine("AOC2020_1b");

            var numbers = GetInput();

            for (int i = 0; i < numbers.Count; i++)
            {
                for (int j = i + 1; j < numbers.Count; j++)
                {
                    for (int k = j+1; k < numbers.Count; k++)
                    {
                        if (numbers[i] + numbers[j] + numbers[k] == 2020)
                        {
                            Console.WriteLine("Result : {0}, {1}, {2} -> {3}", numbers[i], numbers[j], numbers[k],  numbers[i] * numbers[j] * numbers[k]);
                            return;
                        }
                    }
                }
            }

            Console.WriteLine("Values were not found !!!!!");

        }

        struct Policy
        {
            public int a;
            public int b;

            public char letter;

            public string password;
        }
        static List<Policy> GetInput_2()
        {
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =  new System.IO.StreamReader(@"..\input_2.i");

            List<Policy> policies = new List<Policy>();

            while ((line = file.ReadLine()) != null)
            {
                
                var parts = line.Split(':', ' ', '-'); // 9-12 q: qqqxhnhdmqqqqjz

                if (parts.Length != 5)
                {
                    Debug.Assert(false);
                    continue;
                }

                Policy p = new Policy();
                p.a = Convert.ToInt32(parts[0]);
                p.b = Convert.ToInt32(parts[1]);
                p.letter =  Convert.ToChar(parts[2]);
                p.password = parts[4];

                policies.Add(p);

            }
            return policies;
        }



        static bool EvalPassword1(Policy policy)
        {
            int cntr = 0;
            
            foreach (char ch in policy.password)
            {
                if (ch == policy.letter)
                cntr++;
            }

            return cntr >= policy.a && cntr <= policy.b;
        }


       static void p_2a()
        {
            Console.WriteLine("AOC2020_2a");

            var policies = GetInput_2();

            int valid = 0;

            foreach (Policy p in policies)
            {
                if (EvalPassword1(p))
                    valid++;
            }

            Console.WriteLine("Valid passwords : {0}", valid);

        }

        static bool EvalPassword2(Policy policy)
        {
            int cntr = 0;
            
            if (policy.password[policy.a-1] == policy.letter) cntr++;
            if (policy.password[policy.b-1] == policy.letter) cntr++;

            return cntr == 1;
        }
        
       static void p_2b()
        {
            Console.WriteLine("AOC2020_2b");

            var policies = GetInput_2();

            int valid = 0;

            foreach (Policy p in policies)
            {
                if (EvalPassword2(p))
                    valid++;
            }

            Console.WriteLine("Valid passwords : {0}", valid);

        }


        static void Main(string[] args)
        {
            //p_1a();
            //p_1b();

            //p_2a();
            p_2b();

            Console.ReadKey();
            return;

        }
    }
}
