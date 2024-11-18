namespace BarberShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietDonHang = new HashSet<ChiTietDonHang>();
        }

        [Key]
        public int MaSanPham { get; set; }

        public string HinhAnh { get; set; }

        [Required]
        [StringLength(200)]
        public string TenSanPham { get; set; }

        [Column(TypeName = "money")]
        public decimal GiaBan { get; set; }

        public int? SoLuong { get; set; }

        public int? DaBan { get; set; }

        public int? TrangThai { get; set; }

        public int? MaLoaiSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }

        public virtual LoaiSanPham LoaiSanPham { get; set; }
    }
}
