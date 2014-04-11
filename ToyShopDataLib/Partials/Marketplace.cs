using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToyShopDataLib.AdvExport;

namespace ToyShopDataLib
{
    public partial class Marketplace
    {
        public Style GetActiveStyle()
        {
            if (!Styles.Any()) return null;
            var style = Styles.Last(s => s.Active);
            return style;
        }

        public void SetActiveStyle(Style style)
        {
            if (Styles.Any(s => s.Id == style.Id)) return;
            Styles.Clear();
            if (style == null) return;
            Styles.Add(style);
        }

        public Adv24au Apply(Product product)
        {
            //var adv = product.Advs.First(a => a.Product.Id == product.Id && a.Marketplace.Id == this.Id);

            var adv = product.Advs.FirstOrDefault(a => a.Marketplace.Id == this.Id);


            bool save = false;
            if (adv == null)
            {
                var category = GetCategory(product);
                adv = new Adv24au(product, category, this);

                product.Advs.Add(adv);

                save = true;
            }

            if (!adv.Active)
            {
                adv.Active = true;
                save = true;
            }

            if (save)
            {
                Context.Save();
            }

            return adv;
        }

        public Uri GetBaseUri()
        {
            var uri = new Uri(Url);
            return uri;
        }

        private AdvExporter GetAdvExporter()
        {
            var exporter = AdvExporter.GetExporter(this);
            return exporter;
        }

        private AdvPreparer GetAdvPreparer()
        {
            var exporter = GetAdvExporter();
            var preparer = exporter.GetPreparer();
            return preparer;
        }

        public decimal PreparePrice(decimal price)
        {
            var preparer = GetAdvPreparer();
            price = preparer.PreparePrice(price);
            return price;
        }

        public string PrepareTitle(string text)
        {
            var preparer = GetAdvPreparer();
            text = preparer.PrepareTitle(text);
            return text;
        }

        public string PrepareDescription(string text)
        {
            var preparer = GetAdvPreparer();
            text = preparer.PrepareDescription(text);
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

        //private string PrepareCamelCase(string title)
        //{
        //    var largeWords = title.Split(new[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();

        //    var sb = new StringBuilder();
        //    for (int i = 0; i < largeWords.Count; i++)
        //    {
        //        if (i > 0) sb.Append(" ");
        //        var largeWord = largeWords[i];

        //        if (largeWord.Length > 5)
        //        {
        //            var substring = largeWord.Substring(0, 6);
        //            bool manyApperCase = substring.ToUpper() == substring;

        //            if (manyApperCase)
        //            {
        //                largeWord = ToCamelCase(largeWord);
        //            }
        //        }

        //        // sb.Append(largeWord);
        //        title = title.Replace(largeWords[i], largeWord);
        //    }

        //    string result;
        //    //string result = sb.ToString();

        //    result = title;

        //    return result;
        //}

        //private string ToCamelCase(string largeWord)
        //{
        //    string result = largeWord[0] + largeWord.Substring(1).ToLower();
        //    return result;

        //}



        //public string[] PolicyWords = { "в ассортименте", "в ассорт", @"regex\d+\s?шт", "гипермаркет", "супермаркет", "бегемот" };

        public AdvCategory GetCategory(Product product)
        {
            var marketCategory = GetMarketCategory(product);
            var advCategory = GetAdvCategory(marketCategory);

            return advCategory;
        }

        private static MarketCategory GetMarketCategory(Product product)
        {
            MarketCategory result;
            var style = product.GetSpecialStyle();
            if (style == null)
            {
                result = MarketCategory.Default;
            }
            else
            {
                result = style.DefMarketCategoryObj;
            }
            return result;
        }

        private AdvCategory GetAdvCategory(MarketCategory marketCategory)
        {

            // поскольку это кастыль на время пока не категории не созданы вручную
            // пока ищем по заголовку
            AdvCategory cat = AdvCategory.FirstOrDefault(c => c.Title == marketCategory.Title);


            //AdvCategory cat = AdvCategory.FirstOrDefault(c => c.Number == marketCategory.Value);

            if (cat == null)
            {
                cat = new AdvCategory();
                cat.Title = marketCategory.Title;
                cat.Number = marketCategory.Value;
                cat.Marketplace = this;
                this.AdvCategory.Add(cat);
                Context.Save();
            }

            return cat;
        }

        public Adv24au GetAdv(Product product)
        {
            var adv = product.Advs.FirstOrDefault(a => a.Marketplace.Id == this.Id);

            if (adv == null)
            {
                adv = Apply(product);
            }

            return adv;
        }

        public void Sync(MarketplaceSyncMode mode = MarketplaceSyncMode.Published, List<Product> products = null)
        {
            if (mode == MarketplaceSyncMode.Selected && (products == null || products.Count == 0))
            {
                throw new ArgumentOutOfRangeException();
            }

            List<Adv24au> advs = null;

            if (mode == MarketplaceSyncMode.Selected)
            {
                advs = products.Select(GetAdv).ToList();
            }
            else if (mode == MarketplaceSyncMode.All)
            {
                advs = Context.Inst.ProductSet.Select(GetAdv).ToList();
            }

            var exporter = GetAdvExporter();
            exporter.Sync(advs);
        }

    }

    public enum MarketplaceSyncMode
    {
        Selected = 0,
        Published = 1,
        All = 2
    }
}