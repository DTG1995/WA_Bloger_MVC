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
    
    public partial class WA_GroupUser
    {
        public WA_GroupUser()
        {
            this.WA_Users = new HashSet<WA_Users>();
            this.WA_Roles = new HashSet<WA_Roles>();
        }
    
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    
        public virtual ICollection<WA_Users> WA_Users { get; set; }
        public virtual ICollection<WA_Roles> WA_Roles { get; set; }
    }
}
