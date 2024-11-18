using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BarberShop.Areas.Customer.Controllers
{
    public class HomeController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Trang Chủ Khách Hàng
        public async Task<ActionResult> Index()
        {
            try
            {
                List<SanPham> sanPham = await db.SanPham.ToListAsync();
                List<DichVu> dichVu = await db.DichVu.ToListAsync();

                return View(sanPham);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Hiển thị nội dung thất bại.";
                return View();
            }
        }
    }
}