using System;
using System.Collections.Generic;
using System.Linq;

namespace Aoc2020
{
    class Day23
    {
    
        //private static List<int> def_input = new List<int>(){ 3, 8, 9, 1, 2, 5, 4, 6, 7 };
        private static List<int> def_input = new List<int>(){ 9, 5, 2, 3, 1, 6, 4, 8, 7 };
        

        private static void rotate(List<int> input, int rqno)
        {
            while(true)
            {
                if (input[0]==rqno) return;
                var first = input[0]; input.RemoveAt(0);
                input.Add(first);
            }

        }

        private static int getdestPos(List<int> input, int rqval)
        {
            if (rqval == 0) {
                int max = input.Max();
                return input.FindIndex( a => a == max);
            }
            int dest = input.FindIndex( a => a == rqval);
            if (dest < 0) return getdestPos(input, rqval -1 );
            return dest;
        }

        public static void Task1()
        {

            Console.WriteLine("AOC2020_Day23_1"); 

            var input = new List<int>(def_input);
            var currentno = input[0];

            for (int move = 0; move < 100; move++)
            {

                rotate(input, currentno);
                Console.WriteLine("Move {0} - {1}", move +1, String.Join(", ", input) );
                
                var removed = input.GetRange(1, 3);
                input.RemoveRange(1, 3);

                int destpos = getdestPos(input, currentno -1);
                input.InsertRange(destpos +1, removed);
                    
                currentno = input[1];
                
            }

            rotate(input, 1);
            Console.WriteLine("Result {0}", String.Join("", input) );

            
        }

        class ListItem
        {
            public int value;
            public ListItem previtem = null;
            public ListItem nextitem = null;
        };


        static private void PrintList(ListItem first, int limit = 10)
        {
            ListItem iter = first;
            var result = new List<int>();
            int limcounter = 0;
            while(true && limcounter < limit)
            {
                result.Add(iter.value);
                iter = iter.nextitem;
                limcounter++;
                if (iter == first) break;

            }
            Console.WriteLine("Res- {0}", String.Join(",", result) );
        }

        public static void Task2()
        {

            
            int totmoves = 10000000;
            int maxno =    1000000;
            
            //int maxno = def_input.Count;
            //int totmoves = 100;

            Console.WriteLine("AOC2020_Day23_2"); 

            List<ListItem> items = new List<ListItem>();

            Console.WriteLine("BuildList");
            for (int i =0; i < maxno; i++)
            {
                ListItem it = new ListItem();
                if (i < def_input.Count)
                {
                    it.value = def_input[i];
                }
                else
                {
                    it.value = i+1;
                }
                items.Add(it);
            }

            Console.WriteLine("Dictionary");
            Dictionary<int, ListItem> itemdict = new Dictionary<int, ListItem>();
            for (int i =0; i < items.Count; i++)
            {
                ListItem it = items[i];
                if ( i == 0)
                    it.previtem = items[items.Count-1];
                else 
                    it.previtem = items[(i - 1)%items.Count];
                it.nextitem = items[(i + 1)%items.Count];
                itemdict[it.value] = it;
            }

            Console.WriteLine("Calculating");
            ListItem current = items[0];
            for (int move = 0; move < totmoves; move++)
            {
                //if (move %100 == 0)
                //Console.WriteLine("Move {0} - {1}", move +1, String.Join(", ", itemdict) );

                //PrintList(itemdict[1]);
                
                // zjistim ktere tri jsou odebrane
                ListItem firstremoved = current.nextitem;
                ListItem secondremoved = firstremoved.nextitem;
                ListItem lastremoved = secondremoved.nextitem;

                // najdu detination
                int destval = current.value -1;
                while(true)
                {
                    if (destval == firstremoved.value ||
                        destval == secondremoved.value ||
                        destval == lastremoved.value)
                        {
                            destval--;
                            continue;
                        }

                    break;
                }
                if (destval == 0)
                {
                    ListItem maxtest = lastremoved.nextitem;
                    while(maxtest != firstremoved)
                    {
                        if (destval < maxtest.value )
                            destval = maxtest.value;
                        maxtest = maxtest.nextitem;
                    }
                }

                ListItem destitem = itemdict[destval];
                //PrintList(itemdict[1]);
                //Console.WriteLine("Move {0} - curr {1} , dest {2}", move +1, current.value, destitem.value);
                
                // vyriznuti tri hodnot
                current.nextitem = lastremoved.nextitem;
                current.nextitem.previtem = current;

                // a jejich prilepeni za dest
                lastremoved.nextitem = destitem.nextitem;
                destitem.nextitem.previtem = lastremoved;
                destitem.nextitem = firstremoved;
                firstremoved.previtem = destitem;

                current = current.nextitem;
                
            }

            Console.WriteLine("FINAL");
            PrintList(itemdict[1]);

            ListItem second = itemdict[1].nextitem;
            ListItem third = second.nextitem;

            long res = (long) second.value * (long) third.value;
            Console.WriteLine("Res = {0}", res);
    


        }

    }
}
