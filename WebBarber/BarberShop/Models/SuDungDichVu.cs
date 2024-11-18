namespace BarberShop.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SuDungDichVu")]
    public partial class SuDungDichVu
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDichVu { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaLichHen { get; set; }

        [Column(TypeName = "money")]
        public decimal ThanhTien { get; set; }

        public DateTime ThoiGianHuy { get; set; }

        public bool? Huy { get; set; }

        public virtual DichVu DichVu { get; set; }

        public virtual LichHen LichHen { get; set; }
    }
}
