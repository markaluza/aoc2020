using System;

namespace Aoc2020
{
    class Day1
    {
        public static void Task1()
        {
            Console.WriteLine("AOC2020_Day1_Task1");

            var numbers = Day1_Common.GetInput();

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

        public static void Task2()
        {
            Console.WriteLine("AOC2020_Day1_Task2");

            var numbers = Day1_Common.GetInput();

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



    }
}