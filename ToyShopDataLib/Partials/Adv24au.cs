using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HtmlAgilityPack;
using ToyShopDataLib.Utils;
using Utils;

namespace ToyShopDataLib
{
    public partial class Adv24au
    {
        public const int DeafaultDays = 1;

        public MarketCategory MarketCategory
        {
            get
            {
                return MarketCategory.Get(MrkCategory);
            }
            set
            {
                MrkCategory = value == null ? 0 : value.Value;
            }
        }

        public Adv24au(Product product, AdvCategory category, Marketplace market)
        {
            Product = product;
            Category = category;
            Marketplace = market;

            Days = DeafaultDays;
            DateUpdate = DateTime.Now;
            DateExpire = DateTime.Now;

            Active = true;
        }



        public static int ParseCreateLotResponse(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);


            var numberSpan = doc.DocumentNode.NodeByXpath(
                "/html/body/div[1]/div[2]/div/div[1]/div[1]/div/span/span");

            var number = Convert.ToInt32(numberSpan.InnerText.Replace("№", ""));




            //var descHeader = doc.DocumentNode.NodeByXpath("/html/body/h/a");
            //var href = descHeader.GetAttributeValue("href", "");
            //var numberStr = href.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries).First();

            //var number = Convert.ToInt32(numberStr);

            return number;
        }

        public static int ParseUnreadMsgCount(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var numberSpan = doc.DocumentNode.NodeByXpath(
                "//*[@id=\"stickytopbar\"]/div/div[2]/div/div[1]/div[3]/a/span/span[3]/span/span");

            var count = Convert.ToInt32(numberSpan.InnerText);

            return count;
        }

        public static List<int> ParseUnreadMsgNumbers(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var msgTable = doc.DocumentNode.NodeByClass("items messages");
            var noReadIcons = msgTable.SelectNodes("//*[contains(@class, 'icon msgnoread')]");


            var numbers = new List<int>();
            int msgNumber = 0;
            foreach (var icoUnread in noReadIcons)
            {
                //var icoUnread = node.NodeByClass("icon msgnoread");
                //if (icoUnread == null) continue;

                var value = icoUnread.GetAttributeValue("id", null);
                if (value == null) continue;

                value = value.Replace("span", string.Empty);

                msgNumber = Convert.ToInt32(value);
                numbers.Add(msgNumber);
            }

            return numbers;
        }

        //public bool NeedUpdateImage()
        //{


        //    //var imagePath = Product.GetImagePath();
        //    //var fieldInfo = new FileInfo(imagePath);
        //    //if (!fieldInfo.Exists)
        //    //{
        //    //    return false;
        //    //}

        //    //if (string.IsNullOrWhiteSpace(Image))
        //    //{
        //    //    return true;
        //    //}

        //    //// на случай что еще не переделано на imageId
        //    //if (Image.Contains(".jpg") || Image.Contains(".bmp"))
        //    //{
        //    //    return true;
        //    //}

        //    //var lastWriteTime = File.GetLastWriteTime(imagePath);

        //    //bool needUpdate = lastWriteTime > DateUpdate;

        //    //return needUpdate;
        //}



        //public bool IsPublished()
        //{
        //    bool res = Number != 0;
        //    return res;

        //}

        public bool IsExpired()
        {
            var need = DateExpire < DateTime.Now;
            return need;
        }

        public void OnClosed()
        {
            DateExpire = DateTime.Now;
        }

        public TimeSpan TimeBegoreExpired()
        {
            if (DateExpire <= DateTime.Now)
            {
                return default(TimeSpan);
            }

            var result = DateExpire - DateTime.Now;
            return result;
        }

        public void UpdateDateExpire()
        {
            DateExpire = DateTime.Now.AddDays(Days);
        }

        public void ApplyStyle()
        {
            var multiStyler = new MultiStyler();
            multiStyler.ApplyStyles(Product, Product.BegemotProduct, this);
        }

        private AdvExportInfo exportInfo;

