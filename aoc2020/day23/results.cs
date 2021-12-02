using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    class Day23
    {
        
        //private static List<byte> def_input = new List<byte>(){ 3, 8, 9, 1, 2, 5, 4, 6, 7 };
        private static List<byte> def_input = new List<byte>(){ 9, 5, 2, 3, 1, 6, 4, 8, 7 };
        

        private static void rotate(List<byte> input, byte rqno)
        {
            while(true)
            {
                if (input[0]==rqno) return;
                var first = input[0]; input.RemoveAt(0);
                input.Add(first);
            }
        }

        private static int getdestPos(List<byte> input, byte rqval)
        {
            if (rqval == 0) { return input.FindIndex( a => a == input.Max()); }
            int dest = input.FindIndex( a => a == rqval);
            if (dest < 0) return getdestPos(input, (byte) (rqval -1) );
            return dest;
        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day23_1"); 

            var input = new List<byte>(def_input);
            byte currentno = input[0];

            for (int move = 0; move < 100; move++)
            {


                rotate(input, currentno);
                Console.WriteLine("Move {0} - {1}", move +1, String.Join(", ", input) );
                
                var removed = input.GetRange(1, 3);
                input.RemoveRange(1, 3);

                int destpos = getdestPos(input, (byte) (currentno -1));
                input.InsertRange(destpos +1, removed);
                    
                currentno = input[1];
                
            }

            rotate(input, 1);
            Console.WriteLine("Result {0}", String.Join("", input) );

            
        }


        public static void Task2()
        {

            
            Console.WriteLine("AOC2020_Day23_2"); 


        }

    }
}
