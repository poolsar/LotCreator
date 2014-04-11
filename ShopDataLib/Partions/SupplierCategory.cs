using System.Collections.Generic;
using System.Linq;

namespace ShopDataLib
{
    public partial class SupplierCategory : IHaveScrapeStatus
    {
        public override string ToString()
        {
            string toString = string.Format("{0}", Title);
            return toString;
        }


        public ShopCategory CreateShopCategory(ShopCategory parent, bool merge = false)
        {
            ShopCategory shopCategory = null;


            if (merge && Parent != null) // принудительное слитие
            {
                shopCategory = parent;
            }
            else
            {
                ShopCategory mergeSubCat = FindMergeCat(parent, this);

                if (mergeSubCat != null) // слитие если в родительской категории есть похожая категория
                {
                    shopCategory = mergeSubCat;
                }
                else // если не сливаем, то создаем новую
                {
                    shopCategory = new ShopCategory(parent);
                    shopCategory.Title = this.Title;
                }
            }


            CreateShopProducts(shopCategory, Products.ToList(), true);
            CreateShopCategories(shopCategory, Childs.ToList());


            return shopCategory;
        }

        public static void CreateShopProducts(ShopCategory parentShopCat, List<SupplierProduct> supProds, bool merge)
        {
            foreach (var supProd in supProds)
            {
                bool isMergeProd = merge;
                if (isMergeProd)
                {
                    isMergeProd = parentShopCat.Products.Any(
                        p => p.Id == supProd.Id || p.Title == supProd.Title);
                }

                if (isMergeProd)
                {
                    continue;
                }

                var shopProduct = supProd.CreateShopProduct(parentShopCat);
                parentShopCat.Products.Add(shopProduct);
            }
        }

        public static List<ShopCategory> CreateShopCategories(ShopCategory parentShopCat, List<SupplierCategory> supCats)
        {
            List<ShopCategory> shopCategories = new List<ShopCategory>();

            foreach (var supCat in supCats)
            {

                ShopCategory shopCategory = supCat.CreateShopCategory(parentShopCat);
                shopCategories.Add(shopCategory);
            }

            return shopCategories;
        }

        private static ShopCategory FindMergeCat(ShopCategory parentShopCat, SupplierCategory supCat)
        {
            IEnumerable<ShopCategory> sameParentCats;
            if (parentShopCat == null)
            {
                sameParentCats = Context.Inst.ShopCategorySet.Where(c => c.Parent == null);
            }
            else
            {
                sameParentCats = parentShopCat.Childs;
            }

            if (sameParentCats == null) return null;

            // проверяем есть ли в этой категории уже подгатегории похожие на создаваемую 
            ShopCategory mergeSubCat = sameParentCats.FirstOrDefault(
               c => (supCat.ShopCategory != null && supCat.ShopCategory.Id == c.Id) || c.Title == supCat.Title);


            return mergeSubCat;
        }
    }
}

