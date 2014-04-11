using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using ShopDataLib;
using Utils;

namespace PrestaWinClient.Logic
{


    internal class Scrapper24AuRu
    {
        public const string BaseUri = "http://krsk.24au.ru/";


        public const string BaseName = "24au.ru";
        public const decimal SupplierDiscount = 0m;

        public static bool CheckUri(string uri)
        {
            var contains = uri.Contains(BaseUri);
            return contains;
        }

        public SupplierProduct ScraperProduct(string uri)
        {
            var pageLoader = new PageLoader();
            var response = pageLoader.RequestsHtml(uri, decompress: true);
            var product = Parse(uri, response.Html);

            return product;
        }

        private SupplierProduct Parse(string uri, string html)
        {
            var context = Context.Inst;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            Supplier supplier = ParseSupplier(doc);
            SupplierCategory сategory = ParseCategory(doc, supplier);
            SupplierProduct product = ParseProduct(doc, uri, supplier, сategory);
            ParseImages(doc, product, context);

            return product;
        }

        private static Supplier ParseSupplier(HtmlDocument doc)
        {
            // Поставщик
            var userLink = doc.DocumentNode.NodeByXpath("//*[@id=\"card_user_form\"]/div/div/div[1]/div/a[1]");
            var supplierName = userLink.InnerText;
            var supplierUri = userLink.GetAttributeValue("href", null);

            Supplier supplier = Context.Inst.SupplierSet.FirstOrDefault(sup => sup.Uri == supplierUri);
            if (supplier == null)
            {
                supplier = new Supplier();
                supplier.Title = string.Format("{0} ({1})", supplierName, BaseName);
                supplier.Uri = supplierUri;
            }
            return supplier;
        }

        private static SupplierCategory ParseCategory(HtmlDocument doc, Supplier supplier)
        {
            SupplierCategory parentCategory = null;

            var categoryLinks = doc.DocumentNode.NodeByXpath("//*[@id=\"lot-content-col\"]/div[1]/div").SelectNodes("a");

            string catalogRooUri = "http://krsk.24au.ru/auction/";
            foreach (var categoryLink in categoryLinks)
            {
                var tCatHref = categoryLink.GetAttributeValue("href", null);

                // проверка, что ссылка на категорию
                if (tCatHref == null) continue;
                if (tCatHref == catalogRooUri) continue;
                if (!tCatHref.Contains(catalogRooUri)) continue;

                var catUrlParts = tCatHref.Split(new[] { catalogRooUri, @"/", @"\" }, StringSplitOptions.RemoveEmptyEntries);
                if (catUrlParts.Count() == 0) continue;

                // парсим

                var tCat = supplier.Categories.FirstOrDefault(c => c.UriOnSource == tCatHref);
                if (tCat == null)
                {
                    tCat = new SupplierCategory();
                    tCat.Set("UriOnSource", tCatHref);
                    tCat.Set("Supplier", supplier);
                }

                tCat.Set("Title", categoryLink.InnerText);


                if (parentCategory != null)
                {
                    tCat.Set("Parent", parentCategory);
                }

                parentCategory = tCat;
            }
            return parentCategory;
        }

        private static SupplierProduct ParseProduct(HtmlDocument doc, string uri, Supplier supplier, SupplierCategory сategory)
        {
            var idOnSource = uri.Split(new[] { BaseUri, @"/", @"\" }, StringSplitOptions.RemoveEmptyEntries)[0];

            SupplierProduct product = supplier.Products.FirstOrDefault(p => p.IdOnSource == idOnSource);

            if (product == null)
            {
                product = new SupplierProduct();

                Context.Inst.SupplierProductSet.Add(product);

                product.Set("IdOnSource", idOnSource);
                product.Set("UriOnSource", uri);

                product.Set("Supplier", supplier);
                product.Set("Category", сategory);
            }

            string title = doc.DocumentNode.TextByXpath("//*[@id=\"lot-postinfo-cnt\"]");
            product.Set("Title", title);


            var price = doc.DocumentNode.PriceByXpath("//*[@id=\"bid_form\"]/div/div/div[1]/div[1]/span[2]", "");
            product.Set("Price", price);
            product.Set("DiscountPrice", price);
            product.Set("CostPrice", price);


            var description = doc.DocumentNode.NodeByXpath("//*[@id=\"lot-content-col\"]/div[5]/div").InnerHtml;
            description = description.Replace("\r\n", "").Replace("\n\r", "").Trim();
            product.Set("Description", description);


            Context.Save();
            return product;
        }

        private static void ParseImages(HtmlDocument doc, SupplierProduct product, ShopEntities context)
        {

            List<HtmlNode> imageLinks = new List<HtmlNode>();

            var mainImageLink = doc.DocumentNode.NodeByXpath("//*[@id=\"Gallery\"]/div/a");
            imageLinks.Add(mainImageLink);
            
            var otherImagesDiv = doc.DocumentNode.NodeByXpath("//*[@id=\"Gallery\"]/div[2]");
            if (otherImagesDiv!=null)
            {
                var otherImagesLinks = otherImagesDiv.SelectNodes("a");
                imageLinks.AddRange(otherImagesLinks);
            }
            
            
            var imageUris = imageLinks.Select(l => l.GetAttributeValue("href", "")).ToList();

            string baseImageUri = "http://media2.24aul.ru/imgs/";
            var pageLoader = new PageLoader();

            foreach (string imageUri in imageUris)
            {
                var imageName = imageUri.Split(new[] { baseImageUri }, StringSplitOptions.RemoveEmptyEntries)[0];
                var saveFolder = string.Format("img/24auru/{0}", product.IdOnSource);
                var savePath = string.Format("{0}/{1}.jpg", saveFolder, imageName);

                var image = product.Images.FirstOrDefault(im => im.LocalPath == savePath);
                if (image == null)
                {
                    image = new Image();

                    image.Set("UriOnSupplier", imageUri);
                    image.Set("LocalPath", savePath);
                    FileSystemUtils.GetFolder(saveFolder);

                    if (!File.Exists(savePath))
                    {
                        pageLoader.RequestImage(imageUri, savePath);
                    }
                }

                if (product.DefaultImage == null) product.DefaultImage = image;
                product.Images.Add(image);
            }

            context.SaveChanges();
        }
    }
}