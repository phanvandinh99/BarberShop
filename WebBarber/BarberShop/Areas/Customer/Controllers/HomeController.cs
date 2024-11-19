using BarberShop.Models;
using System;
using System.Data.Entity;
using System.Linq;
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
                ViewBag.SanPham = await db.SanPham.OrderByDescending(n => n.MaSanPham)
                                                  .Take(15)
                                                  .ToListAsync();

                ViewBag.DichVu = await db.DichVu.ToListAsync();

                return View();
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Hiển thị nội dung thất bại.";
                return View();
            }
        }
    }
}