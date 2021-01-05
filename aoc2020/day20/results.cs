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

        char [,] map = new char[10,10];

        public string l { get { string foo = ""; for (int i =0; i < 10; i++) foo += map[0,i]; return foo; } }
        public string r { get  { string foo = ""; for (int i =0; i < 10; i++) foo += map[9,i]; return foo; } }        
        public string t{ get { string foo = ""; for (int i =0; i < 10; i++) foo += map[i,0];return foo; } } 
        public string b { get  { string foo = ""; for (int i =0; i < 10; i++) foo += map[i,9]; return foo;  } } 

        public void SetData(string tiledata)
        {
            var lines = tiledata.Split("\r\n");

            TileNum = int.Parse(lines[0].Split(" ")[1].TrimEnd(':'));


            for(int y =1 ; y <= 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[x,y-1] = lines[y][x];
                }

            }

            Sides.Add(t);
            Sides.Add(r);
            Sides.Add(ReverseString(b));
            Sides.Add(ReverseString(l));             

        }        

        public void Rotate()
        {
            var map2 = map;
            map = new char[10,10];
            for(int y =0 ; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[x,y] = map2[y,9 - x];
                }
            }
        }

        public void FlipH()
        {
            var map2 = map;
            map = new char[10,10];
            for(int y =0 ; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[x,y] = map2[9-x,y];
                }
            }
        }
        public void FlipV()
        {
            var map2 = map;
            map = new char[10,10];
            for(int y =0 ; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    map[x,y] = map2[x,9-y];
                }
            }
        }



        public void MakeTopside(string side)
        {
            // proste nahodne rotuje a flipuji tak dlouho at zo padne...
            for (int i =0; ; i++)
            {
                if ((i+1)%3 == 0) FlipH();
                if ((i+1)%5 == 0) FlipV();

                if (t != side) Rotate();
                else
                {
                    return;
                }
            }
        }

        public void MakeLeftside(string side)
        {
            // proste nahodne rotuje a flipuji tak dlouho at zo padne...
            for (int i =0; ; i++)
            {
                if ((i+1)%3 == 0) FlipH();
                if ((i+1)%5 == 0) FlipV();

                if (l != side) Rotate();
                else
                {
                    return;
                }
            }
        }



        public bool ContainsSide(string side)
        {
            return Sides.Contains(side) || Sides.Contains(ReverseString(side));
        }

        public Tile GetPossibleNeighbour_onside(TileList list, string side)
        {
            foreach(var tile in list)
            {
                if (TileNum == tile.TileNum) continue;
                if (tile.ContainsSide(side)) { return tile; }
                
            }
            return null;
        }        

        public int GetPossibleNeighbours(TileList list)
        {

            int neighbours = 0;

            foreach(var tile in list)
            {
                if (TileNum == tile.TileNum) continue;

                foreach (var side in Sides)
                {
                    if (tile.ContainsSide(side)) 
                    {
                        neighbours++;
                    }
                }
                
            }

            return neighbours;
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

        public Tile FindMatchingTileBySide(Tile Orig, string side)
        {
            Tile ret = null;
            foreach(var tile in this)
            {
                if (tile.TileNum != Orig.TileNum && tile.ContainsSide(side))
                {
                    if (ret != null)
                    {
                        // pozn test jestli nejsou unikatni ???
                        System.Diagnostics.Debug.Assert(false);
                    }
                    ret = tile;
                }
            }

            return ret;
        }

       public Tile GetTileByNum(int TileNum)
        {
            foreach(var tile in this)
                if (tile.TileNum == TileNum)
                    return tile;

            return null;
        } 

    }


    class Day20
    {

        private static List<Tile> GetCorners(TileList list)
        {
            List<Tile> corners = new List<Tile>();

            foreach(var tile in list)
            {
                int neighbours = tile.GetPossibleNeighbours(list);
                if (neighbours == 2 )
                {
                    corners.Add(tile);
                }
            }

            return corners;

        }
        

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day20_1"); 


            TileList list = new TileList();
            list.Load();

            long mult = 1;
            foreach(var tile in GetCorners(list))
            {
                int neighbours = tile.GetPossibleNeighbours(list);
                Console.WriteLine("tile : {0}", tile.TileNum); 
                mult *= tile.TileNum;
            }
                        
            Console.WriteLine("Mult : {0}", mult);
        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day20_2"); 
                        
            TileList list = new TileList();
            list.Load();

            // proste vemu prvni roh
            var TopLeft = GetCorners(list)[0];

            // zrotuji jej tak aby mel vlevo a vpravo volno...
            while (
                list.FindMatchingTileBySide(TopLeft, TopLeft.t) != null ||
                list.FindMatchingTileBySide(TopLeft, TopLeft.l) != null
            ) TopLeft.Rotate();
            

            Tile [,] map = new Tile[12,12];
            map[0,0] = TopLeft;

            // vyplnim mapu 
            for (int j =0; j < 12; j++)
            {
                for (int i =1; i < 12; i++)
                {
                    // najdu tile pod nim a zrotuji tak aby byl side na vrsku...
                    Tile top = map[j, i-1];
                    map[j, i] = list.FindMatchingTileBySide(top, top.b);
                    map[j, i].MakeTopside(top.b);

                    // najdu tile mapravo a zrotuji tak aby byl side na vnalevo
                    Tile left = map[i-1, j];
                    map[i, j] = list.FindMatchingTileBySide(left, left.r);
                    map[i, j].MakeLeftside(left.r);
                }
            }

            Console.WriteLine("TODO...");

        }        

    }

}
