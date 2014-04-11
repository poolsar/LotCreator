using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Web;
using System.Text;
using HtmlAgilityPack;
using ShopDataLib;
using Utils;

namespace PrestaWinClient.Logic
{

    class TimyrPriceRow
    {
        public string Title { get; set; }
        public string Uri { get; set; }
        public decimal CurrentPrice { get; set; }

    }


    internal class TaimyrScrapper
    {
        private const string SupplierUri = "http://e-taimyr.ru/";
        private const string SupplierName = "ЭкоТаймыр";
        private const decimal SupplierDiscount = 10m;


        //public void ScrapeCatalog()
        //{
        //    DownloadPrice();
        //}



        public List<TimyrPriceRow> DownloadPrice()
        {
            string uri = SupplierUri + "catalog.php?price=show";
            var pageLoader = new PageLoader();

            HtmlResponse htmlResponse = pageLoader.RequestsHtml(uri);

            var timyrPriceRows = ParsePrice(htmlResponse.Html);

            return timyrPriceRows;
        }


        public void DownloadProducts()
        {
            try
            {
                WriteOutput("Загрузка запущена");

                var context = Context.Inst;

                var supplier = context.SupplierSet.FirstOrDefault(s => s.Title == SupplierName);
                if (supplier == null)
                {
                    supplier = new Supplier();
                    supplier.Title = "ЭкоТаймыр";
                    supplier.Uri = SupplierUri;
                    supplier.Discount = SupplierDiscount;

                    context.SupplierSet.Add(supplier);
                    context.SaveChanges();
                }

                WriteOutput("Загрузка прайса");

                var timyrPriceRows = DownloadPrice();

                WriteOutput("Прайса загружен");


                SupplierProduct product = null;

                //var tpRow = timyrPriceRows.First(r => r.Uri == "catalog.php?id=60");
                //var product = DownloadProduct(context, tpRow, supplier);
                //context.SupplierProductSet.Add(product);
                //context.SaveChanges();
                //context.SaveHistory();

                var count = timyrPriceRows.Count;
                int counter = 0;

                foreach (var pRow in timyrPriceRows)
                {
                    WriteOutput("Загрузка продукта {0}", pRow.Title);

                    product = DownloadProduct(context, pRow, supplier);
                    context.SaveHistory();

                    counter++;
                    var message = string.Format("Продукт загружен {0} из {1}", counter, count);
                    WriteOutput(message);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Action<string> Output { get; set; }

        void WriteOutput(string format, params object[] args)
        {
            var message = string.Format(format, args);
            WriteOutput(message);
        }

        private void WriteOutput(string message)
        {
            if (Output == null) return;

            Output(message);
        }

        private SupplierProduct DownloadProduct(ShopEntities context, TimyrPriceRow pRow, Supplier supplier)
        {
            var pageLoader = new PageLoader();
            string uri = SupplierUri + pRow.Uri;
            var response = pageLoader.RequestsHtml(uri);
            var product = ParseTaimyrProduct(context, pRow, response.Html, supplier);
            return product;
        }

        //public class TaimyrCategory
        //{
        //    public int Id { get; set; }
        //    public string Title { get; set; }
        //    public int ParentCategoryId { get; set; }

        //    public TaimyrCategory()
        //    {

        //        ParentCategoryId = -1;

        //        _allCategories.Add(this);
        //    }

        //    public TaimyrCategory Parent
        //    {
        //        get
        //        {
        //            var result = _allCategories.FirstOrDefault(c => c.Id == this.ParentCategoryId);
        //            return result;
        //        }
        //    }

        //    public List<TaimyrCategory> Childs
        //    {
        //        get
        //        {
        //            var result = _allCategories.Where(c => c.ParentCategoryId == this.Id).ToList();
        //            return result;
        //        }
        //    }

        //    public string Uri { get; set; }

        //    static List<TaimyrCategory> _allCategories = new List<TaimyrCategory>();
        //}



        //private T IfModified<T>(SupplierProduct prod, T oval, T nval)
        //{
        //    var modified = !oval.Equals(nval);

        //    if (modified && prod.Status == ScrapeStatus.Stable)
        //    {
        //        prod.Status = ScrapeStatus.Modified;
        //        return nval;
        //    }

        //    return oval;
        //}



        private SupplierProduct ParseTaimyrProduct(ShopEntities context, TimyrPriceRow pRow, string html, Supplier supplier)
        {
            var doc = new HtmlDocument();

            doc.LoadHtml(html);


            var idOnSource = pRow.Uri.Split(new[] { "id=" }, StringSplitOptions.RemoveEmptyEntries)[1];

            var product = supplier.Products.FirstOrDefault(p => p.IdOnSource == idOnSource);

            if (product == null)
            {
                product = new SupplierProduct();

                context.SupplierProductSet.Add(product);

                product.Set("IdOnSource", idOnSource);
                product.Set("Supplier", supplier);
            }


            product.Set("Title", pRow.Title);
            product.Set("UriOnSource", pRow.Uri);


            var infoDiv = doc.DocumentNode.NodeByXpath("/html/body/div[3]/div[2]");


            //------------------------------------------------
            // категории
            //
            var categoryLinks = infoDiv.NodeByXpath("p[1]").SelectNodes("a");

            SupplierCategory parentCategory = null;
            foreach (var categoryLink in categoryLinks)
            {
                var tCatHref = categoryLink.GetAttributeValue("href", null);

                if (!tCatHref.Contains("?sid=")) continue;

                var sid = tCatHref.Split(new[] { "sid=" }, StringSplitOptions.RemoveEmptyEntries)[1];

                var tCat = supplier.Categories.FirstOrDefault(c => c.IdOnSource == sid);

                if (tCat == null)
                {
                    tCat = new SupplierCategory();
                    tCat.Set("IdOnSource", sid);
                    tCat.Set("Supplier", supplier);
                }

                tCat.Set("Title", categoryLink.InnerText);
                tCat.Set("UriOnSource", tCatHref);

                if (parentCategory != null)
                {
                    tCat.Set("Parent", parentCategory);
                }

                parentCategory = tCat;
            }

            product.Set("Category", parentCategory);


            //------------------------------------------------
            // цена без акции (если есть такая цена - значит текущая цена по акции)
            //

            var priceB = infoDiv.PriceByXpath("p[3]/font[2]/s", "руб.");
            if (priceB != null)
            {
                product.Set("IsSale", true);
            }

            var discountPrice = pRow.CurrentPrice;
            var price = priceB ?? discountPrice;
            var costPrice = discountPrice * (100m - supplier.Discount) / 100m;

            product.Set("Price", price);
            product.Set("DiscountPrice", pRow.CurrentPrice);
            product.Set("CostPrice", costPrice);


            //--------------------------------------------------
            // описание
            //
            var descriptionPs = infoDiv.SelectNodes("p").Skip(3).ToList();

            StringBuilder sbDescription = new StringBuilder();

            foreach (var descriptionP in descriptionPs)
            {
                sbDescription.AppendLine(descriptionP.OuterHtml);
            }

            product.Set("Description", sbDescription.ToString());

            context.SaveChanges();


            var pageLoader = new PageLoader();
            var imageTd = infoDiv.NodeByXpath("div[1]/table/tr/td");
            var imageLinks = imageTd.SelectNodes("a");

            for (int i = 0; i < imageLinks.Count; i++)
            {
                var imageLink = imageLinks[i];
                var imageUri = imageLink.GetAttributeValue("href", null);
                if (imageUri == null) continue;

                //imageUri = "plugins/resize.php?f=../uploads/ct60-0.jpg&amp;w=800";

                var imageName = imageUri.Split(new[] { "plugins/resize.php?f=../uploads/", "&" }, StringSplitOptions.RemoveEmptyEntries)[0];
                imageUri += "&s=0"; // убираем логотип


                //var ext = new[] {".jpg", "png"}.FirstOrDefault(e => imageUri.Contains(e));

                var sourceUri = SupplierUri + imageUri;
                var savePath = string.Format("img/taimyr/{0}/{1}", product.IdOnSource, imageName);



                var image = product.Images.FirstOrDefault(im => im.LocalPath == savePath);
                if (image == null)
                {
                    image = new Image();

                    image.Set("UriOnSupplier", imageUri);
                    image.Set("LocalPath", savePath);
                    FileSystemUtils.GetFolder(savePath);

                    if (!File.Exists(savePath))
                    {
                        pageLoader.RequestImage(sourceUri, savePath);
                    }
                }

                if (product.DefaultImage == null) product.DefaultImage = image;
                product.Images.Add(image);
            }

            context.SaveChanges();

            return product;
        }

        //private void PrepareFolder(string savePath)
        //{
        //    var folders = savePath.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

        //    string currentDirectory = Directory.GetCurrentDirectory();

        //    DirectoryInfo parentDirectory = new DirectoryInfo(currentDirectory);
        //    for (int i = 0; i < folders.Length - 1; i++)
        //    {
        //        var folder = folders[i];
        //        var sub = parentDirectory.EnumerateDirectories().FirstOrDefault(d => d.Name.ToLower() == folder.ToLower());
        //        parentDirectory = sub ?? parentDirectory.CreateSubdirectory(folder);
        //    }
        //}


        private List<TimyrPriceRow> ParsePrice(string priceHtml)
        {
            List<TimyrPriceRow> result = new List<TimyrPriceRow>();

            var doc = new HtmlDocument();

            doc.LoadHtml(priceHtml);

            HtmlNode table = doc.DocumentNode.NodeByXpath("/html/body/table");
            var trNodes = table.SelectNodes("tr");

            foreach (var tr in trNodes)
            {
                var a = tr.NodeByXpath("td[1]/a");

                if (a == null) continue;
                if (string.IsNullOrWhiteSpace(a.InnerText)) continue;

                TimyrPriceRow pRow = new TimyrPriceRow();

                pRow.Title = a.InnerText;
                pRow.Uri = a.GetAttributeValue("href", "");

                result.Add(pRow);

                var priceText = tr.TextByXpath("td[2]");
                if (string.IsNullOrWhiteSpace(priceText)) continue;

                decimal price;
                priceText = priceText.Replace("руб.", "").Trim();
                var parsed = decimal.TryParse(priceText, out price);
                if (!parsed) continue;

                pRow.CurrentPrice = price;
            }




            return result;


        }
    }
}