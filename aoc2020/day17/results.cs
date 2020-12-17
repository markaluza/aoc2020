using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Day17
    {

        class Cube
        {
        
            int dim = 0;
            bool [,,,] active = null;

            public void Load(int iters)
            {
               //var lines = System.IO.File.ReadAllLines(@"./day17/input_test.txt");
                var lines = System.IO.File.ReadAllLines(@"./day17/input.txt");

                dim = lines[0].Length + (iters+1) * 2;
                if (dim%2 == 0) dim++;

                active = new bool[dim, dim, dim, dim];
                
                int middle = dim/2;
                int m2 = dim/3;
                
                for (int y =0; y < lines.Length; y++)
                {
                    for (int x = 0; x < lines[y].Length; x++)
                    {
                        if (lines[y][x] == '#')
                        {
                            active[ x+1 + m2, y+1 + m2, middle, middle] = true;
                        }
                    }
                }

            }

            public void printZ(int z)
            {
                
                int z2 = dim/2 + z;

                Console.WriteLine("Z: {0}, Zidx: {1}", z, z2);  

                for (int y = 0; y < dim; y++)
                {
                    for (int x = 0; x < dim; x++)
                    {
                        if (active[x,y,z2, 0])
                            Console.Write("#");
                        else 
                            Console.Write(".");

                    }  
                    Console.WriteLine();                  
                }
                
            }

            public void Step()
            {
                bool [,,,] step = new bool[dim, dim, dim, dim];

                for (int x = 1; x < dim-1; x++)
                {
                    for (int y = 1; y < dim-1; y++)
                    {
                        for (int z = 1; z < dim-1; z++)
                        {
                            
                            for (int w = 1; w < dim-1; w++)
                            {
                                // pocet sousedu
                                int sumactive = 0;
                                int sumcombinations = 0;

                                for(int nw = -1; nw <= 1; nw++)
                                {
                                    for(int nx = -1; nx <= 1; nx++)
                                    {
                                        for(int ny = -1; ny <= 1; ny++)
                                        {
                                            for(int nz = -1; nz <= 1; nz++)
                                            {
                                                if (nx == 0 && ny == 0 && nz == 0 && nw ==0) continue;

                                                sumcombinations++;
                                                if (active[x+nx, y+ny, z+nz, w + nw]) sumactive++;
                                            }                                     
                                        }                                
                                    }
                                }

                                int suminactive = sumcombinations - sumactive;

                                if (active[x,y,z, w])
                                {
                                    step[x,y,z,w] = (sumactive == 2 || sumactive == 3);
                                }
                                else
                                {
                                    step[x,y,z, w] = sumactive == 3;
                                }                                

                            }

                        } 
                    } 
                }

                active = step; 

            }

            public int GetActiveSum()
            {
                int sum = 0;
                for (int x = 1; x < dim-1; x++)
                {
                    for (int y = 1; y < dim-1; y++)
                    {
                        for (int z = 1; z < dim-1; z++)
                        {
                            for (int w = 1; w < dim-1; w++)
                            {                            
                                if (active[x,y,z, w]) sum++; 
                            }
                        }
                    }
                }   
                return sum;
            }


        }
        
        public static void Task()
        {
            Console.WriteLine("AOC2020_Day17");  

            var cube = new Cube();
            cube.Load(6);
            cube.printZ(0);
            for (int i =0; i < 6; i++)
            {
                cube.Step();
                cube.printZ(0);
            }
            Console.Write("Sum : {0}", cube.GetActiveSum());

        }

    }
}