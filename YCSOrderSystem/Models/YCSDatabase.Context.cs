﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YCSOrderSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class YCSDatabaseEntities : DbContext
    {
        public YCSDatabaseEntities()
            : base("name=YCSDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<OrderTable> OrderTables { get; set; }
        public virtual DbSet<ProdOrder> ProdOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Salary> Salaries { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
    }
}
