using System.Collections.Generic;

namespace Aoc2020
{

    class Day1_Common
    {

        public static List<int> GetInput()
        {
            string line;

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\input_1.i");

            List<int> numbers = new List<int>();

            while ((line = file.ReadLine()) != null)
            {
                int number = int.Parse(line);
                numbers.Add(number);

            }
            return numbers;
        }

    }
}
