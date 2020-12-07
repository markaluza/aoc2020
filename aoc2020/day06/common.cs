using System;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day6_Common
    {
        public static List<HashSet<char>> GetGroups()
        {
            var inputfile =  new List<string>(System.IO.File.ReadAllLines(@"..\input_6.i"));
            inputfile.Add("");

            List<HashSet<char>> groups = new List<HashSet<char>>();

            HashSet<char> group = new HashSet<char>();
            foreach (var line in inputfile)
            {
                
                if (string.IsNullOrEmpty(line))
                {
                    if (group.Count >0)
                    {
                        groups.Add(group);
                        group = new HashSet<char>();
                    }
                    continue;
                }
                
                foreach (char ch in line)
                    group.Add(ch);

            }

            return groups;
        }

        public static List<HashSet<char>> GetGroups2()
        {
            var inputfile =  new List<string>(System.IO.File.ReadAllLines(@"..\input_6.i"));
            inputfile.Add("");

            List<HashSet<char>> groups = new List<HashSet<char>>();

            HashSet<char> group = null; 
            foreach (var line in inputfile)
            {
                
                if (string.IsNullOrEmpty(line))
                {
                    if (group!=null)
                    {
                        groups.Add(group);
                    }
                    group = null; 
                    continue;
                }
                
                if (group == null)
                {
                    group = new HashSet<char>();
                    foreach (char ch in line)
                    {
                        group.Add(ch);
                    }
                }
                else
                {

                    var lineset = new HashSet<char>(line);

                    foreach (char chgroup in group)
                    {
                        if (!lineset.Contains(chgroup))
                        {
                            group.Remove(chgroup);
                        }
                    } 
                }
                

            }

            return groups;
        }                  
    }
}