using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day6_Common
    {

        public enum SetOp
        {
            Union, 
            Intersect
        }

        public static List<HashSet<char>> GetGroups(SetOp op)
        {
            var inputfile =  new List<string>(System.IO.File.ReadAllLines(@"./day06/input.txt"));
            inputfile.Add("");

            List<HashSet<char>> groups = new List<HashSet<char>>();

            HashSet<char> group = null;
            foreach (var line in inputfile)
            {
                
                if (string.IsNullOrEmpty(line))
                {
                    if (group.Count >0)
                    {
                        groups.Add(group);
                    }
                    group = null;
                    continue;
                }
                
                if (group == null)
                {
                    group = new HashSet<char>(line);
                }
                else
                {
                    switch(op)
                    {
                        case SetOp.Union:
                        group.UnionWith(new HashSet<char>(line));
                        break;

                        case SetOp.Intersect:
                        group.IntersectWith(new HashSet<char>(line));
                        break;
                        
                    }
                }
                    
            }

            return groups;
        }       
    }
}