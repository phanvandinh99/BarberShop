using System.Data.Entity;

namespace BarberShop.Models
{
    public partial class DatabaseBarberShop : DbContext
    {
        public DatabaseBarberShop()
            : base("name=DatabaseBarberShop")
        {
        }

        public virtual DbSet<ChiNhanh> ChiNhanh { get; set; }
        public virtual DbSet<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual DbSet<DichVu> DichVu { get; set; }
        public virtual DbSet<DonHang> DonHang { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<LichHen> LichHen { get; set; }
        public virtual DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public virtual DbSet<NhanVien> NhanVien { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<SuDungDichVu> SuDungDichVu { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiNhanh>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<ChiTietDonHang>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DichVu>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DichVu>()
                .Property(e => e.ThoiGian)
                .IsUnicode(false);

            modelBuilder.Entity<DichVu>()
                .HasMany(e => e.SuDungDichVu)
                .WithRequired(e => e.DichVu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .Property(e => e.TaiKhoanKH)
                .IsUnicode(false);

            modelBuilder.Entity<DonHang>()
                .HasMany(e => e.ChiTietDonHang)
                .WithRequired(e => e.DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.TaiKhoanKH)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.DonHang)
                .WithRequired(e => e.KhachHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LichHen>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<LichHen>()
                .Property(e => e.TongTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LichHen>()
                .Property(e => e.TaiKhoanKH)
                .IsUnicode(false);

            modelBuilder.Entity<LichHen>()
                .HasMany(e => e.SuDungDichVu)
                .WithRequired(e => e.LichHen)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.TaiKhoanNV)
                .IsUnicode(false);

            modelBuilder.Entity<NhanVien>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaBan)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .HasMany(e => e.ChiTietDonHang)
                .WithRequired(e => e.SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SuDungDichVu>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);
        }
    }
}
