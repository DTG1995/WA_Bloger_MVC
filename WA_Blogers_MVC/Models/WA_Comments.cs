//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WA_Blogers_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WA_Comments
    {
        public WA_Comments()
        {
            this.WA_Comments1 = new HashSet<WA_Comments>();
        }
    
        public int CommentID { get; set; }
        public string ContenComment { get; set; }
        public Nullable<int> PostID { get; set; }
        public Nullable<int> Author { get; set; }
        public Nullable<System.DateTime> Created { get; set; }
        public Nullable<int> Parent { get; set; }
    
        public virtual WA_Posts WA_Posts { get; set; }
        public virtual WA_Users WA_Users { get; set; }
        public virtual ICollection<WA_Comments> WA_Comments1 { get; set; }
        public virtual WA_Comments WA_Comments2 { get; set; }
    }
}
