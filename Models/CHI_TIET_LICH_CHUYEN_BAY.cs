//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class CHI_TIET_LICH_CHUYEN_BAY
    {
        public int ID { get; set; }
        public string IDChiTietChuyenBay { get; set; }
        public string IDChuyenBay { get; set; }
        public string IDSanBayTrungGian { get; set; }
        public System.TimeSpan ThoiGianDung { get; set; }
        public string GhiChu { get; set; }
    
        public virtual CHUYENBAY CHUYENBAY { get; set; }
        public virtual SANBAY SANBAY { get; set; }
    }
}