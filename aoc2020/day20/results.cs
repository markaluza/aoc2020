using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Tile
    {
        public int TileNum = -1;

        public List<int> Nmbs1 = new List<int>();
        public List<int> Nmbs2 = new List<int>();

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

            for(int j =1 ; j < lines.Length; j++)
            {
                l += lines[j][0];
                r += lines[j][lines[j].Length -1];
            }

            Nmbs1.Add(GetSideNumber(t));
            Nmbs1.Add(GetSideNumber(r));
            Nmbs1.Add(GetSideNumber(b));
            Nmbs1.Add(GetSideNumber(l));

            Nmbs2.Add(GetSideNumber(ReverseString( t)));
            Nmbs2.Add(GetSideNumber(ReverseString( r)));
            Nmbs2.Add(GetSideNumber(ReverseString( b)));
            Nmbs2.Add(GetSideNumber(ReverseString( l)));     


        }

    }

    class TileList : List<Tile>
    {
        
        public void Load()
        {

            var lines = System.IO.File.ReadAllText(@"./day20/input_test.txt");
            var tiles = lines.Split("\r\n\r\n");

            foreach(var tiledata in tiles)
            {
                Tile t = new Tile();
                t.SetData(tiledata);
                Add(t);
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

        /*List<Tile> GetAvailableTilesWithNmb()
        {
            
            List<Tile>

        }
        */

    }



    class Day20
    {
        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day20_1"); 


            TileList list = new TileList();
            list.Load();

                        

        }


    }
}