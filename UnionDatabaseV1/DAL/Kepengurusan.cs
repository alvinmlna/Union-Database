namespace UnionDatabaseV1.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Union.Kepengurusan")]
    public partial class Kepengurusan
    {
        public int Id { get; set; }

        public int PUK_ID { get; set; }

        [Required]
        public string Ketua { get; set; }

        [Required]
        public string Sekertaris { get; set; }

        [Required]
        public string Bendahara { get; set; }

        [Required]
        public string Waka1 { get; set; }

        [Required]
        public string Wasek1 { get; set; }

        [Required]
        public string Waka2 { get; set; }

        [Required]
        public string Wasek2 { get; set; }

        [Required]
        public string Waka3 { get; set; }

        [Required]
        public string Wasek3 { get; set; }

        [Required]
        public string Waka4 { get; set; }

        [Required]
        public string Wasek4 { get; set; }

        [Required]
        public string Waka5 { get; set; }

        [Required]
        public string Wasek5 { get; set; }

        [Required]
        public string Waka6 { get; set; }

        [Required]
        public string Wasek6 { get; set; }

        public virtual PUK PUK { get; set; }
    }
}
