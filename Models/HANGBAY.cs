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
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class HANGBAY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HANGBAY()
        {
            ImageEmp = "~/Content/images/add.jpg";
            this.CHUYENBAYs = new HashSet<CHUYENBAY>();
            this.MAYBAYs = new HashSet<MAYBAY>();
        }

        public int ID { get; set; }

        [Display(Name = "M� h�ng h�ng kh�ng")]
        public string IDHangBay { get; set; }

        [Display(Name = "T�n h�ng h�ng kh�ng")]
        public string TenHangbay { get; set; }

        [Display(Name = "Avatar")]
        public string ImageEmp { get; set; }

        [NotMapped]
        public HttpPostedFileBase UploadImage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUYENBAY> CHUYENBAYs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MAYBAY> MAYBAYs { get; set; }
    }
}
