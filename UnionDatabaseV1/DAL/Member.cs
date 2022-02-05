namespace UnionDatabaseV1.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Union.Member")]
    public partial class Member
    {
        [StringLength(50)]
        public string MemberID { get; set; }

        [Required]
        public string Name { get; set; }

        public int PUK_ID { get; set; }

        [Required]
        [StringLength(40)]
        public string Gender { get; set; }

        public virtual PUK PUK { get; set; }
    }
}
