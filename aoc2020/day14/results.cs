using System;
using System.Collections.Generic;


namespace Aoc2020
{

    class Day14
    {

        public static string ToBinStr_(ulong n)
        {
            if (n < 2) return n.ToString();

            var divisor = n / 2;
            var remainder = n % 2;

            return ToBinStr_(divisor) + remainder;
        }


        public static char [] ToBinStr(ulong n)
        {
            string foo = ToBinStr_(n);
            while(foo.Length < 36)
                foo = "0" + foo;

            return foo.ToCharArray();
        }            
     
        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day14_Task1");

            var lines = new List<string>(System.IO.File.ReadAllLines(@"./day14/input.txt"));

            var memory = new Dictionary<ulong, ulong>();

            char [] scurmask = null;

            foreach(var line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    scurmask = line.Substring(7).ToCharArray();
                    continue;
                }
                else if (line.StartsWith("mem["))
                {
                    char [] value = ToBinStr(ulong.Parse(line.Substring( line.IndexOf("=") + 2)));
                    ulong v_addr = ulong.Parse(line.Substring( line.IndexOf("[") + 1, line.IndexOf("]")  - line.IndexOf("[") - 1));

                    for (int i =0; i < 36; i++)
                    {
                        switch(scurmask[i])
                        {
                            case '0': value[i] = '0'; break;
                            case '1': value[i] = '1'; break;
                        }
                    }

                    string svalue = new string(value);
                    ulong res = Convert.ToUInt64(svalue, 2);

                    if (memory.ContainsKey(v_addr))
                    {
                        memory[v_addr] = res;
                    }
                    else
                    {
                        memory.Add(v_addr, res);
                    }

                }
                else
                {
                    throw new Exception("err input...");
                }

            }

            ulong sum = 0;
            foreach(var memitem in memory)
            {
                sum += memitem.Value;
            }

            Console.WriteLine("Sum : {0}", sum);

        }

        static ulong IntPow(ulong x, ulong pow)
        {
            ulong ret = 1;
            while ( pow != 0 )
            {
                if ( (pow & 1) == 1 )
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day14_Task2");

            var lines = new List<string>(System.IO.File.ReadAllLines(@"./day14/input.txt"));

            var memory = new Dictionary<ulong, ulong>();

            char [] curmask = null;
            uint curmask_x = 0; 

            foreach(var line in lines)
            {
                if (line.StartsWith("mask = "))
                {
                    curmask = line.Substring(7).ToCharArray();
                    curmask_x = 0; 
                    foreach(var ch in curmask) if (ch == 'X') curmask_x ++;
                    continue;
                }
                else if (line.StartsWith("mem["))
                {
                    var value = ulong.Parse(line.Substring( line.IndexOf("=") + 2));
                    var saddr = ToBinStr(ulong.Parse(line.Substring( line.IndexOf("[") + 1, line.IndexOf("]")  - line.IndexOf("[") - 1)));

                    for (ulong i = 0; i < IntPow(2, curmask_x); i++ )
                    {
                        var iaddr = ToBinStr(i);
                        char [] tmpaddr = new char[36];
                        Array.Copy(saddr, tmpaddr, 36);

                        int xno = 0;
                        for (int j =0; j < 36; j++)
                        {
                            switch(curmask[j])
                            {
                                case '0': break;                     // ponecham
                                case '1': tmpaddr[j] = '1'; break;   // prehodim
                                case 'X': tmpaddr[j] = iaddr[iaddr.Length -1 - xno]; xno++; break; // x-té ixko nahradim 
                            }
                        }

                        string stmpaddr = new string(tmpaddr);
                        ulong addr = Convert.ToUInt64(stmpaddr, 2);

                        if (memory.ContainsKey(addr))
                        {
                            memory[addr] = value;
                        }
                        else
                        {
                            memory.Add(addr, value);
                        }

                    }


                }
                else
                {
                    throw new Exception("err input...");
                }

            }

            ulong sum = 0;
            foreach(var memitem in memory)
            {
                sum += memitem.Value;
            }

            Console.WriteLine("Sum : {0}", sum);

        }


    }
}