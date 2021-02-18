using System;
using System.Collections.Generic;

namespace Aoc2020
{
    class Day21
    {

        public class Food
        {
            public SortedSet<string> ingredients = null;
            public SortedSet<string> alergenes = null;
            public bool ContainsAlergen(string alergen) { return alergenes.Contains(alergen); }

        }

        class Foods
        {
            List<Food> foods = new List<Food>();

            SortedSet<string> alergenes = new SortedSet<string>();

            public void Read()
            {
                System.IO.StreamReader file =  new System.IO.StreamReader(@"./day21/input.txt");

                string line = "";
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Replace(")", "");

                    var foo = line.Split(" (contains ");

                    var food = new Food();
                    food.ingredients = new SortedSet<string>(foo[0].Split(" "));
                    food.alergenes = new SortedSet<string>(foo[1].Split(", "));
                    foods.Add(food);

                    alergenes.UnionWith(food.alergenes);
                    
                }
            }

            public void FindNoAlergenes()
            {
                foreach(var al in alergenes)
                {
                    var suspicious = new SortedSet<string>();
                    var free = new SortedSet<string>();

                    foreach(var food in foods)
                    {
                        if (food.ContainsAlergen(al))
                        {
                            if (suspicious.Count == 0)
                            {
                                suspicious = food.ingredients;
                                continue;
                            }

                            // problem je nekde v pruniku
                            var interserc = new SortedSet<string>(suspicious);
                            interserc.IntersectWith(food.ingredients);

                            // v tahle to nebude
                            suspicious.ExceptWith(interserc);
                            free.UnionWith(suspicious);
                            suspicious = interserc;

                            
                        }
                    }

                    Console.WriteLine(" ==== {0} ==== ", al); 

                    Console.Write(" suspicious : "); 
                    foreach(var s in suspicious)
                        Console.Write("{0}, ", s); 
                    Console.WriteLine(""); 

                   Console.Write(" free : "); 
                    foreach(var f in free)
                        Console.Write("{0}, ", f); 
                    Console.WriteLine(""); 



                }
            }
        }

        public static void Task1()
        {
            Console.WriteLine("AOC2020_Day21_1"); 

            Foods foods = new Foods();
            foods.Read();
            foods.FindNoAlergenes();
            

        }
    }
}
