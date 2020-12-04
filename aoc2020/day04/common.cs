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

            private bool ValidItemBounds(string it, int min, int max)
            {
                int itnmb = int.Parse(Items[it]); 
                return itnmb >= min && itnmb <= max;
            }

            public bool IsValid2()
            {
                if (!IsValid()) 
                {
                    return false;
                }

                if (!ValidItemBounds("byr", 1920, 2002)) return false;
                if (!ValidItemBounds("iyr", 2010, 2020)) return false;
                if (!ValidItemBounds("eyr", 2020, 2030)) return false;

                Regex rehgt = new Regex(@"^(\d+)(cm|in)$");
                Match resulthgt = rehgt.Match(Items["hgt"]);
                if (!resulthgt.Success) 
                {
                    return false;
                }
                int hgt = int.Parse(resulthgt.Groups[1].ToString());
                switch(resulthgt.Groups[2].ToString())
                {
                    case "cm":
                        if (hgt < 150 || hgt > 193) 
                        {
                            return false;
                        }
                    break;
                    case "in":
                        if (hgt < 59 || hgt > 76) 
                        {
                            return false;
                        }
                    break;
                }

                Regex rehcl = new Regex(@"^#([a-f0-9]{6})$");
                Match resulthcl = rehcl.Match(Items["hcl"]);
                if (!resulthcl.Success) 
                {
                    return false;
                }

                Regex reecl = new Regex(@"^(amb|blu|brn|gry|grn|hzl|oth)$");
                Match resultecl = reecl.Match(Items["ecl"]);
                if (!resultecl.Success) 
                {
                    return false;
                }           
 
                Regex repid = new Regex(@"^([0-9]{9})$");
                Match resultpid = repid.Match(Items["pid"]);
                if (!resultpid.Success) 
                {
                    return false;
                }

                return true;

            }
        }

        static private Passport GeneratePassport(string line)
        {
            var items = line.Split(' ');
            Passport pass = new Passport();
            foreach (var item in items)
            {
                var it = item.Split(":");
                if (it.Length != 2) return null;
                pass.SetItem(it[0], it[1]);
            } 
            return pass;
        }

        public static List<Passport> GetPassports()
        {
            List<Passport> Passports = new List<Passport>();
            var inputfile = System.IO.File.ReadAllLines(@"..\input_4.i");

            string passportline = "";
            foreach (string line in inputfile)
            {
                if (line.Length > 0)
                    if (passportline.Length > 0) passportline+=" ";
                passportline  +=  line;

                if (line.Length <= 0)
                {
                    var pass = GeneratePassport(passportline);
                    if (pass != null)
                        Passports.Add(pass);

                    passportline = "";
                }
            }

            if ( passportline != "")
            {
                var pass = GeneratePassport(passportline);
                if (pass != null)
                    Passports.Add(pass);
            }      

            return Passports;             

        }

    
    }
}