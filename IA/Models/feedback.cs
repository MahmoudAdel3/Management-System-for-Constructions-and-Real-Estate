//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IA.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class feedback
    {
        public int Id { get; set; }
        public int tl_id { get; set; }
        public int pm_id { get; set; }
        public int project_id { get; set; }
        public string content { get; set; }
        public int member_id { get; set; }
    
        public virtual user user { get; set; }
        public virtual user user1 { get; set; }
        public virtual user user2 { get; set; }
        public virtual project project { get; set; }
    }
}
