namespace UnionDatabaseV1.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Union.User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string MemberID { get; set; }

        public int Akses { get; set; }

        public int? PUK { get; set; }

        public string MemberName { get; set; }

        public string Password { get; set; }

        public virtual PUK PUK1 { get; set; }
    }
}
