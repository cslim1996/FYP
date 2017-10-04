using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExamTimetabling2016
{
    class ConstraintDA
    {
        public ConstraintDA()
        {
        }
        public bool constraintValidation(string[] variable, double[] input)
        {
            //System.Console.WriteLine(string.Join(",", variable));
            System.Console.WriteLine(string.Join(",", input));
            var foundconstraint = new List<int>();
            int[] arrayofconstraint = foundconstraint.ToArray();
            //track input parameter "variable"
            int c = 0;
            //track input parameter "input" 
            int b = 0;
            int d = 0;
            //track the line of file
            int line = 0;
            int count = 0;
            Boolean? valid = null;
            Boolean? valid2 = null;
            bool finalvalidation = false;
            string[] text = System.IO.File.ReadAllLines(@"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
            foreach (string a in text)
            {

                string[] word = a.Split(' ');
                for (int i = 0; i < word.Length; i++)
                {
                    if (Regex.IsMatch(word[i], @"^[a-zA-Z]+$"))
                        count++;
                }
                for (int i = 0; i < word.Length; i++)
                {

                    if (Regex.IsMatch(word[i], @"^[a-zA-Z]+$"))
                    {
                        if (d == variable.Length)
                        {
                            break;
                        }
                        else if (String.Equals(word[i].ToUpper(), variable[d].ToUpper()))
                        {
                            i = -1;
                            d++;
                            c++;

                        }
                    }

                }
                d = 0;
                //track the constraint line number
                line++;
                //System.Console.WriteLine("line number:" + line);
                //System.Console.WriteLine(a);
                if (c == count && c == variable.Length)
                {
                    //constraint used will store to string list
                    foundconstraint.Add(line);
                    for (int z = 0; z < word.Length; z++)
                    {
                        if (b == variable.Length)
                        {
                            break;
                        }
                        else if (String.Equals(word[z].ToUpper(), variable[b].ToUpper()))
                        {
                            if (z + 1 == word.Length)
                                break;
                            else
                            {
                                switch (word[z + 1])
                                {
                                    case ">=":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] >= input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] >= Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "==":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] == input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] == Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case ">":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] > input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] > Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "!=":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] != input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            //System.Console.WriteLine(word[z] + "=" + word[z + 2] + "!=" + input[b]);
                                            if (input[b] != Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "<":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] < input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] < Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "<=":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] <= input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] <= Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;

                                }
                                //System.Console.WriteLine(word[z] + "  " + valid);
                            }
                            if (!valid2.HasValue)
                            {
                                valid2 = valid;
                            }
                            else if (z == 0)
                                break;
                            else if (word[z - 1].Equals("||"))
                            {
                                //System.Console.Write(word[z - 1]);
                                valid2 = (bool)valid ||
                                    (bool)valid2;
                            }
                            else if (word[z - 1].Equals("&&"))
                            {
                                //System.Console.Write(word[z - 1]);
                                valid2 = (bool)valid && (bool)valid2;
                            }
                            if (valid2 == true)
                                finalvalidation = true;
                            else
                                finalvalidation = false;
                            z = -1;
                        }

                    }
                }
                c = 0;
                b = 0;
                valid2 = null;
                valid = null;
                count = 0;
            }
            System.Console.WriteLine("used constraint:");
            Console.WriteLine(string.Join(",", foundconstraint));
            //Console.ReadLine();
            //Console.Clear();
            return finalvalidation;
        }

        public double invigilatorRequiredConstraint(string[] variable, double[] input)
        {
            //System.Console.WriteLine(string.Join(",", variable));
            System.Console.WriteLine(string.Join(",", input));
            var foundconstraint = new List<int>();
            int[] arrayofconstraint = foundconstraint.ToArray();
            //track input parameter "variable"
            int c = 0;
            //track input parameter "input" 
            int b = 0;
            int d = 0;
            //track the line of file
            int line = 0;
            int count = 0;
            Boolean? valid = null;
            Boolean? valid2 = null;
            string[] text = System.IO.File.ReadAllLines(@"D:\ExamTimetabling2016(Combined)\FYP\ExamTimetabling2016(FINAL TESTED)\ExamTimetabling2016\constraint.txt");
            foreach (string a in text)
            {

                string[] word = a.Split(' ');
                for (int i = 0; i < word.Length; i++)
                {
                    if (Regex.IsMatch(word[i], @"^[a-zA-Z]+$"))
                        count++;
                }
                for (int i = 0; i < word.Length; i++)
                {

                    if (Regex.IsMatch(word[i], @"^[a-zA-Z]+$"))
                    {
                        if (d == variable.Length)
                        {
                            break;
                        }
                        else if (String.Equals(word[i].ToUpper(), variable[d].ToUpper()))
                        {
                            i = -1;
                            d++;
                            c++;

                        }
                    }

                }
                d = 0;
                //track the constraint line number
                line++;
                //System.Console.WriteLine("line number:" + line);
                //System.Console.WriteLine(a);
                if (c == count && c == variable.Length)
                {
                    //constraint used will store to string list
                    foundconstraint.Add(line);
                    for (int z = 0; z < word.Length; z++)
                    {
                        if (b == variable.Length)
                        {
                            break;
                        }
                        else if (String.Equals(word[z].ToUpper(), variable[b].ToUpper()))
                        {
                            if (z + 1 == word.Length)
                                break;
                            else
                            {
                                switch (word[z + 1])
                                {
                                    case ">=":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] >= input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] >= Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "==":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] == input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] == Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case ">":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] > input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] > Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "!=":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] != input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            //System.Console.WriteLine(word[z] + "=" + word[z + 2] + "!=" + input[b]);
                                            if (input[b] != Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "<":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] < input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] < Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "<=":
                                        if (Regex.IsMatch(word[z], @"^[a-zA-Z]+$") && Regex.IsMatch(word[z + 2], @"^[a-zA-Z]+$"))
                                        {
                                            if (input[b] <= input[b + 1])
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        else
                                        {
                                            if (input[b] <= Double.Parse(word[z + 2]))
                                                valid = true;
                                            else
                                                valid = false;
                                            b++;
                                        }
                                        break;
                                    case "=":
                                        break;
                                }
                                //System.Console.WriteLine(word[z] + "  " + valid);
                            }
                            if (!valid2.HasValue)
                            {
                                valid2 = valid;
                            }
                            else if (z == 0)
                                break;
                            else if (word[z - 1].Equals("||"))
                            {
                                //System.Console.Write(word[z - 1]);
                                valid2 = (bool)valid ||
                                    (bool)valid2;
                            }
                            else if (word[z - 1].Equals("&&"))
                            {
                                //System.Console.Write(word[z - 1]);
                                valid2 = (bool)valid && (bool)valid2;
                            }
                            else if (word[z - 1].Equals("->"))
                            {
                                if (valid2 == true)
                                    return Double.Parse(word[z + 2]);
                            }

                        }

                    }
                }
                c = 0;
                b = 0;
                valid2 = null;
                valid = null;
                count = 0;

            }
            System.Console.WriteLine("used constraint:");
            Console.WriteLine(string.Join(",", foundconstraint));
            return 0;
            //Console.ReadLine();
            //Console.Clear();
        }
        public double convertDayOfWeek(DateTime dayOfWeek)
        {
            double date = 0;
            switch (dayOfWeek.Date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    date = 1;
                    break;
                case DayOfWeek.Thursday:
                    date = 4;
                    break;
                case DayOfWeek.Tuesday:
                    date = 2;
                    break;
                case DayOfWeek.Wednesday:
                    date = 3;
                    break;
                case DayOfWeek.Friday:
                    date = 5;
                    break;
                case DayOfWeek.Saturday:
                    date = 6;
                    break;
            }
            return date;
        }
        public double convertGender(char gender)
        {
            Console.Write(gender);
            if (gender.ToString().Equals("M"))
            {
                return 1;
            }
            else
                return 2;
        }
        public double convertMuslim(bool muslim)
        {
            if (muslim == true)
                return 1;
            else
                return 2;
        }
        public double convertPeriod(string period)
        {
            if (period.ToString().Equals("PM"))
                return 2;
            else if (period.ToString().Equals("AM"))
                return 1;
            else
                return 3;
        }
    }

}
