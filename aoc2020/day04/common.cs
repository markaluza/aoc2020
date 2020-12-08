using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Aoc2020
{

    class Day4_Common
    {

        public class Passport
        {
            Dictionary<string, string> Items = new Dictionary<string, string>();

            public void SetItem(string name, string value) { Items.Add(name, value); }

            public bool IsValid()
            {
                return 
                    Items.ContainsKey("byr") && 
                    Items.ContainsKey("iyr") && 
                    Items.ContainsKey("eyr") && 
                    Items.ContainsKey("hgt") && 
                    Items.ContainsKey("hcl") && 
                    Items.ContainsKey("ecl") && 
                    Items.ContainsKey("pid");
            }

            private bool ValidateBounds(string it, int min, int max)
            {
                int itnmb = int.Parse(it); 
                return itnmb >= min && itnmb <= max;
            }

            public bool IsValid2()
            {
                if (!IsValid()) 
                {
                    return false;
                }

                if (
                    !ValidateBounds(Items["byr"], 1920, 2002) ||
                    !ValidateBounds(Items["iyr"], 2010, 2020) ||
                    !ValidateBounds(Items["eyr"], 2020, 2030)) 
                    return false;

                Match resulthgt = Regex.Match(Items["hgt"], @"^(\d+)(cm|in)$");

                if (
                    !Regex.Match(Items["hcl"], @"^#([a-f0-9]{6})$").Success ||
                    !Regex.Match(Items["ecl"], @"^(amb|blu|brn|gry|grn|hzl|oth)$").Success || 
                    !Regex.Match(Items["pid"], @"^([0-9]{9})$").Success ||
                    !resulthgt.Success)
                     return false;

                switch(resulthgt.Groups[2].ToString())
                {
                    case "cm":
                        if (!ValidateBounds(resulthgt.Groups[1].ToString(), 150, 193)) return false;
                    break;
                    case "in":
                        if (!ValidateBounds(resulthgt.Groups[1].ToString(), 59, 76)) return false;
                    break;
                }                     

                return true;

            }
        }

        public static List<Passport> GetPassports()
        {
            List<Passport> Passports = new List<Passport>();

            var inputfile =  new List<string>(System.IO.File.ReadAllLines(@"./day04/input.txt"));
            inputfile.Add(""); // at se tam prida i posledni

            Passport pass = new Passport();
            foreach (string line in inputfile)
            {
                if (string.IsNullOrEmpty(line))
                {
                    Passports.Add(pass);
                    pass = new Passport();
                    continue;
                }

                var items = line.Split(' ');
                foreach (var item in items)
                {
                    var it = item.Split(":");
                    if (it.Length != 2) return null;
                    pass.SetItem(it[0], it[1]);
                } 
        
            } 

            return Passports;             

        }

    
    }
}