﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HeThongBanVeMayBay.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QLBANVEMAYBAYEntities : DbContext
    {
        public QLBANVEMAYBAYEntities()
            : base("name=QLBANVEMAYBAYEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<CATEGORY> CATEGORies { get; set; }
        public virtual DbSet<CHI_TIET_LICH_CHUYEN_BAY> CHI_TIET_LICH_CHUYEN_BAY { get; set; }
        public virtual DbSet<CHUYENBAY> CHUYENBAYs { get; set; }
        public virtual DbSet<HANHKHACH> HANHKHACHes { get; set; }
        public virtual DbSet<MAYBAY> MAYBAYs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHIEUDATCHO> PHIEUDATCHOes { get; set; }
        public virtual DbSet<SANBAY> SANBAYs { get; set; }
        public virtual DbSet<VECHUYENBAY> VECHUYENBAYs { get; set; }
    }
}