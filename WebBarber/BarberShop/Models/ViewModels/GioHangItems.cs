namespace BarberShop.Models.ViewModels
{
    public class GioHangItems
    {
        DatabaseBarberShop db = new DatabaseBarberShop();
        public int iMaSanPham { get; set; }
        public string sTenSanPham { get; set; }
        public string sHinhAnh { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        //Hàm tạo cho giỏ hàng
        public GioHangItems(int iMaSanPham)
        {
            this.iMaSanPham = iMaSanPham;
            SanPham sanPham = db.SanPham.Find(iMaSanPham);
            sTenSanPham = sanPham.TenSanPham;
            sHinhAnh = sanPham.HinhAnh;
            dDonGia = (double)sanPham.GiaBan;
            iSoLuong = iSoLuong;
        }
    }
}