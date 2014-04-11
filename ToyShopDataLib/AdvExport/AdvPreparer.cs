using System.Collections.Generic;
using ToyShopDataLib.Utils;

namespace ToyShopDataLib.AdvExport
{
    public class AdvPreparer
    {
        public virtual decimal PreparePrice(decimal price)
        {
            return price;
        }

        public virtual string PrepareTitle(string text)
        {
            text = ClearPolicyWords(text);
            return text;
        }

        public virtual string PrepareDescription(string text)
        {
            text = ClearPolicyWords(text);
            return text;
        }

        protected virtual string ClearPolicyWords(string text)
        {
            foreach (string policyWord in AllPolicyWords)
            {
                var isRegex = policyWord.Contains("regex");

                if (isRegex)
                {
                    var regex = policyWord.Replace("regex", "");
                    text = text.CleatByRegex(regex);
                }
                else
                {
                    text = text.Replace(policyWord + " ", "");
                    text = text.Replace(" " + policyWord, "");
                }
            }

            text = text.Replace("  ", " ");

            return text;
        }

        public virtual string[] PolicyWords
        {
            get
            {
                return new string[]{};
            }
        }

        private string[] _allPolicyWords;
        public virtual string[] AllPolicyWords
        {
            get
            {
                if (_allPolicyWords == null)
                {
                    var words = new List<string>() 
                    {
                        "гипермаркет", "супермаркет", "бегемот"
                    };

                    words.AddRange(PolicyWords);
                    _allPolicyWords = words.ToArray();
                }

                return _allPolicyWords;
            }
        }

    }
}