using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Program
    {

        static List<int> GetInput()
        {
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\input_1.txt");

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

        static void p_2a()
        {
            Console.WriteLine("AOC2020_2a");

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



        static void Main(string[] args)
        {
            //p_1a();
            p_2a();

            Console.ReadKey();
            return;

        }
    }
}
