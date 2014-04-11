using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using RestSharp.Contrib;
using ToyShopDataLib.Utils;

namespace ToyShopDataLib
{
    public partial class Style
    {
        public MarketCategory DefMarketCategoryObj
        {
            get
            {
                return MarketCategory.Get(DefMrkCategory);
            }
            set
            {
                DefMrkCategory = value == null ? 0 : value.Value;
            }
        }


        public StyleType StyleType
        {
            get { return (StyleType)Type; }
            set { Type = (int)value; }
        }

        public void Apply(Product product, BegemotProduct bproduct)
        {
            var needAdd = !product.SpecialStyle.Any(s => s.Id == Id);
            if (needAdd)
            {
                product.SpecialStyle.Add(this);
            }

            product.ReApplyStyle();
        }

        public static Style GetDefault()
        {
            var style = new Style();

            style.Title = StyleTag.TitleClear.InBrakets();
            style.Header = StyleTag.TitleClear.InBrakets();
            style.Content = StyleTag.Description.InBrakets();
            style.Footer = string.Empty;

            return style;
        }

        public string GetCode(bool tagMode = false)
        {
            var code = ProductCode.GetCode(this, tagMode);
            return code;
        }
    }


    public class StyleTag
    {
        public const string Title = "Title";
        public const string TitleShort = "TitleShort";
        public const string TitleClear = "TitleClear";
        public const string TitleClearShort = "TitleClearShort";
        public const string Description = "Description";
        public const string Price = "Price";
        public const string PriceOld = "PriceOld";

        /// <summary>
        /// ћожно просто указать в любой части и тег заменитьс€ на предыдущее значение части
        /// </summary>
        public const string Last = "Last";

        /// <summary>
        /// Ќапример "{Spec.Title}" и тег заменитс€ на указанную часть указанного типа стил€
        /// </summary>
        public const string PrefixSpec = "Spec";

        /// <summary>
        /// Ќапример "{Sale.Title}" и тег заменитс€ на указанную часть указанного типа стил€
        /// </summary>
        public const string PrefixSale = "Sale";

        /// <summary>
        /// Ќапример "{Adv.Title}" и тег заменитс€ на указанную часть указанного типа стил€
        /// </summary>
        public const string PrefixAdv = "Adv";


        public const string CodeProduct = "CodeProduct";

        public const string CodeStyle = "CodeStyle";

        public const string CodeSale = "CodeSale";

        public const string Sales = "Sales";

        public const string Menu = "Menu";

