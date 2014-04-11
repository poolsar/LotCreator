using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ToyShopDataLib.Utils
{
    public static class StringUtils
    {
        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static decimal ToDecimal(this string str)
        {
            var result = Convert.ToDecimal(str);
            return result;
        }

        public static string InBrakets(this string str)
        {
            var result = string.Format("{{{0}}}", str);
            return result;
        }

        /// <summary>
        /// Добавить новую строку
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NL(this string str)
        {
            var result = str + Environment.NewLine;
            return result;
        }


        public static string CleatByRegex(this string str,string regex)
        {
            string result = string.Empty;

            Regex regexI = new Regex(regex);

            result = regexI.Replace(str, "");

            if (result != str)
            {
                
            }
            
            
            return result;
        }

        
    }
}
