using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using ToyShopDataLib.Logic;
using Utils;


namespace ToyShopDataLib
{
    public partial class BegemotProduct
    {
        public static string NoDescription = "no_desc";
        public static string NoImagePath = "no_image";



        public bool HasImage()
        {
            var imagePath = GetImagePath();
            var has = imagePath != NoImagePath;

            return has;
        }

        public string GetImagePath()
        {
            if (ImagePath == NoImagePath)
            {
                return NoImagePath;
            }

            if (string.IsNullOrWhiteSpace(ImagePath))
            {
                ImagePath = GenerateImagePath(Article);
            }

            bool needDownload = !File.Exists(ImagePath);

            if (needDownload)
            {
                var scraper = new BegemotProductScraper();
                scraper.DownloadImage(this);
            }

            return ImagePath;
        }

        public string DownloadImage(Uri imageUri)
        {
            var scraper = new BegemotProductScraper();
            scraper.DownloadImage(this, imageUri);
            return ImagePath;
        }

        public string GenerateImagePath()
        {
            var imagePath = GenerateImagePath(Article);
            return imagePath;
        }

        public static string GenerateImagePath(string Article)
        {
            var imagePath = string.Format("{0}/{1}.jpg", Context.ImageSourceFolder, Article);
            return imagePath;
        }

        public string GetDescription()
        {
            if (string.IsNullOrWhiteSpace(Descrption))
            {
                var descHashPath = GetDescHashPath();
                bool exists = File.Exists(descHashPath);

                if (exists)
                {
                    ReadFromHash(descHashPath);
                }
                else
                {
                    ScrapeInfoFromSite();
                }
            }

            return Descrption;
        }

        private string GetDescHashPath()
        {
            string descHashPath = string.Format("{0}/{1}.txt", Context.ImageSourceFolder, Article);
            return descHashPath;
        }

        public void ScrapeInfoFromSite()
        {
            var scraper = new BegemotProductScraper();
            scraper.DownloadDescription(this);

            string descHashPath = GetDescHashPath();
            WriteHash(descHashPath);
        }

        private void WriteHash(string path)
        {
            File.WriteAllText(path, Descrption);
        }

        private void ReadFromHash(string hashDesc)
        {
            Descrption = File.ReadAllText(hashDesc);
        }

        public void SetNoImage()
        {
            ImagePath = NoImagePath;
        }

        public void SetNoDescrption()
        {
            Descrption = NoDescription;
        }

        public bool IsNoDescrption()
        {
            bool no = Descrption == NoDescription;
            return no;
        }

        public bool IsNoImage()
        {
            bool no = ImagePath == NoImagePath;
            return no;
        }

        public string GetClearTitle()
        {
            string clearTitle = TitleAlgoritm.Process(Title);
            return clearTitle;
        }

        public string GetClearShortTitle()
        {
            var titleShort = GetTitleShort();
            string clearTitle = TitleAlgoritm.Process(titleShort);
            return clearTitle;
        }


        public string GetClearDescrption()
        {
            string clearDescrption = IsNoDescrption() ? string.Empty : Descrption;
            return clearDescrption;
        }

        public string GetTitleShort()
        {
            var titleShort = Title.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)[0];
            return titleShort;
        }

        public BegemotSalePrice GetActiveSale()
        {
            var now = DateTime.Now;
            var result = BegemotSalePrice.LastOrDefault(
                s => s.BegemotSale.Active && s.BegemotSale.DateStart <= now && now < s.BegemotSale.DateStop);
            return result;
        }
    }

    public class BegemotProductScraper
    {
        public static string DescriptionUrlFormat = "http://www.begemott.ru/tov_{0}.html";
        public static string ImageUrlFormat = "http://www.begemott.ru/photos/{0}.jpg";

        public void DownloadDescription(BegemotProduct product)
        {
            PageLoader pageLoader = new PageLoader();

            string uri = string.Format(DescriptionUrlFormat, product.Code);

            try
            {
                // uri = "http://krsk.24au.ru/2365348/";
                var response = pageLoader.RequestsHtml(uri, decompress: true);
                var doc = new HtmlDocument();
                doc.LoadHtml(response.Html);


                var titleHeader =
                       doc.DocumentNode.NodeByXpath("/html/body/table/tr/td/table[1]/tr/td/table/tr/td[4]/div[2]/h1");

                product.Title = titleHeader.InnerText.Trim();


                bool contains = doc.DocumentNode.InnerHtml.Contains("<b>Описание:</b>");
                if (contains)
                {
                    var descHeader =
                        doc.DocumentNode.NodeByXpath(
                            "/html/body/table/tr/td/table[1]/tr/td/table/tr/td[4]/div[2]/div[2]/b");

                    var nodes = descHeader.ParentNode.ChildNodes;
                    var indexHeader = nodes.IndexOf(descHeader);
                    var descNode = nodes[indexHeader + 1];
                    var innerText = descNode.InnerText.Trim();
                    product.Descrption = innerText;


                }
                else
                {
                    product.SetNoDescrption();
                }
            }
            catch (WebException we)
            {
                product.SetNoDescrption();
            }
        }

        //public static string Convert(this Encoding sourceEncoding, Encoding targetEncoding, string value)
        //{
        //    string reEncodedString = null;

        //    byte[] sourceBytes = sourceEncoding.GetBytes(value);
        //    byte[] targetBytes = Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);
        //    reEncodedString = sourceEncoding.GetString(targetBytes);

        //    return reEncodedString;
        //}


        public void DownloadImage(BegemotProduct bproduct,  Uri imageUri = null)
        {
            PageLoader pageLoader = new PageLoader();

            string uri = null;

            if (imageUri == null)
            {
                uri = string.Format(ImageUrlFormat, bproduct.Article);
            }
            else
            {
                uri = imageUri.ToString();
            }

            string savePath = bproduct.GenerateImagePath();
            try
            {
                pageLoader.RequestImage(uri, savePath);
                bproduct.ImagePath = savePath;
            }
            catch (WebException we)
            {
                bproduct.SetNoImage();
            }
        }

    }
}