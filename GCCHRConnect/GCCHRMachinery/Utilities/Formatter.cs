using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GCCHRMachinery.Utilities
{
    public class Formatter
    {
        /// <summary>
        /// This function takes in an unformatted string and returns it by changing to proper case, adding/removing relevant spaces
        /// </summary>
        /// <param name="unformatted">The string to be formatted</param>
        /// <returns></returns>
        public static string FormatString(string unformatted)
        {
            #region Convert string to Title/Proper case
            string formatted;
            TextInfo ti = new CultureInfo("en-US", false).TextInfo;
            unformatted = unformatted.ToLower();
            formatted = ti.ToTitleCase(unformatted);
            String finalstring = "";
            string[] Newformat = formatted.Split(' ');
            foreach (string words in Newformat)
            {
                finalstring += words;
            }
            #endregion

            //Add spaces
            string Str = "";
            int i = 0;
            //Looping through each letter of finalstring
            foreach (char character in finalstring)
            {
                Str += character;
                if (i < finalstring.Length - 2)
                {
                    char nextCharacter = char.Parse(finalstring.Substring(i + 1, 1));
                    //To add spaces before capital letters
                    if (character != '(' && char.IsUpper(nextCharacter))
                    {
                        Str += " ";
                    }

                    //To add spaces after '.'
                    if (character == '.' && nextCharacter != ')' && nextCharacter != ' ')
                    {
                        Str += " ";
                    }
                    i++;
                }
            }
            return Str;
        }

        public static List<string> TrimList(List<string> rawList)
        {
           List<string> trimmedList =  rawList.Select(d => d.Trim()).ToList();
            return trimmedList;
        }
    }
}