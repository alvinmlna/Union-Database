﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ConnectionString : DbContext
    {
        public ConnectionString()
            : base("name=ConnectionString")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FileManager> FileManagers { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<PUK> PUKs { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
