using System.Collections.Generic;
using System.Linq;

namespace ToyShopDataLib
{
    public partial class ProductCategory
    {
        public static ProductCategory FromBegemotProduct(BegemotProduct bproduct)
        {
            var rootCats = Context.Inst.ProductCategorySet.Where(c => c.Parent == null).ToList();

            ProductCategory cat = GetOrAdd(rootCats, bproduct.Group, null);
            cat = GetOrAdd(cat.Childs.ToList(), bproduct.Group1, cat);
            cat = GetOrAdd(cat.Childs.ToList(), bproduct.Group2, cat);
            return cat;
        }

        private static ProductCategory GetOrAdd(List<ProductCategory> prodCats, string title, ProductCategory parent)
        {
            var category = prodCats.FirstOrDefault(c => c.Title == title);
            if (category == null)
            {
                category = new ProductCategory();
                category.Title = title;
                category.Parent = parent;

                Context.Inst.ProductCategorySet.Add(category);
                Context.Save();

                // rootCats
                if (parent == null)
                {
                    prodCats.Add(category);
                }
            }
            return category;
        }

    }
}