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
    
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            this.Description = "";
            this.Childs = new HashSet<ProductCategory>();
            this.Products = new HashSet<Product>();
            this.AdvCategories = new HashSet<AdvCategory>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<ProductCategory> Childs { get; set; }
        public virtual ProductCategory Parent { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<AdvCategory> AdvCategories { get; set; }
    }
}