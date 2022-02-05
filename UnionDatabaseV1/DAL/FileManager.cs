namespace UnionDatabaseV1.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Union.FileManager")]
    public partial class FileManager
    {
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public string Name { get; set; }

        public int Category { get; set; }

        public int? PUK_ID { get; set; }

        public virtual PUK PUK { get; set; }
    }
}
