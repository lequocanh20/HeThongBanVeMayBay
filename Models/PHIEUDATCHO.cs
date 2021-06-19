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

    public partial class PHIEUDATCHO
    {
        public int ID { get; set; }

        [Display(Name = "Mã đặt chỗ")]
        public string IDDatCho { get; set; }

        [Display(Name = "Mã chuyến bay")]
        public int IDChuyenBay { get; set; }

        public string CMND { get; set; }

        [Display(Name = "Giá tiền")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [DataType(DataType.Currency)]
        public int GiaTien { get; set; }

        [Display(Name = "Loại vé")]
        public string LoaiVe { get; set; }


        [Display(Name = "Trạng thái")]
        public string TrangThai { get; set; }
    
        public virtual CHUYENBAY CHUYENBAY { get; set; }
        public virtual HANHKHACH HANHKHACH { get; set; }
    }
}
