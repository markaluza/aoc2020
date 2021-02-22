using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day21
    {

        public class Food
        {
            public List<string> ingredients = null;
            public SortedSet<string> alergenes = null;
            public bool ContainsAlergen(string alergen) { return alergenes.Contains(alergen); }

        }

        class Foods
        {
            public List<Food> foods = new List<Food>();

            SortedSet<string> allalergenes = new SortedSet<string>();
            public SortedSet<string> allingrediences = new SortedSet<string>();

            public void Read()
            {
                System.IO.StreamReader file =  new System.IO.StreamReader(@"./day21/input.txt");

                string line = "";
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Replace(")", "");

                    var foo = line.Split(" (contains ");

                    var food = new Food();
                    food.ingredients = new List<string>(foo[0].Split(" "));
                    food.alergenes = new SortedSet<string>(foo[1].Split(", "));
                    foods.Add(food);

                    allalergenes.UnionWith(food.alergenes);
                    allingrediences.UnionWith(food.ingredients);
                    
                }
            }

            public List<Tuple<string, string>> FindAlergenes()
            {

                var alergenesfound = new Dictionary<string, SortedSet<string>>();

                foreach(var al in allalergenes)
                {
                    var suspicious = new SortedSet<string>();
                    var free = new SortedSet<string>();

                    foreach(var food in foods)
                    {
                        if (food.ContainsAlergen(al))
                        {
                            if (suspicious.Count == 0)
                            {
                                suspicious = new SortedSet<string>(food.ingredients);
                                continue;
                            }

                            // problem je nekde v pruniku
                            var interserc = new SortedSet<string>(suspicious);
                            interserc.IntersectWith(food.ingredients);
                            // v techhle to nebude...
                            suspicious.ExceptWith(interserc);
                            // a tyhle muzu je klidem vyradit
                            free.UnionWith(suspicious); 
                            suspicious = interserc;

                        }
                    }

                    alergenesfound.Add(al, suspicious);

                }

                var alergeneseliminated = new List<Tuple<string, string>>();

                while(alergenesfound.Count > 0)
                {
                    foreach(var alergene in alergenesfound)
                    {
                        if (alergene.Value.Count == 1)
                        {
                            alergeneseliminated.Add(new Tuple<string, string>(alergene.Key, alergene.Value.Min));
                            alergenesfound.Remove(alergene.Key);

                            foreach(var al in alergenesfound)
                            {
                                if (al.Value.Contains(alergene.Value.Min))
                                {
                                    al.Value.Remove(alergene.Value.Min);
                                }
                            }

                        }
                    }
                }

                return alergeneseliminated;

            }


        }

        public static void Task1()
        {
            Console.WriteLine("AOC2020_Day21_1"); 

            Foods foods = new Foods();
            foods.Read();
            var alergenes = foods.FindAlergenes();

            var found = new SortedSet<string>();
            foreach(var i in alergenes)
            {
                found.Add(i.Item2);
                Console.WriteLine("{0} - {1}", i.Item1, i.Item2); 
            };

            int tot = 0; int t2 = 0;
            foreach(var food in foods.foods)
            {
                t2 += food.ingredients.Count;
                foreach (var ingr in  food.ingredients)
                {
                    if (!found.Contains(ingr))
                    {
                        tot ++;
                    }
                }
            }

            Console.WriteLine("OK Ingrs = {0}", tot);
            
        }

        public static void Task2()
        {
            Console.WriteLine("AOC2020_Day21_2"); 

            Foods foods = new Foods();
            foods.Read();
            var alergenes = foods.FindAlergenes();

           alergenes.Sort((x, y) => {
                return x.Item1.CompareTo(y.Item1);
            });

            Console.Write("Ret - ");

            foreach(var al in alergenes)
            {
                Console.Write("{0},", al.Item2);
            }

            
        }

    }
}
