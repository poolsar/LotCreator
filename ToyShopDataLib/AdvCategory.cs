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
    
    public partial class AdvCategory
    {
        public AdvCategory()
        {
            this.Description = "";
            this.Image = "";
            this.Published = false;
            this.Advs = new HashSet<Adv24au>();
            this.ProductCategories = new HashSet<ProductCategory>();
            this.Childs = new HashSet<AdvCategory>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool Published { get; set; }
        public string LinkRewrite { get; set; }
    
        public virtual ICollection<Adv24au> Advs { get; set; }
        public virtual Marketplace Marketplace { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<AdvCategory> Childs { get; set; }
        public virtual AdvCategory Parent { get; set; }
    }
}
