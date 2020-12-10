using System.Collections.Generic;

namespace Aoc2020
{

    class Day1_Common
    {

        public static List<int> GetIntInput(string sfile)
        {
            string line;

            System.IO.StreamReader file =
                new System.IO.StreamReader(sfile);

            List<int> numbers = new List<int>();

            while ((line = file.ReadLine()) != null)
            {
                int number = int.Parse(line);
                numbers.Add(number);

            }
            return numbers;
        }

        public static List<int> GetInput()
        {
            return GetIntInput(@"./day01/input.txt");
        }

    }
}
