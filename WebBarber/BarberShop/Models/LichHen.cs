namespace BarberShop.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LichHen")]
    public partial class LichHen
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LichHen()
        {
            SuDungDichVu = new HashSet<SuDungDichVu>();
        }

        [Key]
        public int MaLichHen { get; set; }

        [Required]
        [StringLength(10)]
        public string SDT { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; }

        public DateTime ThoiGianDat { get; set; }

        public DateTime NgayDat { get; set; }

        [Column(TypeName = "money")]
        public decimal? TongTien { get; set; }

        public byte TrangThai { get; set; }

        [StringLength(50)]
        public string TaiKhoanNV { get; set; }

        public int? MaChiNhanh { get; set; }

        [StringLength(50)]
        public string TaiKhoanKH { get; set; }

        public virtual ChiNhanh ChiNhanh { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual NhanVien NhanVien { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuDungDichVu> SuDungDichVu { get; set; }
    }
}
