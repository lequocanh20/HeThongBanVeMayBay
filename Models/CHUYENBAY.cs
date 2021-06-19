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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class CHUYENBAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUYENBAY()
        {
            this.PHIEUDATCHOes = new HashSet<PHIEUDATCHO>();
            this.VECHUYENBAYs = new HashSet<VECHUYENBAY>();
        }
    
        public int ID { get; set; }

        [Display(Name = "Mã chuyến bay")]
        public string IDChuyenBay { get; set; }

        [Display(Name = "Hãng bay")]
        [Required(ErrorMessage = "Vui lòng nhập chữ")]
        public string HangBay { get; set; }

        [Display(Name = "Sân bay đi")]
        [Required(ErrorMessage = "Vui lòng nhập chữ")]
        public string IDSanBayDi { get; set; }

        [Display(Name = "Sân bay đến")]
        [Required(ErrorMessage = "Vui lòng nhập chữ")]
        public string IDSanBayDen { get; set; }

        [Display(Name = "Giá tiền")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int GiaTien { get; set; }

        [Display(Name = "Ngày bay")]
        [DataType(DataType.Date)]
        public System.DateTime NgayBay { get; set; } = DateTime.Now;

        [Display(Name = "Giờ bay")]
        public System.TimeSpan GioBay { get; set; }

        [Display(Name = "Thời gian tới dự kiến")]
        public System.TimeSpan ThoiGianToiDuKien { get; set; }

        [Display(Name = "Số ghế hạng 1")]
        [Required(ErrorMessage = "Vui lòng nhập số")]
        public int SoGheHang1 { get; set; }

        [Display(Name = "Số ghế hạng 2")]
        [Required(ErrorMessage = "Vui lòng nhập số")]
        public int SoGheHang2 { get; set; }

        public virtual HANGBAY HANGBAY1 { get; set; }
        public virtual MAYBAY MAYBAY { get; set; }
        public virtual SANBAY SANBAY { get; set; }
        public virtual SANBAY SANBAY1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUDATCHO> PHIEUDATCHOes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VECHUYENBAY> VECHUYENBAYs { get; set; }
    }
}
