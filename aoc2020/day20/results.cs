using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Arr2dManipulation
    {
        static public void FlipH(ref char [,] Arr)
        {
            int dx = Arr.GetLength(0);
            int dy = Arr.GetLength(1);

            var Arr2 = Arr;
            Arr = new char[dx,dy];
            for(int y =0 ; y < dy; y++)
            {
                for (int x = 0; x < dx; x++)
                {
                    Arr[x,y] = Arr2[dx-1-x,y];
                }
            }
        }
        
        public static void Rotate(ref char [,] Arr)
        {

            int dx = Arr.GetLength(0);
            int dy = Arr.GetLength(1);

            var Arr2 = Arr;
            Arr = new char[dy,dx];

               
            for(int y =0 ; y < dy; y++)
            {
                for (int x = 0; x < dx; x++)
                {
                    Arr[y,x] = Arr2[dx-1-x ,y];
                }
            }
        }     

        public static int GetHaspCount(char [,] Arr)
        {

            int dx = Arr.GetLength(0);
            int dy = Arr.GetLength(1);

            int hc = 0;
            for(int y =0 ; y < dy; y++)
            {
                for (int x = 0; x < dx; x++)
                {
                    if (Arr[x,y] == '#') hc++;
                }
            }
            return hc;
        }      

        public static void Print(char [,] Arr)
        {
            for(int y =0; y < Arr.GetLength(1); y++)
            {
                for(int x =0; x < Arr.GetLength(0); x++)
                {
                    Console.Write(Arr[x,y]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("----");

        }

    }

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

        public char [,] map = new char[10,10];

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
            Sides.Add(t);  Sides.Add(r);  Sides.Add(ReverseString(b)); Sides.Add(ReverseString(l));             
        }        

        public void Rotate() { Arr2dManipulation.Rotate(ref map); }

        public void FlipH() { Arr2dManipulation.FlipH(ref map); }

        public void MakeTopside(string side)
        {
            // proste nahodne rotuje a flipuji tak dlouho az to padne...
            for (int i =0; ; i++)
            {
                if (t == side) return;
                if ((i+1)%4 == 0) FlipH();
                else Rotate();
            }
        }

        public void MakeLeftside(string side)
        {
            // proste nahodne rotuje a flipuji tak dlouho az to padne...
            for (int i =0; ; i++)
            {
                if (l == side) 
                {
                    return;
                }
                if ((i+1)%4 == 0) FlipH();
                else Rotate();
            }
        }

        public bool ContainsSide(string side)
        {
            return Sides.Contains(side) || Sides.Contains(ReverseString(side));
        }

        public int GetPossibleNeighbours(TileList list)
        {
            int neighbours = 0;
            foreach(var tile in list)
            {
                if (TileNum == tile.TileNum) continue;
                foreach (var side in Sides)
                {
                    if (tile.ContainsSide(side)) neighbours++;
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
                    ret = tile;
                    break;
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
                if (neighbours == 2 ) corners.Add(tile);
            }
            return corners;
        }
        
        class Monster
        {
            char [,] pattern = null;

            int patternhashCount = 0;

            public void Load()
            {
                pattern = new char[20, 3];
                patternhashCount = 0;
                var lines = System.IO.File.ReadAllLines(@"./day20/monster.txt");
                for (int l = 0; l < 3; l++)
                {
                    for (int ch=0; ch < 20; ch++)
                    {
                        pattern[ch, l] = lines[l][ch];
                        if (pattern[ch,l] == '#') patternhashCount++;
                    }
                }

            }

            public void Rotate() { Arr2dManipulation.Rotate(ref pattern); }

            public void FlipH() { Arr2dManipulation.FlipH(ref pattern); }            

            int px { get { return pattern.GetLength(0); } }
            int py { get { return pattern.GetLength(1); } }

            bool IsMonster(int mx, int my, ref char [,] map)
            {
                for (int y = 0; y < py; y++)
                {
                    for (int x = 0; x < px; x++)
                    {
                        if (pattern[x,y] == '#')
                        {
                            if (map[mx+x, my + y] == '.') 
                                return false;
                        } 
                    }
                }
                return true;
            }

            void SetMonster(int mx, int my, ref char [,] map)
            {
                for (int y = 0; y < py; y++)
                {
                    for (int x = 0; x < px; x++)
                    {
                        if (pattern[x,y] == '#') 
                            map[mx+x, my + y] = 'O';
                    }
                }
            }

            
            public void TestMonsters(ref char [,] map)
            {
                int mx = map.GetLength(0);
                int my = map.GetLength(1);

                // pres vsechny body
                for (int y =0; y < my - py; y++)
                {
                    for (int x =0; x < mx - px; x++)
                    {
                        if (IsMonster(x,y, ref map))
                        {
                            SetMonster(x, y, ref map);
                        }
                    }
                }
            }
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
            
            // mapa spravne zorientocanych tilu
            Tile [,] tmap = new Tile[12,12];
            tmap[0,0] = TopLeft;

            int cnt = Convert.ToInt32(Math.Sqrt(list.Count));
            // vyplnim mapu 
            for (int j =0; j < cnt; j++)
            {
                for (int i =1; i < cnt; i++)
                {
                    // najdu tile pod nim a zrotuji tak aby byl side na vrsku...
                    Tile top = tmap[j, i-1];
                    tmap[j, i] = list.FindMatchingTileBySide(top, top.b);
                    tmap[j, i].MakeTopside(top.b);

                    // najdu tile mapravo a zrotuji tak aby byl side na vnalevo
                    Tile left = tmap[i-1, j];
                    tmap[i, j] = list.FindMatchingTileBySide(left, left.r);
                    tmap[i, j].MakeLeftside(left.r);
                }
            }

            // jedna mapa
            var map = new char[cnt*8, cnt * 8];
            for (int ty =0; ty < cnt; ty++)
            {
                for (int tx =0; tx < cnt; tx++)
                {
                    // najdu tile pod nim a zrotuji tak aby byl side na vrsku...
                    Tile t = tmap[tx, ty];

                    for (int y =1; y < 9; y++)
                    {
                        for (int x = 1; x < 9; x++)
                        {
                            map[tx * 8 + x -1, ty*8 + y -1] = t.map[x,y];
                        } 
                    }
                }
            }

            var monster = new Monster();
            monster.Load();

            for (int i =0; i < 8; i++)
            {
                monster.TestMonsters(ref map);
                if ((i+1)%4 == 0) Arr2dManipulation.FlipH(ref map);
                else Arr2dManipulation.Rotate(ref map);
            }

            Console.WriteLine("# : {0}", Arr2dManipulation.GetHaspCount(map));
            

        }        

    }

}
