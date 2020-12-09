using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day9_Common
    {

        public static List<Int64> GetInput()
        {
            string line;

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"./day09/input.txt");

            var numbers = new List<Int64>();

            while ((line = file.ReadLine()) != null)
            {
                Int64 number = Int64.Parse(line);
                numbers.Add(number);

            }
            return numbers;
        }

        public static bool ValidNmb(List<Int64> preamble, Int64 val)
        {
            for (int j = 0; j < preamble.Count-1; j++)
            {   
                for (int k = j+1;  k < preamble.Count; k++)
                {
                    if (preamble[j] + preamble[k] == val)
                    {
                        return true;
                    }
                }
            }    
            return false;       
        }

    }
}