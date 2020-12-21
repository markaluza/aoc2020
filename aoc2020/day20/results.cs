using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Tile
    {
        public int TileNum = -1;

        public List<int> Nmbs = new List<int>();
        public List<int> NmbsRev = new List<int>();

        public static string ReverseString( string s )
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }

        int GetSideNumber(string line)
        {
            line = line.Replace(".", "0");
            line = line.Replace("#", "1");

            return Convert.ToInt32(line, 2);
        }

        public void SetData(string tiledata)
        {
            var lines = tiledata.Split("\r\n");

            TileNum = int.Parse(lines[0].Split(" ")[1].TrimEnd(':'));

            string l = "";
            string r = "";
            string t= lines[1];
            string b = lines[lines.Length -1];

            for(int j =1 ; j < lines[j].Length; j++)
            {
                l += lines[j][0];
                r += lines[j][lines[j].Length -1];
            }

            Nmbs.Add(GetSideNumber(t));
            Nmbs.Add(GetSideNumber(r));
            Nmbs.Add(GetSideNumber(b));
            Nmbs.Add(GetSideNumber(l));

            NmbsRev.Add(GetSideNumber(ReverseString(t)));
            NmbsRev.Add(GetSideNumber(ReverseString(r)));
            NmbsRev.Add(GetSideNumber(ReverseString(b)));
            NmbsRev.Add(GetSideNumber(ReverseString(l)));     


        }

    }

    class TileList : List<Tile>
    {

        public Dictionary<int, List<Tile>> nmbs = new  Dictionary<int, List<Tile>>();
        
        public void Load()
        {

            var lines = System.IO.File.ReadAllText(@"./day20/input.txt");
            var tiles = lines.Split("\r\n\r\n");

            foreach(var tiledata in tiles)
            {
                Tile t = new Tile();
                t.SetData(tiledata);
                Add(t);

                foreach(var nmb in t.Nmbs)
                {
                    if (!nmbs.ContainsKey(nmb))
                    {
                        nmbs.Add(nmb, new List<Tile>());
                    }

                    nmbs[nmb].Add(t);
                }

                foreach(var nmb in t.NmbsRev)
                {
                    if (!nmbs.ContainsKey(nmb))
                    {
                        nmbs.Add(nmb, new List<Tile>());
                    }

                    nmbs[nmb].Add(t);
                }

            }

        }

        public TileList Clone()
        {
            var foo = new TileList();
            foreach(var tile in this)
            {
                Add(tile);
            }
            return foo;

        }

    }


    class Day20
    {
        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day20_1"); 


            TileList list = new TileList();
            list.Load();

            /*
            foreach(var nmbs in list.nmbs)
            {
                Console.Write("{0} - ", nmbs.Key);
                foreach(var nmb in nmbs.Value)
                {
                    Console.Write("{0}, ", nmb.TileNum);
                }
                if (nmbs.Value.Count == 1)
                {
                    unique++;
                }
                Console.WriteLine();
            }
            */


                        

        }


    }
}