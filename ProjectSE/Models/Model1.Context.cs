﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectSE.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DatabaseSEEntities : DbContext
    {
        public DatabaseSEEntities()
            : base("name=DatabaseSEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Renter> Renters { get; set; }
        public virtual DbSet<Repair> Repairs { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Technician> Technicians { get; set; }
        public virtual DbSet<Estimate> Estimates { get; set; }
    }
}
