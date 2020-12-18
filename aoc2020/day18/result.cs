using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Day18
    {

        static void Solve(Stack<Int64> vals, Stack<char> ops)
        {
            while (ops.Count > 0 && ops.Peek() != '(')
            {
                switch(ops.Pop())
                {
                    case '+': vals.Push(vals.Pop() + vals.Pop()); break;
                    case '*': vals.Push(vals.Pop() * vals.Pop()); break;    
                    throw new NotImplementedException();
                }
            }

        }
        static Int64 Solve(string exp)
        {
            exp = exp.Replace(" ", "");
            exp += "X";

            var ops = new Stack<char>();
            var vals = new Stack<Int64>();
            
            foreach(var ch in exp)
            {
                switch(ch)
                {

                case ' ': continue;

                case '0': case '1': case '2': case '3': case '4': case '5': case '6': case '7': case '8': case '9':
                    vals.Push(ch - '0');
                    Solve(vals, ops);
                    break;

                case ')': case 'X':
                    Solve(vals, ops);
                    if (ch == ')') 
                    {
                        ops.Pop();
                        Solve(vals, ops);
                    }                    
                    break;

                case '+': case '*': case '(':
                    ops.Push(ch); 
                    break;

                default:
                    throw new NotImplementedException();
                }
                
            }

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
                sum += Solve(line);
            }

            Console.WriteLine("Sun : {0}", sum); 
            
        }   


    }

}