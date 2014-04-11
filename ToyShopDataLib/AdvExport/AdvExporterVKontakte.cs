using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace ToyShopDataLib.AdvExport
{
    public class AdvExporterVKontakte:AdvExporter
    {
        public override Uri BaseUri
        {
            get { return new Uri("http://vk.com"); }
        }
        
        //public void Sync(List<Product> products)
        //{
        //    var products = products.OrderBy(p =>
        //    {
        //        var style = p.GetSpecialStyle();
        //        var sTitle = style == null ? string.Empty : style.Title;
        //        return sTitle;
        //    }).ThenByDescending(p => p.Price).ToList();


        //    string rootPath = "c:/mytemp/ImagesVKontakte";
        //    CreateDirIfNotExist(rootPath);

        //    string emprtyStyleFolderName = "Other";

        //    ProccessMesenger.Write("Подготовка картинок для контакта");

        //    int count = products.Count;
        //    int counter = 0;
        //    foreach (var row in catalogRows)
        //    {
        //        counter++;
        //        ProccessMesenger.Write("Подготовка картинок для контакта {0} из {1}", counter, count);


        //        string stylePath = row.Style.IsEmpty() ? emprtyStyleFolderName : row.Style;
        //        string dirPath = string.Format("{0}/{1}", rootPath, stylePath);
        //        CreateDirIfNotExist(dirPath);

        //        var product = row.GetProduct();

        //        var sourcePath = product.GetImagePath();
        //        var destPath = string.Format("{0}/{1}", dirPath, new FileInfo(sourcePath).Name);

        //        File.Copy(sourcePath, destPath, true);
        //    }
        //}

        private static void CreateDirIfNotExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}