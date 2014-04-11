using System.Collections.Generic;
using System.Linq;

namespace ShopDataLib
{
    public partial class Supplier
    {
        public override string ToString()
        {
            string toString = string.Format("{0}", Title);
            return toString;
        }

        public List<ShopCategory> CreateShopCategories(ShopCategory parentShopCat)
        {
            var shopCategories = SupplierCategory.CreateShopCategories(parentShopCat, GetRootCategories());
            return shopCategories;
        }

        private List<SupplierCategory> GetRootCategories()
        {
            return Categories.Where(c => c.Parent == null).ToList();
        }
    }
}