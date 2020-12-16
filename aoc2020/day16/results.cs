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

            public bool ValidTicket(Ticket ticket, ref SortedSet<int> badnumbers)
            {
                foreach(var nmb in ticket)
                {
                    if (!(
                        nmb >= Interval1.From && nmb <= Interval1.To ||
                        nmb >= Interval2.From && nmb <= Interval2.To))
                        {
                            badnumbers.Add(nmb);
                        }
                }

                return badnumbers.Count == 0;

            }

        }

        static List<Rule> Rules = new List<Rule>();

        class Ticket : List<int> {};

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


        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day16_Task1");   

            LoadInput();

            var invalid = new List<int>();
            int valtickets = 0;
            foreach(var ticket in NearbyTickets)
            {
                SortedSet<int> invalidnmbs = null; //new ;
                foreach(var rule in Rules)
                {
                    var invalidrulenmbs = new SortedSet<int>();
                    if (rule.ValidTicket(ticket, ref invalidrulenmbs))
                    {
                        invalidnmbs = null;
                        valtickets++;
                        break;
                    }
                    else
                    {
                        if (invalidnmbs == null)
                        {
                            invalidnmbs = new SortedSet<int>(invalidrulenmbs);
                        }
                        invalidnmbs.IntersectWith(invalidrulenmbs);
                    }
                }
                if (invalidnmbs != null)
                {
                    foreach(var inv in invalidnmbs)
                        invalid.Add(inv);
                }
            }

            int sum = 0;
            foreach(var inv in invalid)   
            {
                sum += inv;
            }

            Console.WriteLine("Sum : {0}", sum); 

        }

        public static void Task2()
        {

            Console.WriteLine("AOC2020_Day16_Task2");

        }

    }
}