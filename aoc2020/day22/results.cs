using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day22
    {
        

        static class CardReader
        {

            static public void ReadCards(out List<byte> Player1, out List<byte> Player2)
            {
                Player1 = new List<byte>();
                Player2 = new List<byte>();

                //var input = System.IO.File.ReadAllLines(@"./day22/input_test.txt");
                var input = System.IO.File.ReadAllLines(@"./day22/input.txt");

                bool p2 = false;

                for(int i = 1; i <input.Length; i++)
                {
                    if (input[i] == "") { p2 = true; i++; continue; }
                    if (!p2) Player1.Add(byte.Parse(input[i]));
                    else Player2.Add(byte.Parse(input[i]));

                }

            }
        }

        private static long Score(List<byte> cards)
        {
            long score = 0;
            for (int i =0; i < cards.Count; i++)
            {
                score += cards[i] * (cards.Count - i);
            }
            return score;
        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day22_1"); 

            List<byte> Player1 = null;
            List<byte> Player2 = null;

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

            List<byte> winner = Player1.Count > 0 ? Player1 : Player2;
            Console.WriteLine("Score = {0}", Score(winner));
            
        }

        private static int gameno = 0;


        private static int RecursiveGame(List<byte> Player1, List<byte> Player2)
        {


            List<(long, long)> Rounds = new List<(long, long)>();
            while(Player1.Count > 0 && Player2.Count > 0)
            {

                gameno++;
               // Console.WriteLine("Game {0}: ", gameno );
                //Console.WriteLine("P1: {0}", String.Join(", ", Player1) );
               // Console.WriteLine("P2: {0}", String.Join(", ", Player2) );

                var p1h = Score(Player1);
                var p2h = Score(Player2);

                if (Rounds.Contains((p1h, p2h)))
                {
                    //Console.WriteLine("Game {0} - Same hash", gameno );
                    return 1;
                }

                Rounds.Add((p1h, p2h));

                var p1c = Player1[0]; Player1.RemoveAt(0);
                var p2c = Player2[0]; Player2.RemoveAt(0);

                if (p1c > Player1.Count || p2c > Player2.Count)
                {
                    // regular
                    if (p1c > p2c) 
                    {
                        Player1.Add(p1c); Player1.Add(p2c);
                        //Console.WriteLine("Game {0} - Regular P1 wins", gameno);
                    }
                    else if (p2c > p1c)
                    {
                        Player2.Add(p2c); Player2.Add(p1c);
                        //Console.WriteLine("Game {0} - Regular P2 wins", gameno);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    // recursion
                    var Player1copy = Player1.GetRange(0, p1c);
                    var Player2copy = Player2.GetRange(0, p2c);

                    if (RecursiveGame(Player1copy, Player2copy) == 1)
                    {
                         Player1.Add(p1c); Player1.Add(p2c);
                         //Console.WriteLine("Game {0} - Recursive P1 wins", gameno);
                    }
                    else
                    {
                        Player2.Add(p2c); Player2.Add(p1c);
                        //Console.WriteLine("Game {0} - Recursive P2 wins", gameno);
                    }

                }
            }

            if (Player1.Count > 0)
                return 1;
            else 
                return 2;

        }

        public static void Task2()
        {

            
            Console.WriteLine("AOC2020_Day22_2"); 

            List<byte> Player1 = null;
            List<byte> Player2 = null;

            CardReader.ReadCards(out Player1, out Player2);

            List<byte> Player1orig = new List<byte>(Player1);
            List<byte> Player2orig = new List<byte>(Player2);

            var winner = RecursiveGame(Player1, Player2);
            Console.WriteLine("Game {0} - Winner = {1}", gameno, winner );

            if (winner == 1)
                Console.WriteLine("Score : {0}", Score(Player1));
            else 
                Console.WriteLine("Score : {0}", Score(Player2));

        }

    }
}
