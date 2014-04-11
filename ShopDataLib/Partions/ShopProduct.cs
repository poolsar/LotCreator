using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using Utils;

namespace ShopDataLib
{
    public partial class ShopProduct:IHaveImages
    {
        private decimal _margin;

        public void Delete()
        {
            this.Images.ToList().ForEach(i => i.Delete());

            Context.Inst.ShopProductSet.Remove(this);
        }


        public override string ToString()
        {
            return string.Format("Товар магазина - {0}", Title);
        }


        public static void RecalcAllProductsPrice(decimal margin)
        {
            var query = from shp in Context.Inst.ShopProductSet
                        select new { ShopProduct = shp, shp.SupplierProducts };

            var products = query.ToList();


            var marginIndex = 1 + (margin / 100m);

            products.ForEach(p =>
            {
                p.ShopProduct.Price = p.ShopProduct.PurchasePrice() * marginIndex;
                p.ShopProduct.DiscountPrice = p.ShopProduct.PurchasingDiscountPrice() * marginIndex;
            });
        }

        public decimal PurchasePrice()
        {
            var res = this.SupplierProducts.Max(sp => sp.CalcPurchasingPrice());
            return res;
        }

        public decimal PurchasingDiscountPrice()
        {
            var res = this.SupplierProducts.Max(sp => sp.CalcPurchasingDiscountPrice());
            return res;
        }


        public decimal MarginPercent
        {
            get
            {
                decimal res = (DiscountPrice / PurchasingDiscountPrice() - 1) * 100;
                return res;
            }
            set
            {
                DiscountPrice = PurchasingDiscountPrice() * (1 + value / 100m);
            }
        }

        public decimal Margin
        {
            get
            {
                decimal res = DiscountPrice - PurchasingDiscountPrice();
                return res;
            }
            set
            {
                DiscountPrice = PurchasingDiscountPrice() + value;
            }
        }


        public decimal DiscountPercent
        {
            get
            {
                if (!IsSale) return 0;

                decimal res = (Price - DiscountPrice) / Price * 100;
                return res;
            }
        }

        //public System.Drawing.Image GetDefaulImageBin()
        //{
        //    System.Drawing.Image res = null;

        //    var defImage = DefaultImage;
        //    if (defImage == null)
        //    {
        //        defImage = Images.FirstOrDefault();
        //    }

        //    if (defImage != null)
        //    {
        //        res = ImageLiberator.ImageFromFile(defImage.LocalPath);
        //    }

        //    return res;
        //}

        public IEnumerable<Image> SameCategoryImages()
        {
            var catImages = Category.SameCategoryImages();
            return catImages;
        }

        public Image AddImage(string imagePath)
        {
            if (Id==0)
            {
                throw new  ApplicationException("Сохраните ShopProduct перед добавлением картинок");
            }
            var image = Image.AddImage(this,imagePath);

            return image;
        }

        public void RemoveImage(string imagePath)
        {
            Image.RemoveImage(this, imagePath);
        }

        public DirectoryInfo GetImgFolder()
        {


            var path = string.Format("img/ShopProduct/{0}", Id);

            var folder = FileSystemUtils.GetFolder(path);
            return folder;

        }

        public void ImageUpdated(string imagePath)
        {
            Image.ImageUpdate(this, imagePath);
        }

        public string GetLinkRewrite()
        {

            if (string.IsNullOrWhiteSpace(LinkRewrite))
            {
                LinkRewrite = Title.Replace(",", "").Replace(" ", "-");

                LinkRewrite = Transliteration.Front(LinkRewrite, TransliterationType.ISO);

            }

            return LinkRewrite;
        }

        public Image GetDefaultImage()
        {

            return Image.GetDefaultImage(this);
        }

        public void Move(ShopCategory newCategory)
        {
            if (newCategory==null)
            {
                throw new ArgumentNullException();
            }

            Category.Products.Remove(this);
            newCategory.Products.Add(this);
            Category = newCategory;
        }
    }



}