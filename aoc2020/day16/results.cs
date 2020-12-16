using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Aoc2020
{

    class Day16
    {

        class Rule
        {
            public string Name;

            public struct Interval
            {
                public int From;
                public int To;
            };
            
            public Interval Interval1, Interval2;

            public bool ValidNumber(int nmb)
            {
                return nmb >= Interval1.From && nmb <= Interval1.To ||
                        nmb >= Interval2.From && nmb <= Interval2.To;
            }

            public bool ValidTicket(Ticket ticket, ref SortedSet<int> badnumbers)
            {
                foreach(var nmb in ticket)
                {
                    if (!ValidNumber(nmb))
                    {
                        badnumbers.Add(nmb);
                    }
                }

                return badnumbers.Count == 0;

            } 

        }

        static List<Rule> Rules = new List<Rule>();

        class Ticket : List<int> 
        {

            public void AvailableRules(List<Rule> rules, ref SortedSet<string> ret)
            {
                ret.Clear();

                for (int nmb = 0; nmb < Count; nmb++)
                {
                    foreach(var rule in rules)
                    {

                        if (rule.ValidNumber(this[nmb]))
                        {
                            ret.Add(string.Format("{0} {1}", rule.Name, nmb+1));
                        }
                    }
                }
            }
        }

        static Ticket MyTicket = null;

        static List<Ticket> NearbyTickets = new List<Ticket>();

        private static Ticket CsvLine2Ticket(string line)
        {
            var list = new Ticket();
            var nmbs = line.Split(",");
            foreach(var nmb in nmbs)
            {
                list.Add(int.Parse(nmb));
            }           
            return list;
        }

        private static void LoadInput()
        {
            var allfile = System.IO.File.ReadAllText(@"./day16/input.txt");
            
            var items = allfile.Split("\r\n\r\n");

            // rules
            var rules = items[0].Split("\r\n");
            foreach(var rule in rules)
            {
                // type: 50-635 or 642-966
                Match res2 = Regex.Match(rule,
                        @"(.+): (\d+)-(\d+) or (\d+)-(\d+)*$"
                    );
                if (!res2.Success) throw new Exception();
                
                var cr = new Rule();
                cr.Name = res2.Groups[1].Value.ToString();
                cr.Interval1.From = int.Parse(res2.Groups[2].Value.ToString());
                cr.Interval1.To = int.Parse(res2.Groups[3].Value.ToString());
                cr.Interval2.From = int.Parse(res2.Groups[4].Value.ToString());
                cr.Interval2.To = int.Parse(res2.Groups[5].Value.ToString());        
                Rules.Add(cr);
            }

            // my ticket
            var myticket = items[1].Split("\r\n");
            MyTicket = CsvLine2Ticket(myticket[1]);

            var nearbytickets = items[2].Split("\r\n");
            foreach(var ticket in nearbytickets)
            {
                if (ticket == "nearby tickets:") continue;
                NearbyTickets.Add(CsvLine2Ticket(ticket));
            }     
        }


        public static void Task12()
        {

            Console.WriteLine("AOC2020_Day16_Task12");   

            LoadInput();

            var invalid = new List<int>();

            SortedSet<string> rulepositions = null;

            //NearbyTickets.Add(MyTicket);

            foreach(var ticket in NearbyTickets)
            {
                SortedSet<int> invalidnmbs = null; //new ;
                foreach(var rule in Rules)
                {
                    var invalidrulenmbs = new SortedSet<int>();
                    if (rule.ValidTicket(ticket, ref invalidrulenmbs))
                    {
                        invalidnmbs = null;
                        break;
                    }
                    else
                    {
                        if (invalidnmbs == null)
                        {
                            invalidnmbs = invalidrulenmbs;
                        }
                        else
                        {
                            invalidnmbs.IntersectWith(invalidrulenmbs);
                        }
                        
                    }
                }
                // neplatny ticket
                if (invalidnmbs != null && invalidnmbs.Count > 0)
                {

                    foreach(var inv in invalidnmbs)
                        invalid.Add(inv);

                }
                // platny ticket
                else
                {
                    var actrulpos = new SortedSet<string>();
                    ticket.AvailableRules(Rules, ref actrulpos);

                    if (rulepositions == null)
                    {
                        rulepositions = actrulpos;
                    }
                    else
                    {
                        rulepositions.IntersectWith(actrulpos);
                    }

                }
            }

            int sum = 0;
            foreach(var inv in invalid)   
            {
                sum += inv;
            }

            Console.WriteLine("Task1 Sum : {0}", sum); 


            var dict = new Dictionary<string, SortedSet<int>>();

            foreach(var rule in rulepositions)
            {
                Match res2 = Regex.Match(rule,
                        @"(.+) (\d+)$"
                    );
                if (!res2.Success) throw new Exception();
                string name = res2.Groups[1].ToString();
                int pos = int.Parse(res2.Groups[2].ToString());

                if (!dict.ContainsKey(name)) dict.Add(name, new SortedSet<int>());
                dict[name].Add(pos);

            }
            
            long mult = 1;
            while(dict.Count > 0)
            {
                foreach(var d in dict)
                {

                    var copy = new SortedSet<int>(d.Value);

                    foreach (var d2 in dict)
                    {
                        if (d.Key == d2.Key) continue;

                        copy.ExceptWith(d2.Value);
                    }

                    if (copy.Count == 1)
                    {
                        if (d.Key.StartsWith("departure"))
                        {
                            int mynmb = MyTicket[copy.Min-1]; 
                            Console.WriteLine("{0} : {1} - {2}", d.Key, copy.Min, mynmb ); 
                            mult *= mynmb;

                        }

                        dict.Remove(d.Key);
                        
                        foreach (var dd in dict)
                        {
                            dd.Value.Remove(copy.Min);
                        }
                    }
                }
            }
            Console.WriteLine("Task2 :Mult {0}", mult); 
        }
    }
}