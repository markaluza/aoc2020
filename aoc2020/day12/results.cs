using System;
using System.Collections.Generic;


namespace Aoc2020
{

    class Pos
    {
        public int x = 0;
        public int y = 0;

        public Pos(int _x, int _y) { x = _x; y = _y; }
        public Pos() {}

    }  

    class Day12
    {

        const int dir_E = 0;
        const int dir_S = 1;
        const int dir_W = 2;
        const int dir_N = 3;      

        public static void Task1()
        {
            
            Console.WriteLine("AOC2020_Day12_Task1");

            var lines = new List<string>(System.IO.File.ReadAllLines(@"./day12/input.txt"));

            var pos = new Pos();

            int dir = dir_E;
            
            foreach(string line in lines)
            {
                
                var cdir = line[0]; 
                int val = int.Parse(line.Substring(1));

                switch(cdir)
                {
                    case 'E': pos.x += val; break;
                    case 'S': pos.y += val; break;
                    case 'W': pos.x -= val; break;
                    case 'N': pos.y -= val; break;

                    case 'R': dir = (dir + val/90)%4; break;
                    case 'L': dir = (4 + dir - val/90)%4; break;

                    case 'F': 
                        switch(dir)
                        {
                            case dir_E: pos.x += val; break;
                            case dir_S: pos.y += val; break;
                            case dir_W: pos.x -= val; break;
                            case dir_N: pos.y -= val; break;
                        }
                        break;
                    
                }

            }

            Console.WriteLine("Manhatan Distance {0}", pos.x + pos.y);
            
        }     

        public static void Task2()
        {
            
            Console.WriteLine("AOC2020_Day12_Task2");

            var lines = new List<string>(System.IO.File.ReadAllLines(@"./day12/input.txt"));

            var ship = new Pos(0, 0);
            var waypoint = new Pos(10, -1);

            foreach(string line in lines)
            {
                
                var instr = line[0]; 
                int val = int.Parse(line.Substring(1));

                switch(instr)
                {
                    case 'E': waypoint.x += val; break;
                    case 'S': waypoint.y += val; break;
                    case 'W': waypoint.x -= val; break;
                    case 'N': waypoint.y -= val; break;

                    case 'R': 
                        for (int i =0; i < val/90; i++)
                        {
                            (waypoint.x, waypoint.y) = (-waypoint.y, waypoint.x);
                        }
                        break;
                    
                    case 'L':
                        for (int i =0; i < val/90; i++)
                        {
                            (waypoint.x, waypoint.y) = (waypoint.y, -waypoint.x);
                        }
                        break;

                    case 'F': 
                        ship.x += val * waypoint.x; 
                        ship.y += val * waypoint.y;
                        break;
                    
                }

            }

            (ship.x, ship.y) =  (Math.Abs(ship.x), Math.Abs(ship.y));


            Console.WriteLine("Manhatan Distance {0}", ship.x + ship.y);
            
        }

    }

}