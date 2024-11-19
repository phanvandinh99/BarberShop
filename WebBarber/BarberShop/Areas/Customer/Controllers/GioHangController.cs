using BarberShop.Models;
using BarberShop.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BarberShop.Areas.Customer.Controllers
{
    public class GioHangController : Controller
    {

        DatabaseBarberShop db = new DatabaseBarberShop();

        #region Giỏ hàng

        //Xây dựng trang giỏ hàng
        public async Task<ActionResult> GioHang()
        {
            if (Session["GioHang"] == null)
            {
                TempData["ToastMessage"] = "info|Giỏ hành trống.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            List<GioHangItems> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        //Lấy giỏ hàng 
        public List<GioHangItems> LayGioHang()
        {
            List<GioHangItems> lstGioHang = Session["GioHang"] as List<GioHangItems>;
            if (lstGioHang == null)
            {
                //Nếu giỏ hàng chưa tồn tại thì mình tiến hành khởi tao list giỏ hàng (sessionGioHang)
                lstGioHang = new List<GioHangItems>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        //Thêm giỏ hàng
        public async Task<ActionResult> ThemGioHang(int iMaSanPham, string strURL, int iSoLuongMua)
        {
            try
            {
                SanPham sanPham = await db.SanPham.FindAsync(iMaSanPham);
                if (iSoLuongMua > sanPham.SoLuong)
                {
                    TempData["ToastMessage"] = "error|Số lượng trong kho không đủ.";
                    return Redirect(strURL);
                }

                //Lấy ra session giỏ hàng
                List<GioHangItems> lstGioHang = LayGioHang();

                //Kiểm tra sách này đã tồn tại trong session[giohang] chưa
                GioHangItems gioHangItems = lstGioHang.Find(n => n.iMaSanPham == iMaSanPham);
                if (gioHangItems == null)
                {
                    gioHangItems = new GioHangItems(iMaSanPham);
                    gioHangItems.iSoLuong = iSoLuongMua;
                    lstGioHang.Add(gioHangItems);
                    TempData["ToastMessage"] = "success|Thêm giỏ hàng thành công.";
                    return Redirect(strURL);
                }
                else
                {
                    gioHangItems.iSoLuong += iSoLuongMua;

                    TempData["ToastMessage"] = "success|Thêm giỏ hàng thành công.";
                    return Redirect(strURL);
                }
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Thêm giỏ hàng thất bại.";
                return Redirect(strURL);
            }
        }

        //Cập nhật giỏ hàng 
        public async Task<ActionResult> CapNhatGioHang(int iMaSanPham, int iSoLuongMua)
        {
            try
            {
                SanPham sanham = await db.SanPham.FindAsync(iMaSanPham);
                if (sanham == null)
                {
                    TempData["ToastMessage"] = "error|Sản phẩm không hợp lệ";
                    return RedirectToAction("GioHang", "GioHang");
                }

                //Lấy giỏ hàng ra từ session
                List<GioHangItems> lstGioHang = LayGioHang();

                //Kiểm tra sp có tồn tại trong session["GioHang"]
                GioHangItems sanpham = lstGioHang.SingleOrDefault(n => n.iMaSanPham == iMaSanPham);
                //Nếu mà tồn tại thì chúng ta cho sửa số lượng
                if (sanpham != null)
                {
                    if (sanham.SoLuong < iSoLuongMua)
                    {
                        TempData["ToastMessage"] = "error|Kho không đáp ứng";
                        return RedirectToAction("GioHang", "GioHang");
                    }
                    else
                    {
                        sanpham.iSoLuong = iSoLuongMua;
                    }
                }

                TempData["ToastMessage"] = "success|Cập nhật giỏ hàng thành công.";
                return RedirectToAction("GioHang");

            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Cập nhật giỏ hàng thất bại.";
                return RedirectToAction("GioHang", "GioHang");
            }
        }

        //Xóa giỏ hàng
        public async Task<ActionResult> XoaGioHang(int iMaSanPham)
        {
            try
            {
                SanPham sanham = await db.SanPham.FindAsync(iMaSanPham);
                if (sanham == null)
                {
                    TempData["ToastMessage"] = "error|Sản phẩm không hợp lệ";
                    return RedirectToAction("GioHang", "GioHang");
                }

                //Lấy giỏ hàng ra từ session
                List<GioHangItems> lstGioHang = LayGioHang();
                GioHangItems sanpham = lstGioHang.SingleOrDefault(n => n.iMaSanPham == iMaSanPham);
                //Nếu mà tồn tại thì chúng ta cho sửa số lượng
                if (sanpham != null)
                {
                    lstGioHang.RemoveAll(n => n.iMaSanPham == iMaSanPham);

                }
                if (lstGioHang.Count == 0)
                {
                    Session["GioHang"] = null;
                    TempData["ToastMessage"] = "info|Giỏ hàng trống";

                    return RedirectToAction("Index", "Home");
                }
                TempData["ToastMessage"] = "success|Xóa sản phẩm thành công";

                return RedirectToAction("GioHang");
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Xóa giỏ hàng thất bại.";
                return RedirectToAction("GioHang", "GioHang");
            }
        }

        //Tính tổng số lượng và tổng tiền
        //Tính tổng số lượng
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHangItems> lstGioHang = Session["GioHang"] as List<GioHangItems>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        //Tính tổng thành tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHangItems> lstGioHang = Session["GioHang"] as List<GioHangItems>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }
        //tạo partial giỏ hàng
        public ActionResult GioHang_Par()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            //ViewBag.TongTien = TongTien();
            return PartialView();
        }

        #endregion

        #region Đặt hàng
        //Xây dựng chức năng đặt hàng
        [HttpPost]
        public async Task<ActionResult> DatHang(FormCollection f)
        {
            try
            {
                //Kiểm tra đăng đăng nhập
                if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
                {
                    TempData["ToastMessage"] = "info|Bạn cần đăng nhập để mua hàng";
                    return RedirectToAction("DangNhap", "DangNhap");
                }
                //Kiểm tra giỏ hàng
                if (Session["GioHang"] == null)
                {
                    TempData["ToastMessage"] = "info|Giỏ hàng trống";
                    RedirectToAction("Index", "Home");
                }


                // Địa chỉ giao hàng
                string tennguoinhan = f["txtTenNguoiNhan"].ToString();
                string diachi = f["txtDiaChi"].ToString();
                string sdt = f["txtSDT"].ToString();

                //Thêm đơn hàng
                DonHang dh = new DonHang();
                KhachHang kh = (KhachHang)Session["TaiKhoan"];

                dh.TenNguoiNhan = tennguoinhan;
                dh.NgayDat = DateTime.Now;
                dh.DiaChi = diachi;
                dh.SDT = sdt;
                dh.NgayGiao = DateTime.Now.AddDays(1);
                dh.TongTien = TongTien();
                dh.TrangThai = 0;
                dh.TaiKhoanKH = kh.TaiKhoanKH;
                db.DonHang.Add(dh);

                //Thêm chi tiết đơn hàng
                List<GioHangItems> gh = LayGioHang();
                foreach (var item in gh)
                {
                    ChiTietDonHang ctdh = new ChiTietDonHang();
                    ctdh.MaDonHang = dh.MaDonHang;
                    ctdh.MaSanPham = item.iMaSanPham;
                    ctdh.SoLuongMua = item.iSoLuong;
                    ctdh.ThanhTien = (decimal)item.ThanhTien;

                    #region Trừ số lượng mua trong Sản Phẩm
                    SanPham sanPham = await db.SanPham.FindAsync(item.iMaSanPham);
                    if (ctdh.SoLuongMua > sanPham.SoLuong)
                    {
                        TempData["ToastMessage"] = "error|Kho không đáp ứng";
                        return RedirectToAction("GioHang", "GioHang");
                    }
                    sanPham.SoLuong = sanPham.SoLuong - item.iSoLuong;
                    sanPham.DaBan = sanPham.DaBan + item.iSoLuong;
                    #endregion
                    db.ChiTietDonHang.Add(ctdh);
                }
                db.SaveChanges();
                Session["GioHang"] = null;

                TempData["ToastMessage"] = "success|Đặt hàng thành công";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Đặt hàng thất bại.";
                return RedirectToAction("GioHang", "GioHang");
            }
        }
        #endregion
    }
}