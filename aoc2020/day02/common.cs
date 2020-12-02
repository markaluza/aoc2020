using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Aoc2020
{

    class Day2_Common
    {
        public struct Policy
        {
            public int a;
            public int b;

            public char letter;

            public string password;
        }
        public static List<Policy> GetInput()
        {
            string line;


            System.IO.StreamReader file =  new System.IO.StreamReader(@"..\input_2.i");

            List<Policy> policies = new List<Policy>();

            while ((line = file.ReadLine()) != null)
            {
                
                var parts = line.Split(':', ' ', '-'); // 9-12 q: qqqxhnhdmqqqqjz

                if (parts.Length != 5)
                {
                    Debug.Assert(false);
                    continue;
                }

                Policy p = new Policy();
                p.a = Convert.ToInt32(parts[0]);
                p.b = Convert.ToInt32(parts[1]);
                p.letter =  Convert.ToChar(parts[2]);
                p.password = parts[4];

                policies.Add(p);

            }
            return policies;
        }      
    }
}