using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{

    class Day1_Common
    {

        public static List<int> GetIntInput(string sfile)
        {
            return System.IO.File.ReadAllLines(sfile).Select(int.Parse).ToList();
        }

        public static List<int> GetInput()
        {
            return GetIntInput(@"./day01/input.txt");
        }

    }
}
