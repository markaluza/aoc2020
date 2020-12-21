using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Day19
    {


        
        class CMyRegex
        {
            
            Dictionary<string, string> rules = new  Dictionary<string, string>();

            string matchstring = null;

            public void Compile()
            {
                matchstring = rules["0"];

                while(true)
                {
                    string[] digits = Regex.Split(matchstring, @"\D+");  

                    int validdigits = 0;
                    foreach (string value in digits)  
                    {  
                        if (string.IsNullOrEmpty(value)) continue;
                        validdigits++;

                        matchstring = matchstring.Replace(" " + value + " ", " ( " + rules[value] + " ) ");
                    } 
                    if (validdigits == 0) break;
                }     

                if (matchstring.Contains(" ")) 
                    matchstring = matchstring.Replace(" ", "");
                
                matchstring = "^" + matchstring + "$";

            }

            public bool IsComplied() { return matchstring != null;}

            public void PushRule(string rule)
            {
                var foo = rule.Split(": ");

                if (foo[1].Contains("\""))
                    foo[1] = foo[1].Replace("\"", "");

                string rl = " " + foo[1] + " ";

                /*int srhc = rl.IndexOf(" " + foo[0] + " ");

                if (srhc > 0)
                {

                   if (foo[0] == "11")
                   {
                       rl = "42 31 | ( 42 (?1)? 31 ) " ;
                   };

                   if (foo[0] == "8")
                   {
                       rl = " ( 42 )+ ";
                   };

                }*/

                rules.Add(foo[0],  rl);
            }

            public bool MatchRules(string passwd)
            {
                var match =  Regex.Match(passwd, matchstring);

                return match.Success;
            }
        }


        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day19_1"); 

            var lines = System.IO.File.ReadAllLines(@"./day19/input.txt");

            CMyRegex myregex = new CMyRegex();
            int validstrings = 0;

            foreach(var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    myregex.Compile();
                    continue;
                }

                if (!myregex.IsComplied())
                {
                    myregex.PushRule(line);
                    continue;
                }

                if (myregex.MatchRules(line))
                    validstrings++;

            }

            Console.WriteLine("Valid : {0}", validstrings); 


        }

        public static void Task2()
        {
            
            Console.WriteLine("AOC2020_Day19_2 -- !!! not ready"); 

            /*
            

            var lines = System.IO.File.ReadAllLines(@"./day19/input.txt");

            CMyRegex myregex = new CMyRegex();
            int validstrings = 0;

            foreach(var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    myregex.Compile();
                    continue;
                }

                string l2 = line;

                if (l2 == "8: 42")
                    l2 = "8: 42 | 42 8";
                if (l2 == "11: 42 31")
                    l2 = "11: 42 31 | 42 11 31";

                if (!myregex.IsComplied())
                {
                    myregex.PushRule(l2);
                    continue;
                }

                if (myregex.MatchRules(l2))
                    validstrings++;

            }

            Console.WriteLine("Valid : {0}", validstrings); */


        }

    }
}