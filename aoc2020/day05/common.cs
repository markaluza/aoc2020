using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day5_Common
    {
        public static List<int> GetSeatIds()
        {
            var inputfile =  new List<string>(System.IO.File.ReadAllLines(@"..\input_5.i"));

            List<int> ids = new List<int>();

            foreach (var line in inputfile)
            {

                string line2 = line;
                
                line2 = line2.Replace('F', '0');
                line2 = line2.Replace('B', '1');
                line2 = line2.Replace('L', '0');
                line2 = line2.Replace('R', '1');

                ids.Add(Convert.ToInt32(line2, 2));

            }

            return ids;
        }        
    }
}