using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Tile
    {
        public int TileNum = -1;

        public List<string> Sides = new List<string>();

        public static string ReverseString( string s )
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }

        public void SetData(string tiledata)
        {
            var lines = tiledata.Split("\r\n");

            TileNum = int.Parse(lines[0].Split(" ")[1].TrimEnd(':'));

            string l = "";
            string r = "";
            string t = lines[1];
            string b = lines[lines.Length -1];

            for(int j =1 ; j < lines.Length; j++)
            {
                l += lines[j][0];
                r += lines[j][lines[j].Length -1];
            }

            Sides.Add(t);
            Sides.Add(r);
            Sides.Add(b);
            Sides.Add(l);             

        }

        public bool ContainsSide(string side)
        {
            return Sides.Contains(side) || Sides.Contains(ReverseString(side));
        }

        public int GetPossibleNeighbours(TileList list, out TileList output)
        {

            output = new TileList();

            foreach(var tile in list)
            {
                if (TileNum == tile.TileNum) continue;

                foreach (var side in Sides)
                {
                    if (tile.ContainsSide(side)) 
                    {
                        if (!output.Contains(tile))
                            output.Add(tile);
                    }
                }
                
            }
            return output.Count;
        }

    }

    class TileList : List<Tile>
    {
        
        public void Load()
        {

            var lines = System.IO.File.ReadAllText(@"./day20/input.txt");
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

        Tile FindTileBySide(string side)
        {
            foreach(var tile in this)
                if (tile.ContainsSide(side))
                    return tile;

            return null;
        }

        Tile GetTileByNum(int TileNum)
        {
            foreach(var tile in this)
                if (tile.TileNum == TileNum)
                    return tile;

            return null;
        } 

    }


    class Day20
    {
        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day20_1"); 


            TileList list = new TileList();
            list.Load();

            long mult = 1;
            foreach(var tile in list)
            {
                TileList output = null;
                int neighbours = tile.GetPossibleNeighbours(list, out output);
                if (neighbours == 2 )
                {
                    Console.Write("tile : {0} - {1} - ", tile.TileNum, neighbours); 
                    
                    foreach(var neighbour in output)
                    {
                        TileList output2= null;
                        int cnt = neighbour.GetPossibleNeighbours(list,out output2);
                        Console.Write("{0}({1}), ", neighbour.TileNum, cnt); 
                    }

                    mult *= tile.TileNum;

                    Console.WriteLine("");
                }
            }
                        
            Console.WriteLine("Mult : {0}", mult);
        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day20_2"); 

           // TileList list = new TileList();
            //list.Load();
                        
            Console.WriteLine("TODO...");

        }        

    }

}