using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BarberShop.Areas.Customer.Controllers
{
    public class DatLichController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Hiển thị trang đặt lịch
        public async Task<ActionResult> Index()
        {
            try
            {
                ViewBag.ChiNhanh = await db.ChiNhanh.ToListAsync();
                ViewBag.DichVu = await db.DichVu.ToListAsync();

                return View();
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Load danh sách dịch vụ thất bại.";
                return View();
            }
        }
    }
}