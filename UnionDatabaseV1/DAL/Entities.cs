namespace UnionDatabaseV1.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<FileManager> FileManagers { get; set; }
        public virtual DbSet<Kepengurusan> Kepengurusans { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<PUK> PUKs { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileManager>()
                .Property(e => e.Path)
                .IsUnicode(false);

            modelBuilder.Entity<FileManager>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Ketua)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Sekertaris)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Bendahara)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Waka1)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Wasek1)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Waka2)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Wasek2)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Waka3)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Wasek3)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Waka4)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Wasek4)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Waka5)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Wasek5)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Waka6)
                .IsUnicode(false);

            modelBuilder.Entity<Kepengurusan>()
                .Property(e => e.Wasek6)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.MemberID)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Member>()
                .Property(e => e.Gender)
                .IsUnicode(false);

            modelBuilder.Entity<PUK>()
                .Property(e => e.PUK1)
                .IsUnicode(false);

            modelBuilder.Entity<PUK>()
                .HasMany(e => e.FileManager)
                .WithOptional(e => e.PUK)
                .HasForeignKey(e => e.PUK_ID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PUK>()
                .HasMany(e => e.Kepengurusan)
                .WithRequired(e => e.PUK)
                .HasForeignKey(e => e.PUK_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PUK>()
                .HasMany(e => e.Member)
                .WithRequired(e => e.PUK)
                .HasForeignKey(e => e.PUK_ID);

            modelBuilder.Entity<PUK>()
                .HasMany(e => e.User)
                .WithOptional(e => e.PUK1)
                .HasForeignKey(e => e.PUK);

            modelBuilder.Entity<User>()
                .Property(e => e.MemberID)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.MemberName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
