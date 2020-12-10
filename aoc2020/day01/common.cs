using System.Collections.Generic;

namespace Aoc2020
{

    class Day1_Common
    {

        public static List<int> GetIntInput(string sfile)
        {

            List<int> numbers = new List<int>();    
            var lines = System.IO.File.ReadAllLines(sfile);

            foreach(var line in lines) numbers.Add(int.Parse(line));

            return numbers;
        }

        public static List<int> GetInput()
        {
            return GetIntInput(@"./day01/input.txt");
        }

    }
}
