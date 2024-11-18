namespace BarberShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DichVu")]
    public partial class DichVu
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DichVu()
        {
            SuDungDichVu = new HashSet<SuDungDichVu>();
        }

        [Key]
        public int MaDichVu { get; set; }

        [StringLength(100)]
        public string TenDichVu { get; set; }

        [Column(TypeName = "money")]
        public decimal DonGia { get; set; }

        [StringLength(1)]
        public string ThoiGian { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SuDungDichVu> SuDungDichVu { get; set; }
    }
}
