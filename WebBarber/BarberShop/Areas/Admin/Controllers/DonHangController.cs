using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;

namespace BarberShop.Areas.Admin.Controllers
{
    public class DonHangController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();


        // Danh sách đơn hàng
        public async Task<ActionResult> Index()
        {
            List<DonHang> donHang = await db.DonHang.ToListAsync();
            return View(donHang);
        }


        // Xem chi tiết đơn hàng
        [HttpGet]
        public async Task<ActionResult> XemChiTiet(int? iMaDonHang)
        {
            try
            {
                if (iMaDonHang == null)
                {
                    TempData["ToastMessage"] = "info|Đơn hàng không tồn tại";
                    return RedirectToAction("Index", "Home");
                }

                DonHang donHang = await db.DonHang.FindAsync(iMaDonHang);

                ViewBag.ChiTietDonHang = await db.ChiTietDonHang.Where(n => n.MaDonHang == iMaDonHang)
                                                                .ToListAsync();

                return View(donHang);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Xem chi tiết đơn hàng thất bại.";
                return RedirectToAction("Index", "Home");
            }
        }

        // Cập nhật đơn hàng
        [HttpGet]
        public async Task<ActionResult> CapNhatDonHang(int? iMaDonHang, int? iTrangThai)
        {
            if (!iMaDonHang.HasValue || !iTrangThai.HasValue)
            {
                TempData["ToastMessage"] = "info|Đơn hàng không hợp lệ";
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // Tìm đơn hàng trong cơ sở dữ liệu
                DonHang donHang = await db.DonHang.FindAsync(iMaDonHang);
                if (donHang == null)
                {
                    TempData["ToastMessage"] = "info|Đơn hàng không tồn tại.";
                    return RedirectToAction("Index", "Home");
                }

                // Cập nhật trạng thái đơn hàng
                donHang.TrangThai = iTrangThai.Value;
                await db.SaveChangesAsync();

                // Xử lý trạng thái đơn hàng
                string toastMessage = "";

                switch (iTrangThai.Value)
                {
                    case 0:
                        toastMessage = "success|Đơn đang xử lý";
                        break;
                    case 1:
                        toastMessage = "success|Đơn đang giao";
                        break;
                    case 2:
                        toastMessage = "success|Đơn đã giao";
                        break;
                    case 3:
                        // Xử lý khi đơn hàng bị hủy
                        List<ChiTietDonHang> chiTietDonHang = await db.ChiTietDonHang
                            .Where(n => n.MaDonHang == iMaDonHang)
                            .ToListAsync();

                        // Cập nhật số lượng sản phẩm cho các sản phẩm trong đơn hàng bị hủy
                        foreach (var item in chiTietDonHang)
                        {
                            SanPham sanPham = await db.SanPham.FindAsync(item.MaSanPham);
                            if (sanPham != null)
                            {
                                sanPham.SoLuong += item.SoLuongMua;
                                sanPham.DaBan -= item.SoLuongMua;
                            }
                        }

                        await db.SaveChangesAsync();
                        toastMessage = "success|Đơn đã hủy";
                        break;
                    default:
                        toastMessage = "error|Thất bại";
                        break;
                }

                // Thông báo kết quả và chuyển hướng
                TempData["ToastMessage"] = toastMessage;

                // Chuyển hướng đến trang chi tiết đơn hàng
                return RedirectToAction("XemChiTiet", "DonHang", new { iMaDonHang });
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                TempData["ToastMessage"] = "error|Lỗi trong quá trình xử lý đơn hàng.";
                return RedirectToAction("Index", "Home");
            }
        }

    }
}