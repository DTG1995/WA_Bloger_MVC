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
    
    public partial class WA_Likes
    {
        public int PostID { get; set; }
        public string Author { get; set; }
        public Nullable<int> IsLike { get; set; }
    
        public virtual WA_Posts WA_Posts { get; set; }
        public virtual WA_Users WA_Users { get; set; }
    }
}
