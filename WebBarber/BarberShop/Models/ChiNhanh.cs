namespace BarberShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ChiNhanh")]
    public partial class ChiNhanh
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChiNhanh()
        {
            LichHen = new HashSet<LichHen>();
        }

        [Key]
        public int MaChiNhanh { get; set; }

        [Required]
        [StringLength(200)]
        public string TenChiNhanh { get; set; }

        public string DicChi { get; set; }

        [StringLength(10)]
        public string SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LichHen> LichHen { get; set; }
    }
}
