using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Day18
    {

        static bool Solve2(Stack<Int64> vals, Stack<char> ops, bool prec)
        {
            if (ops.Count == 0 || ops.Peek() == '(') return false;

            switch(ops.Pop())
            {
                case '+': vals.Push(vals.Pop() + vals.Pop()); return true;
                case '*': 
                    if (!prec) 
                    {   // bez precedence udelej jenom pokud je to treba
                        vals.Push(vals.Pop() * vals.Pop());                     
                        return true;
                    }
                    ops.Push('*');
                    return false;
                default:
                    throw new NotImplementedException();
             }

        }
        static Int64 Solve(string exp, bool prec)
        {
            exp = exp.Replace(" ", "");

            var ops = new Stack<char>();
            var vals = new Stack<Int64>();
            
            foreach(var ch in exp)
            {
                switch(ch)
                {

                case ' ': continue;

                case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9':
                    vals.Push(ch - '0');
                    Solve2(vals, ops, prec); // udelej operace pokud muzes
                    break;

                case ')': 
                    while(Solve2(vals, ops, false)) // zpracuj obsah cele zavorky
                    {
                    };

                    // popr vyres vse pred tim
                    ops.Pop();
                    Solve2(vals, ops, prec);            
                    break;


                case '+': case '*': case '(':
                    ops.Push(ch); 
                    break;

                default:
                    throw new NotImplementedException();
                }
                
            }

            // zpracuj zbytek
            while(ops.Count > 0 && ops.Peek() != '(')
            {
                Solve2(vals, ops, false);
            };            

            if (vals.Count != 1 || ops.Count != 0)
            {
                throw new Exception("invalid input");
            }

            return vals.Pop();

        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day18_1"); 

            Int64 sum = 0;
            var lines = System.IO.File.ReadAllLines(@"./day18/input.txt");

            foreach(var line in lines)
            {
                sum += Solve(line, false);
            }

            Console.WriteLine("Sun : {0}", sum); 
            
        }   

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day18_2"); 

            Int64 sum = 0;
            var lines = System.IO.File.ReadAllLines(@"./day18/input.txt");

            foreach(var line in lines)
            {
                sum += Solve(line, true);
            }

            Console.WriteLine("Sun : {0}", sum); 

        }          
    }
}