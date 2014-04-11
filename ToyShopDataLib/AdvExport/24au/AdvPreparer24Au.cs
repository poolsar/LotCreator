using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToyShopDataLib.AdvExport
{
    public class AdvPreparer24Au : AdvPreparer
    {
        public override decimal PreparePrice(decimal price)
        {
            price = (int)price;
            return price;
        }

        public override string PrepareTitle(string text)
        {
            text = base.PrepareTitle(text);

            text = PrepareCamelCase(text);
            return text;
        }

        public override string PrepareDescription(string text)
        {
            text = base.PrepareTitle(text);

            text = PrepareCamelCase(text);
            return text;
        }

        //private string RemovePolicyWords(string title)
        //{
        //    var policyWords = PolicyWords;

        //    foreach (var policyWord in policyWords)
        //    {
        //        title = title.Replace(policyWord + " ", "");
        //        title = title.Replace(" " + policyWord, "");
        //    }

        //    return title;
        //}



        private string PrepareCamelCase(string title)
        {
            var largeWords =
                title.Split(new[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var sb = new StringBuilder();
            for (int i = 0; i < largeWords.Count; i++)
            {
                if (i > 0) sb.Append(" ");
                var largeWord = largeWords[i];

                if (largeWord.Length > 5)
                {
                    var substring = largeWord.Substring(0, 6);
                    bool manyApperCase = substring.ToUpper() == substring;

                    if (manyApperCase)
                    {
                        largeWord = ToCamelCase(largeWord);
                    }
                }

                // sb.Append(largeWord);
                title = title.Replace(largeWords[i], largeWord);
            }

            string result;
            //string result = sb.ToString();

            result = title;

            return result;
        }

        private string ToCamelCase(string largeWord)
        {
            string result = largeWord[0] + largeWord.Substring(1).ToLower();
            return result;

        }

        public override string[] PolicyWords
        {
            get
            {
                return new[] { "в ассортименте", "в ассорт", @"regex\d+\s?шт", "цена за" };
            }
        }

    }
}