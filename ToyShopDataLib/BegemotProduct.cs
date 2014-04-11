//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ToyShopDataLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class BegemotProduct
    {
        public BegemotProduct()
        {
            this.BegemotSalePrice = new HashSet<BegemotSalePrice>();
            this.BegemotPriceHistory = new HashSet<BegemotPriceHistory>();
            this.BegemotCountHistory = new HashSet<BegemotCountHistory>();
        }
    
        public int Id { get; set; }
        public string Group { get; set; }
        public string Group1 { get; set; }
        public string Group2 { get; set; }
        public string Article { get; set; }
        public string Title { get; set; }
        public string Brand { get; set; }
        public string Code { get; set; }
        public int NDS { get; set; }
        public int CountPerBlock { get; set; }
        public int CountPerBox { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal WholeSalePrice { get; set; }
        public int Count { get; set; }
        public string Descrption { get; set; }
        public string ImagePath { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public bool Active { get; set; }
        public string CopyInfo { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual ICollection<BegemotSalePrice> BegemotSalePrice { get; set; }
        public virtual ICollection<BegemotPriceHistory> BegemotPriceHistory { get; set; }
        public virtual ICollection<BegemotCountHistory> BegemotCountHistory { get; set; }
    }
}
