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
    
    public partial class Adv24au
    {
        public Adv24au()
        {
            this.AdvImages = new HashSet<AdvImage>();
        }
    
        public int Id { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public System.DateTime DateExpire { get; set; }
        public bool Active { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public int Days { get; set; }
        public bool Published { get; set; }
        public int MrkCategory { get; set; }
        public string LinkRewrite { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Marketplace Marketplace { get; set; }
        public virtual AdvCategory Category { get; set; }
        public virtual ICollection<AdvImage> AdvImages { get; set; }
    }
}