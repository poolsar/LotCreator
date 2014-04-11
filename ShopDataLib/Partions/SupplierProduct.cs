namespace ShopDataLib
{
    public partial class SupplierProduct : IHaveScrapeStatus
    {
        public override string ToString()
        {
            string toString = string.Format("{0}", Title);
            return toString;
        }

        //public ScrapeStatus Status
        //{
        //    get
        //    {
        //        return (ScrapeStatus)StatusCode;
        //    }
        //    set
        //    {
        //        StatusCode = (int)value;
        //    }
        //}

        public ShopProduct CreateShopProduct(ShopCategory shopCategory)
        {
            var shopProduct = new ShopProduct();
            shopProduct.Title = this.Title;
            shopProduct.Description = this.Description;
            shopProduct.Price = this.Price;
            shopProduct.IsSale = this.IsSale;
            shopProduct.DiscountPrice = this.DiscountPrice;

            

            shopProduct.Category = shopCategory;

            this.ShopProduct = shopProduct;

            Context.Inst.ShopProductSet.Add(shopProduct);
            Context.Save();

            foreach (var image in this.Images)
            {
                shopProduct.AddImage(image.LocalPath);
            }

            return shopProduct;
        }

        public decimal CalcPurchasingDiscountPrice()
        {
            var purchPrice = DiscountPrice * SupDiscountIndex();
            return purchPrice;

            
        }

        public decimal CalcPurchasingPrice()
        {
            var purchPrice = Price * SupDiscountIndex();
            return purchPrice;
        }

        private decimal SupDiscountIndex()
        {
            var res = 1 - Supplier.Discount / 100m;
            return res;
        }
    }
}