//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ShopDataLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class PropertyHystory
    {
        public int Id { get; set; }
        public string Entity { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public System.DateTime DateUpdate { get; set; }
        public int EntityId { get; set; }
    }
}
