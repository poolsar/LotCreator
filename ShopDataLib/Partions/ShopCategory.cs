using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utils;

namespace ShopDataLib
{
    public partial class ShopCategory : IHaveImages
    {
        public ShopCategory(ShopCategory parent)
            : this()
        {

            if (parent != null)
            {
                parent.Childs.Add(this);
                this.Parent = parent;
            }

            Context.Inst.ShopCategorySet.Add(this);
        }

        public override string ToString()
        {
            return string.Format("Категория магазина - {0}", Title);
        }

        public string GetRouteString()
        {
            string route = string.Empty;
            ShopCategory cat = this;

            while (cat != null)
            {
                if (string.IsNullOrWhiteSpace(route))
                {
                    route = "/" + route;
                }
                route = cat.Title + route;
                cat = cat.Parent;
            }

            return route;
        }

        public void Delete()
        {
            this.Images.ToList().ForEach(i => i.Delete());

            Context.Inst.ShopCategorySet.Remove(this);
        }


        public Image AddImage(string imagePath)
        {
            var image = Image.AddImage(this, imagePath);
            return image;
        }

        public void RemoveImage(string imagePath)
        {
            Image.RemoveImage(this, imagePath);
        }

        public DirectoryInfo GetImgFolder()
        {
            var path = string.Format("img/ShopCategory/{0}", Id);

            var folder = FileSystemUtils.GetFolder(path);
            return folder;
        }

        public void ImageUpdated(string imagePath)
        {
            Image.ImageUpdate(this, imagePath);
        }

        class Ims
        {
            public ShopCategory Category { get; set; }

            public ICollection<ShopCategory> Childs { get; set; }

            public IEnumerable<Image> Images { get; set; }
        }

        private Dictionary<int, Ims> images = null;



        public IEnumerable<Image> SameCategoryImages()
        {
            if (images == null)
            {
                var imagesQ = from c in Context.Inst.ShopCategorySet
                              let ProductImages = c.Products.SelectMany(p => p.Images)
                              select new Ims { Category = c, Childs = c.Childs, Images = c.Images.Union(ProductImages) };

                images = imagesQ.ToDictionary(i => i.Category.Id);
            }

            var res = SameCategoryImages(this);
            return res;
        }

        IEnumerable<Image> SameCategoryImages(ShopCategory cat)
        {
            var ci = images[cat.Id];
            var childsImages = ci.Childs.AsParallel().SelectMany(SameCategoryImages);
            return ci.Images.Union(childsImages);
        }


        //public System.Drawing.Image GetDefaulImageBin()
        //{
        //    System.Drawing.Image res = null;

        //    var defImage = DefaultImage;
        //    if (defImage== null)
        //    {
        //        defImage = Images.FirstOrDefault();
        //    }

        //    if (defImage != null)
        //    {
        //        res = ImageLiberator.ImageFromFile(defImage.LocalPath);
        //    }

        //    return res;
        //}

        public void SetInShopRecursive(bool value)
        {
            InShop = value;

            foreach (var product in Products)
            {
                product.InShop = value;
            }

            foreach (var category in Childs)
            {
                category.InShop = value;
                category.SetInShopRecursive(value);
            }
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

        public void DeleteOldImages()
        {
            if (DefaultImage == null) return;

            var imagesToDelete = Images.Where(i => i.Id != DefaultImage.Id).ToList();

            foreach (var image in imagesToDelete)
            {
                Image.RemoveImage(this, image.LocalPath);
            }

        }


        public Image GetDefaultImage()
        {
            return Image.GetDefaultImage(this);
        }

        public void Move(ShopCategory newParent)
        {
            if (Parent != null)
            {
                Parent.Childs.Remove(this);
            }

            if (newParent != null)
            {
                newParent.Childs.Add(this);
            }

            Parent = newParent;
        }

        public void Merge(ShopCategory newParent)
        {
            if (newParent == null)
            {
                throw new ArgumentNullException();
            }

            foreach (ShopProduct shopProduct in Products)
            {
                shopProduct.Move(newParent);
            }

            foreach (ShopCategory shopCategory in Childs)
            {
                shopCategory.Move(newParent);
            }
        }
    }
}