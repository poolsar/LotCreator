using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrestaSharp.Lib
{
    public class Functions
    {
        public static string RemoveAccents(string InputString)
        {
            Regex replace_a_Accents = new Regex("[á|à|ä|â]", RegexOptions.Compiled);
            Regex replace_e_Accents = new Regex("[é|è|ë|ê]", RegexOptions.Compiled);
            Regex replace_i_Accents = new Regex("[í|ì|ï|î]", RegexOptions.Compiled);
            Regex replace_o_Accents = new Regex("[ó|ò|ö|ô]", RegexOptions.Compiled);
            Regex replace_u_Accents = new Regex("[ú|ù|ü|û]", RegexOptions.Compiled);
            InputString = replace_a_Accents.Replace(InputString, "a");
            InputString = replace_e_Accents.Replace(InputString, "e");
            InputString = replace_i_Accents.Replace(InputString, "i");
            InputString = replace_o_Accents.Replace(InputString, "o");
            InputString = replace_u_Accents.Replace(InputString, "u");
            return InputString;
        }

        public static string BuildLinkRewrite(string InputString)
        {
            InputString = RemoveAccents(InputString);
            InputString = InputString.ToLower();
            InputString = InputString.Replace(": ", "_");
            InputString = InputString.Replace(" / ", "_");
            InputString = InputString.Replace(" % ", "_");
            InputString = InputString.Replace("% ", "_");
            InputString = InputString.Replace(": ", "_");
            InputString = InputString.Replace(" : ", "_");
            InputString = InputString.Replace(":", "_");
            InputString = InputString.Replace(".", "");
            InputString = InputString.Replace("  ", "_");
            InputString = InputString.Replace(" ", "_");
            InputString = InputString.Replace("/", "_");
            InputString = InputString.Replace("%", "_");
            InputString = InputString.Replace("%", "_");
            InputString = InputString.Replace("ñ", "n");
            InputString = InputString.Replace("'", "_");
            InputString = InputString.Replace("'", "_");
            InputString = InputString.Replace("&#39;", "_");

            InputString = InputString.Replace("-", "_");
            InputString = InputString.Replace("(", "");
            InputString = InputString.Replace(")", "");
            InputString = InputString.Replace("\"", "");
            InputString = InputString.Replace("!", "");
            InputString = InputString.Replace("+", "");

            string old = InputString;

            string s = "-";
            InputString = Regex.Replace(InputString, "[^a-zA-Z0-9]", s).Replace(s + s, s);

            if (old != InputString)
            {

            }

            return InputString;
        }
    }

    public static class CollectionUtils
    {
        public static List<T> Tolist<T>(this ICollection collection)
        {
            List<T> res = new List<T>();

            foreach (var item in collection)
            {
                var tItem = (T)item;
                res.Add(tItem);
            }

            return res;
        }
    }
}
