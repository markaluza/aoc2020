using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day22
    {
        

        static class CardReader
        {

            static public void ReadCards(out List<int> Player1, out List<int> Player2)
            {
                Player1 = new List<int>();
                Player2 = new List<int>();

                var input = System.IO.File.ReadAllLines(@"./day22/input.txt");

                for(int i = 1; i <input.Length; i++)
                {
                    if (i > 0 && i <= 25) Player1.Add(int.Parse(input[i]));
                    if (i >=28) Player2.Add(int.Parse(input[i]));

                }

            }
        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day22_1"); 

            List<int> Player1 = null;
            List<int> Player2 = null;

            CardReader.ReadCards(out Player1, out Player2);

            while(Player1.Count > 0 && Player2.Count > 0)
            {

                var p1c = Player1[0]; Player1.RemoveAt(0);
                var p2c = Player2[0]; Player2.RemoveAt(0);

                if (p1c > p2c) 
                {
                    Player1.Add(p1c); Player1.Add(p2c);
                }
                else if (p2c > p1c)
                {
                    Player2.Add(p2c); Player2.Add(p1c);
                }
                else
                {
                    throw new Exception();
                }


            }

            List<int> winner = Player1.Count > 0 ? Player1 : Player2;

            long score = 0;
            for (int i =0; i < winner.Count; i++)
            {
                score += winner[i] * (winner.Count - i);
            }

             Console.WriteLine("Score = {0}", score);
            
        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day22_1"); 
            
        }

    }
}
