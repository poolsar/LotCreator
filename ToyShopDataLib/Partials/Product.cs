using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ImageMakerWpf;
using ToyShopDataLib.Utils;

namespace ToyShopDataLib
{
    public partial class Product
    {


        public string GetImagePath()
        {
            var ImagePath = string.Format("{0}\\{1}.jpg", Context.ImageSaveFolder, Article);
            return ImagePath;
        }

        public static void UpdateProductActivation(List<string> selectedArticles, Action<string> progressReport = null)
        {
            // если параметр не указан, то ставим пустую заглушку, чтобы каждый раз не проверять на null
            if (progressReport == null) progressReport = s => { };

            progressReport("Обновляем активацию товаров");

            var activeArticles = Context.Inst.ProductSet.Where(p => p.Active).Select(p => p.Article).ToList();

            var activateProdArticles = selectedArticles.Except(activeArticles).ToList();
            var deactiveProdArticles = activeArticles.Except(selectedArticles).ToList();

            progressReport("Деактивируем товары");

            // деактивируем
            var productsToDeactivate =
                (from p in Context.Inst.ProductSet
                 from da in deactiveProdArticles
                 where p.Article == da
                 select p).ToList();
            productsToDeactivate.ForEach(p => p.Active = false);

            progressReport("Активируем товары");

            // активируем
            var productsToActivate =
                (from p in Context.Inst.ProductSet.ToList()
                 from aa in activateProdArticles.ToList()
                 where p.Article == aa
                 select p).ToList();
            productsToActivate.ForEach(p => p.Active = true);

            progressReport("Добавляем активированные товары");

            // добавляем
            var articlesToAdd = activateProdArticles.Except(productsToActivate.Select(p => p.Article)).ToList();

            var productsToAdd =
                (from bp in Context.Inst.BegemotProductSet.ToList()
                 from aa in articlesToAdd.ToList()
                 where bp.Article == aa
                 select bp).ToList();

            var count = productsToAdd.Count;
            var counter = 0;
            var batchCounter = 0;



            foreach (var begemotProduct in productsToAdd)
            {
                Product product = Product.FromBegemotProduct(begemotProduct);
                Context.Inst.ProductSet.Add(product);

                counter++;
                batchCounter++;
                //if (batchCounter >= 50)
                {
                    batchCounter = 0;

                    string message = string.Format("Добавлен активированный товар {0} из {1}", counter, count);
                    progressReport(message);
                }
            }

            progressReport("Сохраняем результаты");
            // сохраняем
            Context.Save();

            progressReport("Обновление активации товаров успешно завершено");
        }

        public static Product FromBegemotProduct(BegemotProduct begemotProduct)
        {
            var product = new Product();
            product.Article = begemotProduct.Article;
            product.Title = begemotProduct.GetClearTitle();

            product.Description = begemotProduct.GetDescription();
            product.Price = begemotProduct.RetailPrice;
            begemotProduct.GetImagePath();

            product.Category = ProductCategory.FromBegemotProduct(begemotProduct);

            product.Active = true;


            return product;
        }



        public void FreeImage()
        {
            if (_bitmap != null)
            {
                _bitmap.Dispose();
                _bitmap = null;
            }
        }


        private Bitmap _bitmap;

        public Bitmap GetImage(BegemotProduct bproduct)
        {
            if (_bitmap == null)
            {
                var path = GetImagePath();
                if (!File.Exists(path))
                {

                    path = bproduct.GetImagePath();
                }

                bool hasImage = path == BegemotProduct.NoImagePath;

                _bitmap = hasImage ? null : new Bitmap(path);
            }

            return _bitmap;
        }

        public decimal CalcMargin(BegemotProduct begemotProduct, BegemotSalePrice sale)
        {
            var selfPrice = PriceCalculator.CalcSelfPrice(begemotProduct, sale);

            var margin = Price - selfPrice;

            return margin;
        }



        public static List<PBS> GetPBS(ICollection<Product> products = null)
        {
            var prods = products != null ? products.AsQueryable() : Context.Inst.ProductSet.AsQueryable();

            var pbsList = (from p in prods
                           from bp in Context.Inst.BegemotProductSet
                           where p.Article == bp.Article
                           let s = Context.Inst.BegemotSalePriceSet.FirstOrDefault(bs =>
                               bs.BegemotSale.Active && bs.Article == p.Article)
                           select new PBS
                              {
                                  Product = p,
                                  BProduct = bp,
                                  BSale = s
                              }).ToList();
            return pbsList;
        }


        public Style GetSpecialStyle()
        {
            if (!SpecialStyle.Any()) return null;

            var style = SpecialStyle.Last(s => s.Active);
            return style;
        }

        public Style GetSaleStyle()
        {
            var activeSale = GetActiveSale();
            var saleStyle = activeSale != null ? activeSale.Style : null;
            return saleStyle;
        }

        public Style GetActiveStyle()
        {
            var style = GetSaleStyle() ?? GetSpecialStyle();
            return style;
        }

        public void ReApplyStyle()
        {
            new MultiStyler().ApplyStyles(this, BegemotProduct);
        }

        


        public Sale GetActiveSale()
        {
            if (!Sales.Any()) return null;

            var now = DateTime.Now;

            var sale = Sales.ToList().LastOrDefault(s => s.IsNowActive());
            return sale;
        }

        public void RecalcPrice(BegemotProduct bproduct, BegemotSalePrice bsale)
        {
            var sale = GetActiveSale();
            PriceCalculator.CalcPrice(this, bproduct, bsale, sale);
        }


        public void ResetActiveSales()
        {
            Sales.Clear();
        }


        public PricePosition PricePosition
        {
            get { return (PricePosition)ImgPricePosition; }
            set { ImgPricePosition = (int)value; }
        }


