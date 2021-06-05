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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class NHANVIEN
    {
        public NHANVIEN()
        {
            ImageEmp = "~/Content/images/add.jpg";
        }
        public int ID { get; set; }

        [Display(Name = "Tài khoản: ")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu: ")]
        public string Pass { get; set; }

        [Display(Name = "Emai: ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Tên NV: ")]
        public string TenNV { get; set; }

        [Display(Name = "Giới tính: ")]
        public string GioiTinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Ngày sinh: ")]
        public DateTime NgaySinh { get; set; } = new DateTime(2000, 01, 01);

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Ngày vào làm: ")]
        public DateTime NgayVaoLam { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Display(Name = "Ngày nghỉ làm: ")]
        public Nullable<DateTime> NgayNghiLam { get; set; }

        [Display(Name = "Chức vụ: ")]
        public string ChucVu { get; set; }

        [Display(Name = "Bộ phận: ")]
        public string BoPhan { get; set; }

        [Display(Name = "Avatar: ")]
        public string ImageEmp { get; set; }
        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }
        public virtual CHUCVU CHUCVU1 { get; set; }
        public virtual PHONGBAN PHONGBAN { get; set; }
    }
    public enum Gender
    {
        Nam, Nữ, Khác
    }
}
