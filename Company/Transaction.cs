//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Company
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public int ID { get; set; }
        public int Store_From { get; set; }
        public int Store_To { get; set; }
        public int Prod_Code { get; set; }
        public Nullable<int> Quentity { get; set; }
        public System.DateTime Expire_Date { get; set; }
        public System.DateTime Production_Date { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
        public virtual Store Store1 { get; set; }
    }
}