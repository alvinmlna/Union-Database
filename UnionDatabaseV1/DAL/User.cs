//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnionDatabaseV1.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public int Id { get; set; }

        [Required]
        public string MemberID { get; set; }
        public int Akses { get; set; }
        public Nullable<int> PUK { get; set; }

        public string MemberName { get; set; }
    
        public virtual PUK PUK1 { get; set; }
    }
}
