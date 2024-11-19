using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BarberShop.Areas.Customer.Controllers
{
    public class SanPhamController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Xem danh sách sản phẩm
        public async Task<ActionResult> Index()
        {
            try
            {
                List<SanPham> sanPham = await db.SanPham.ToListAsync();

                return View(sanPham);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Load danh sách sản phẩm thất bại.";
                return View();
            }
        }

        // Xem chi tiết sản phẩm
        public async Task<ActionResult> ChiTietSanPham(int? iMaSanPham)
        {
            try
            {
                if(!iMaSanPham.HasValue)
                {
                    TempData["ToastMessage"] = "error|Mã sản phẩm không hợp lệ.";
                    return RedirectToAction("Index", "Home");
                }

                SanPham sanPham = await db.SanPham.FindAsync(iMaSanPham);

                if (sanPham == null)
                {
                    TempData["ToastMessage"] = "error|Không tồn tại sản phẩm.";
                    return RedirectToAction("Index", "Home");
                }

                return View(sanPham);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Xem sản phẩm thất bại.";
                return View();
            }
        }
    }
}