        public static string GetPrefix(StyleType styleType)
        {
            switch (styleType)
            {
                case StyleType.SpecialDescrition:
                    return PrefixSpec;
                    break;
                case StyleType.Sale:
                    return PrefixSale;
                    break;
                case StyleType.MarketPlace:
                    return PrefixAdv;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum StyleType
    {
        SpecialDescrition = 0,
        Sale = 1,
        MarketPlace = 2
    }

    public static class StyleTypeExts
    {
        public static string String(this StyleType styleType)
        {
            switch (styleType)
            {
                case StyleType.SpecialDescrition:
                    return "—пец описание";
                    break;
                case StyleType.Sale:
                    return "—кидка";
                    break;
                case StyleType.MarketPlace:
                    return "“оргова€ площадка";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }



    internal class MultiStyler
    {
        private Dictionary<string, string> valueDict;


        public void ApplyStyles(Product product, BegemotProduct bproduct, Adv24au adv = null)
        {
            Marketplace market = adv.Marketplace;

            var specialStyle = product.GetSpecialStyle();
            var saleStyle = product.GetSaleStyle();
            var marketStyle = market == null ? null : market.GetActiveStyle();

            ApplyStyles(product, bproduct, specialStyle, saleStyle, marketStyle, market, adv);
        }

        public void ApplyStyles(Product product, BegemotProduct bproduct, Style specialStyle, Style saleStyle, Style marketStyle, Marketplace market, Adv24au adv)
        {
            //bool isNoStyles = specialStyle == null && saleStyle == null && marketStyle == null;

            var activeSale = product.GetActiveSale();

            Style defultStyle = Style.GetDefault();

            valueDict = new Dictionary<string, string>();
            AddToDict(product, bproduct, specialStyle, activeSale);

            AppleResult ar = null;
            ar = Apply(defultStyle, StyleType.SpecialDescrition);
            ar = Apply(specialStyle, StyleType.SpecialDescrition);
            ar = Apply(saleStyle, StyleType.Sale);
            ar = Apply(marketStyle, StyleType.MarketPlace);

            ar.ClearTags();
            //ar.ClearPolicyWords(market);

            var title = ar.GetResultTitle();
            var description = ar.GetResultDescription();
            
            if (adv == null)
            {
                product.Title = title;
                product.Description = description;
            }
            else
            {
                title = market.PrepareTitle(title);
                description = market.PrepareDescription(description);

                adv.Title = title;
                adv.Description = description;
            }
        }

        private void AddToDict(Product product, BegemotProduct bproduct, Style specialStyle, Sale activeSale)
        {
            valueDict.Add(StyleTag.Title, bproduct.GetClearTitle());
            valueDict.Add(StyleTag.TitleShort, bproduct.GetTitleShort());
            valueDict.Add(StyleTag.TitleClear, bproduct.GetClearTitle());
            valueDict.Add(StyleTag.TitleClearShort, bproduct.GetClearShortTitle());
            valueDict.Add(StyleTag.Description, bproduct.GetClearDescrption());
            valueDict.Add(StyleTag.Price, product.Price.ToString("f2"));
            valueDict.Add(StyleTag.PriceOld, product.PriceOld.ToString("f2"));

            var codeProduct = product.GetCode();
            valueDict.Add(StyleTag.CodeProduct, codeProduct);

            if (specialStyle != null)
            {
                var codeStyle = specialStyle.GetCode(true);
                //codeStyle = HttpUtility.UrlEncode(codeStyle);
                valueDict.Add(StyleTag.CodeStyle, codeStyle);
            }

            if (activeSale != null)
            {
                var codeSale = activeSale.GetCode(true);
                // codeSale = HttpUtility.UrlEncode(codeSale);
                valueDict.Add(StyleTag.CodeSale, codeSale);
            }



            var salesLinks = SalesLinks();
            valueDict.Add(StyleTag.Sales, salesLinks);


            var menuLinks = MenuLinks();
            valueDict.Add(StyleTag.Menu, menuLinks);

        }

        private string SalesLinks()
        {
            var activeSales = Sale.GetActiveSales();

            StringBuilder sb = new StringBuilder();

            bool first = true;
            foreach (Sale sale in activeSales)
            {
                if (!first)
                {
                    sb.AppendLine();
                }
                first = false;
                var codeSale = sale.GetCode(true);
                sb.AppendFormat("{0} [URL=http://krsk.24au.ru/auction/?find_str={1}]здесь[/URL]!", sale.Description, codeSale);
            }

            var result = sb.ToString();
            return result;
        }

        private string MenuLinks()
        {
            var activeStyle = (from p in Context.Inst.ProductSet
                               where p.SpecialStyle.Any()
                               from s in p.SpecialStyle
                               where s.Id != 4
                               select s).Distinct().OrderBy(s => s.DefMrkCategory).ThenBy(s => s.Name).ToList();


            StringBuilder sb = new StringBuilder();

            bool first = true;
            foreach (Style style in activeStyle)
            {
                if (!first)
                {
                    sb.AppendLine();
                }
                first = false;
                var codeStyle = style.GetCode(true);
                sb.AppendFormat("[URL=http://krsk.24au.ru/auction/?find_str={1}]{0}[/URL]", style.Name, codeStyle);
            }

            var result = sb.ToString();
            return result;
        }


        private AppleResult _lastAppleRes;
        private AppleResult Apply(Style style, StyleType styleType)
        {
            if (style == null) return _lastAppleRes;

            if (_lastAppleRes == null) _lastAppleRes = new AppleResult();

            AppleResult appleResultSpec = new AppleResult();

            appleResultSpec.Type = styleType;
            appleResultSpec.Title = Apply(style.Title, _lastAppleRes.Title);
            appleResultSpec.Header = Apply(style.Header, _lastAppleRes.Header);
            appleResultSpec.Content = Apply(style.Content, _lastAppleRes.Content);
            appleResultSpec.Footer = Apply(style.Footer, _lastAppleRes.Footer);

            SaveToDict(appleResultSpec);

            _lastAppleRes = appleResultSpec;

            return appleResultSpec;
        }

        private void SaveToDict(AppleResult appleResult)
        {
            var keyTypes = Enum.GetValues(typeof(KeyType)).Cast<KeyType>().ToList();

            foreach (var keyType in keyTypes)
            {
                var key = appleResult.GetKey(keyType);
                //var keyLast = appleResult.GetKeyAsLast(keyType);

                var value = appleResult.GetValue(keyType);

                SaveToDict(key, value);
                //SaveToDict(keyLast, value);
            }
        }

        private void SaveToDict(string key, string value)
        {
            var added = valueDict.ContainsKey(key);
            if (added)
            {
                valueDict[key] = value;
            }
            else
            {
                valueDict.Add(key, value);
            }
        }

        private string Apply(string stylePart, string lastValue)
        {
            if (stylePart.IsEmpty()) return lastValue;

            foreach (string key in valueDict.Keys)
            {
                var value = valueDict[key];
                stylePart = ReplaceKey(stylePart, key, value);
            }

            stylePart = ReplaceKey(stylePart, StyleTag.Last, lastValue);

            return stylePart;
        }

        private string ReplaceKey(string stylePart, string key, string value)
        {
            string resKey;
            resKey = key.InBrakets();

            if (stylePart.Contains(resKey))
            {
                stylePart = stylePart.Replace(resKey, value);
            }
            return stylePart;
        }

        private class AppleResult
        {
            public StyleType Type { get; set; }

            public string Title { get; set; }
            public string Header { get; set; }
            public string Content { get; set; }
            public string Footer { get; set; }

            public string GetPrefix()
            {
                string result = StyleTag.GetPrefix(Type);
                return result;


            }

            public string GetKey(KeyType keyType)
            {
                var prefix = GetPrefix();
                var postfix = GetPostfix(keyType);
                var key = string.Format("{0}.{1}", prefix, postfix);
                return key;
            }

            //public string GetKeyAsLast(KeyType keyType)
            //{
            //    var postfix = GetPostfix(keyType);
            //    var key = string.Format("Last.{0}", postfix);
            //    return key;
            //}

            string GetPostfix(KeyType keyType)
            {
                switch (keyType)
                {
                    case KeyType.Title:
                        return "Title";
                        break;
                    case KeyType.Header:
                        return "Header";
                        break;
                    case KeyType.Content:
                        return "Content";
                        break;
                    case KeyType.Footer:
                        return "Footer";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("keyType");
                }
            }

            public string GetValue(KeyType keyType)
            {
                switch (keyType)
                {
                    case KeyType.Title:
                        return Title;
                        break;
                    case KeyType.Header:
                        return Header;
                        break;
                    case KeyType.Content:
                        return Content;
                        break;
                    case KeyType.Footer:
                        return Footer;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("keyType");
                }
            }
            /// <summary>
            /// ≈сли остались незамененные теги, мы их чистим
            /// </summary>
            public void ClearTags()
            {
                Title = ClearTags(Title);
                Header = ClearTags(Header);
                Content = ClearTags(Content);
                Footer = ClearTags(Footer);
            }

            private string ClearTags(string part)
            {
                if (part.IsEmpty()) return part;

                var nl = Environment.NewLine;
                var emp = string.Empty;

                Regex regex = new Regex(@"\{([^}]*)\}");


                foreach (Match match in regex.Matches(part))
                {
                    var tag = match.Groups[1].Value.InBrakets();

                    part = part.Replace(nl + tag, emp);
                    part = part.Replace(tag + nl, emp);
                    part = part.Replace(tag, emp);

                }

                return part;
            }

            public void ClearEmptyLines()
            {
                Title = ClearEmptyLines(Title);
                Header = ClearEmptyLines(Header);
                Content = ClearEmptyLines(Content);
                Footer = ClearEmptyLines(Footer);
            }

            public string ClearEmptyLines(string part)
            {
                string[] lines = part.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                StringBuilder sb = new StringBuilder();

                int emptyLinesCount = 0;
                bool firstLine = true;
                foreach (string line in lines)
                {
                    if (line.IsEmpty())
                    {
                        emptyLinesCount++;
                        continue;

                    }
                    if (!firstLine)
                    {
                        sb.AppendLine();
                        if (emptyLinesCount > 0) sb.AppendLine();
                    }

                    sb.Append(line);

                    firstLine = false;
                    emptyLinesCount = 0;
                }

                string result = sb.ToString();

                return result;
            }

            public string GetResultTitle()
            {
                return Title;
            }

            public string GetResultDescription()
            {
                string[] parts = { Header, Content, Footer };

                bool firstLine = true;
                var sb = new StringBuilder();
                foreach (string part in parts)
                {
                    if (part.IsEmpty()) continue;

                    if (!firstLine)
                    {
                        sb.AppendLine();
                        sb.AppendLine();
                    }

                    var clpart = ClearEmptyLines(part);

                    sb.AppendLine(clpart);

                    firstLine = false;
                }

                var description = sb.ToString();

                description = ClearEmptyLines(description);

                return description;
            }

            //public void ClearPolicyWords(Marketplace market)
            //{
            //    if (market == null) return;

            //    Title = ClearPolicyWords(Title, market.PolicyWords);
            //    Header = ClearPolicyWords(Header, market.PolicyWords);
            //    Content = ClearPolicyWords(Content, market.PolicyWords);
            //    Footer = ClearPolicyWords(Footer, market.PolicyWords);
            //}

            //private string ClearPolicyWords(string text, string[] policyWords)
            //{
            //    foreach (string policyWord in policyWords)
            //    {
            //        var isRegex = policyWord.Contains("regex");

            //        if (isRegex)
            //        {
            //            var regex = policyWord.Replace("regex", "");
            //            text = text.CleatByRegex(regex);
            //        }
            //        else
            //        {
            //            text = text.Replace(policyWord + " ", "");
            //            text = text.Replace(" " + policyWord, "");
            //        }
            //    }

            //    return text;
            //}
        }

        enum KeyType
        {
            Title,
            Header,
            Content,
            Footer
        }
    }
}