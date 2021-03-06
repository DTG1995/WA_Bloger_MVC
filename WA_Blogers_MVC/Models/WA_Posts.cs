//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WA_Blogers_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WA_Posts
    {
        public WA_Posts()
        {
            this.WA_Comments = new HashSet<WA_Comments>();
            this.WA_Likes = new HashSet<WA_Likes>();
            this.WA_Blogs = new HashSet<WA_Blogs>();
        }
    
        public int PostID { get; set; }
         [Display(Name = "Tiêu đề")]
        public string Title { get; set; }
        [Display(Name="Mô tả")]
        public string Description { get; set; }
        [AllowHtml]
        [Display(Name = "Nội dung")]
        public string ContentPost { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<int> Author { get; set; }
        public string Picture { get; set; }
         [Display(Name = "Sử dụng mô tả")]
        public Nullable<bool> UseDescription { get; set; }
        public int Seen { get; set; }
          [Display(Name = "Hoạt động")]
        public bool Active { get; set; }
     
        public virtual ICollection<WA_Comments> WA_Comments { get; set; }
        public virtual ICollection<WA_Likes> WA_Likes { get; set; }
         
        public virtual WA_Users WA_Users { get; set; }
        public virtual ICollection<WA_Blogs> WA_Blogs { get; set; }
    }
}
