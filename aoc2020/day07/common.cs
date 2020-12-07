using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Aoc2020
{

    class Day7_Common
    {

        static public Dictionary<string, Dictionary<string, int>> GetRules()
        {
            var Rules  = new Dictionary<string, Dictionary<string, int>>();
            var lines =  new List<string>(System.IO.File.ReadAllLines(@"..\input_7.i"));

            foreach (var line in lines)           
            {
                Match res = Regex.Match(
                    line, 
                    @"^(.+) bags contain (.*)$"
                    );

                if (!res.Success) throw new Exception();

                string clr = res.Groups[1].ToString();
                var contains = new Dictionary<string, int>();
                if (res.Groups[2].ToString() != "no other bags.")
                {
                    var bags = res.Groups[2].ToString().Split(", ");
                    foreach(var bag in bags)
                    {
                        Match res2 = Regex.Match(bag,
                                @"(\d+) (.+) bag[s\.]*$"
                            );
                        if (!res2.Success) throw new Exception();

                        int nmb = int.Parse(res2.Groups[1].ToString());
                        string clr2 = res2.Groups[2].ToString();
                        contains.Add(clr2, nmb);
                        
                    };

                }
                Rules.Add(clr, contains);
    
            }

            return Rules;
        }

    }
}