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
    using System.ComponentModel.DataAnnotations;

    public partial class MAYBAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MAYBAY()
        {
            this.CHUYENBAYs = new HashSet<CHUYENBAY>();
        }
    
        public int ID { get; set; }
        [Display(Name = "Số hiệu máy bay")]
        public string IDMayBay { get; set; }

        [Display(Name = "Số đăng bạ")]
        public string SoDangBa { get; set; }

        [Display(Name = "Loại máy bay")]
        [Required(ErrorMessage = "Vui lòng nhập chữ")]
        public string LoaiMayBay { get; set; }

        [Display(Name = "Hãng hàng không")]
        [Required(ErrorMessage = "Vui lòng nhập chữ")]
        public string HangBay { get; set; }

        [Display(Name = "Sức chứa tối đa")]
        [Required(ErrorMessage = "Vui lòng nhập số")]
        [Range(100, 800, ErrorMessage = "Sức chứa tối đa quá lớn so với thông số hãng máy bay đưa ra, vui lòng kiểm tra lại")]
        public int SucChuaToiDa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUYENBAY> CHUYENBAYs { get; set; }
        public virtual HANGBAY HANGBAY1 { get; set; }
    }
}