        public AdvExportInfo PrepareExport()
        {
            exportInfo = new AdvExportInfo(this);

            PrepareExportRollBack(exportInfo);

            ApplyStyle();
            ApplyPrice();
            PrepareImages();

            bool isExpired = IsExpired();
            bool productAllow = Product.IsAllowPost();

            exportInfo.Close = Published && !isExpired && (!Active || !productAllow);
            exportInfo.Create = !Published && Active && productAllow;
            exportInfo.Repost = Published && isExpired;
            exportInfo.UpdateImages = IsNeedUpdateImages();
            exportInfo.UpdateData = IsNeedUpdateData(exportInfo);

            return exportInfo;
        }

        private void ApplyPrice()
        {
            Price = Marketplace.PreparePrice(Product.Price);
        }

        private void PrepareExportRollBack(AdvExportInfo info)
        {
            exportInfo.Title = Title;
            exportInfo.Description = Description;
            exportInfo.Price = Price;
        }


        public void ExportRollBack()
        {
            if (exportInfo == null) return;

            Title = exportInfo.Title;
            Description = exportInfo.Description;
            Price = exportInfo.Price;

            exportInfo = null;
        }

        private bool IsNeedUpdateImages()
        {
            var result = AdvImages.Any(i => i.CurrentVersion.Data.IsEmpty());
            return result;
        }

        bool IsNeedUpdateData(AdvExportInfo exportInfo)
        {
            var title = exportInfo.Title;
            var description = exportInfo.Description;
            var price = exportInfo.Price;

            bool changedTitle = Title != title;
            bool changedDescription = Description != description;
            bool changedPrice = Price != price;

            bool needUpdate = changedTitle || changedDescription || changedPrice;

            return needUpdate;
        }

        
        private void PrepareImages()
        {
            var q =
                from pi in Product.Images
                let ai = pi.AdvImages.FirstOrDefault(a => a.Adv24au.Id == this.Id)
                let aiv = pi.CurrentVersion.AdvImageVersions.FirstOrDefault(aiv => aiv.AdvImage.Adv24au.Id == this.Id)
                select new { ProductImage = pi, AdvImage = ai, AdvImageVersion = aiv };

            var productAdvImages = q.ToList();

            bool save = false;

            foreach (var productAdvImage in productAdvImages)
            {
                save = false;

                ProductImage productImage = productAdvImage.ProductImage;
                AdvImage advImage = productAdvImage.AdvImage;
                AdvImageVersion advImageVersion = productAdvImage.AdvImageVersion;

                if (advImage == null)
                {
                    advImage = new AdvImage();
                    Context.Inst.AdvImageSet.Add(advImage);

                    advImage.Adv24au = this;
                    advImage.ProductImage = productImage;

                    save = true;
                }

                if (advImage.Order != productImage.Order)
                {
                    advImage.Order = productImage.Order;

                    save = true;
                }

                if (save)
                {
                    Context.Save();
                    save = false;
                }

                if (advImageVersion == null)
                {
                    advImageVersion = new AdvImageVersion();
                    Context.Inst.AdvImageVersionSet.Add(advImageVersion);

                    advImageVersion.AdvImage = advImage;
                    advImageVersion.ProductImageVersion = productImage.CurrentVersion;

                    save = true;
                }

                if (advImage.CurrentVersion != advImageVersion)
                {
                    advImage.CurrentVersion = advImageVersion;

                    save = true;
                }

                if (save)
                {
                    Context.Save();
                }
            }
        }


        public bool IsSale
        {
            get
            {
                bool result = Product.PriceOld > Price;
                return result;
            }
        }

        public int GetQuantity()
        {
            return 5;
        }
    }

    public class AdvExportInfo
    {
        public Adv24au Adv { get; set; }

        public AdvExportInfo(Adv24au adv)
        {
            Adv = adv;
        }

        public bool IsNoAction()
        {
            var anyAction = Create || Close || Repost || UpdateImages || UpdateData;
            var noAction = !anyAction;
            return noAction;
        }

        public bool IsRepostOnly()
        {
            var anyAction = Create || Close || UpdateImages || UpdateData;
            var repostOnly = !anyAction && Repost;
            return repostOnly;
        }

        public bool Close { get; set; }

        public bool Create { get; set; }

        public bool Repost { get; set; }

        public bool UpdateImages { get; set; }

        public bool UpdateData { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }


    }
}