        public string GetCode()
        {
            string str = ProductCode.GetCode(this);
            return str;
        }


        


        public static void SyncProducts()
        {
            SyncProductsData();
            ProductImage.SyncImages();
        }

        private static void SyncProductsData()
        {
            ProccessMesenger.Write("Импорт. Синхронизация товаров. Вычисление измененных товаров");

            var pbsToSync = GetPBS().Where(pbs =>
                pbs.Product.BRetailPrice != pbs.BProduct.RetailPrice ||
                pbs.Product.BWholeSalePrice != pbs.BProduct.WholeSalePrice
                //||
                //pbs.Product.BCount != pbs.BProduct.Count 
                ).ToList();

            int count = pbsToSync.Count;
            int counter = 0;
            foreach (var pbs in pbsToSync)
            {
                counter++;
                ProccessMesenger.Write("Импорт. Синхронизация товара {0} из {1}", counter, count);

                pbs.Product.RecalcPrice(pbs.BProduct, pbs.BSale);
                pbs.Product.ReApplyStyle();

                pbs.Product.BRetailPrice = pbs.BProduct.RetailPrice;
                pbs.Product.BWholeSalePrice = pbs.BProduct.WholeSalePrice;
            }

            ProccessMesenger.Write("Импорт. Синхронизация товаров. Сохранение результатов");
            Context.Save();
        }

        public ShowCaseStyle GetShowCaseStyle()
        {
            var style = GetActiveStyle();

            var showCaseStyle = style != null ? (ShowCaseStyle)style.ShowCaseStyle : ShowCaseStyle.Standart;

            return showCaseStyle;
        }

        public bool IsAllowPost()
        {
            bool allow = !IsOutOfStock();
            return allow;
        }

        public bool IsOutOfStock()
        {
            var result = BegemotProduct.Count == 0;
            return result;
        }
    }

    public enum PricePosition
    {
        TopRight = 0,
        DownRight = 1
    }

    public class PBS
    {
        public Product Product { get; set; }
        public BegemotProduct BProduct { get; set; }
        public BegemotSalePrice BSale { get; set; }
    }


    public class PriceCalculator
    {
        public static decimal CalcPrice(Product product, BegemotProduct begemotProduct, BegemotSalePrice bsale, Sale sale)
        {
            decimal retPrice = CalcRetailPrice(begemotProduct, bsale);

            product.Price = retPrice;
            product.PriceOld = retPrice;

            if (sale != null)
            {
                decimal selfPrice = CalcSelfPrice(begemotProduct, bsale);
                retPrice = sale.CalcSalePrice(retPrice, selfPrice);
                product.Price = retPrice;
            }

            return retPrice;
        }

        private static decimal CalcRetailPrice(BegemotProduct begemotProduct, BegemotSalePrice bsale)
        {
            // розничная цена
            decimal bRetPrice = CalcBegemotRetailPrice(begemotProduct, bsale);
            var price = bRetPrice * Context.OldPriceCoefficient;
            return price;
        }

        private static decimal CalcBegemotRetailPrice(BegemotProduct begemotProduct, BegemotSalePrice sale)
        {
            decimal price;

            if (sale != null)
            {
                price = sale.RetailPriceOld;
            }
            else
            {
                price = begemotProduct.RetailPrice;
            }

            return price;
        }

        public static decimal CalcSelfPrice(BegemotProduct begemotProduct, BegemotSalePrice sale)
        {
            decimal price;

            if (sale != null)
            {
                price = sale.WholeSalePrice;
            }
            else
            {
                price = Context.WithBegemotDescount(begemotProduct.WholeSalePrice);
            }

            return price;
        }

        public static decimal CalcOldPrice(Product product, BegemotProduct begemotProduct, BegemotSalePrice sale)
        {
            decimal oldPrice;
            if (sale == null)
            {
                oldPrice = begemotProduct.RetailPrice * Context.OldPriceCoefficient;
            }
            else
            {
                oldPrice = sale.RetailPriceOld * Context.OldPriceCoefficient;
            }

            return oldPrice;
        }
    }


    internal class TitleAlgoritm
    {
        //public static string Process(string title)
        //{
        //    var words = title.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //    if (words.Count() < 2) return title;

        //    var firstWordWithDigits = words.FirstOrDefault(w =>
        //        w.ToList()
        //        .Intersect(new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', })
        //        .Any());

        //    if (firstWordWithDigits == null) return title;

        //    if (title.Contains(firstWordWithDigits + " "))
        //    {
        //        title = title.Replace(firstWordWithDigits + " ", "");
        //    }
        //    else
        //    {
        //        title = title.Replace(" " + firstWordWithDigits, "");
        //    }

        //    return title;
        //}

        static char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static int digitsInCode = 4;



        public static string Process(string title)
        {
            var words = title.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (words.Count() < 2) return title;

            // чаще всего код идет вторым словом. и если в этом слове есть хоть 1 цифра, значит код сто пудов
            if (words[1].Intersect(digits).Count() > 0)
            {
                title = title.Replace(" " + words[1] + " ", " ");
            }

            var wordWithDigits =
                (from word in words
                 where
                     (from chr in word
                      from d in digits
                      where chr == d
                      select chr)
                 .Count() >= digitsInCode
                 select word
                 ).ToList();

            if (wordWithDigits.Count == 0) return title;

            foreach (var wordWithDigit in wordWithDigits)
            {
                if (title.Contains(wordWithDigit + " "))
                {
                    title = title.Replace(wordWithDigit + " ", "");
                }
                else
                {
                    title = title.Replace(" " + wordWithDigit, "");
                }
            }
            return title;
        }
    }
}