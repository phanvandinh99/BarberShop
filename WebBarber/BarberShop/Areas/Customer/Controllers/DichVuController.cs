using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BarberShop.Areas.Customer.Controllers
{
    public class DichVuController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Xem danh sách dịch vụ
        public async Task<ActionResult> Index()
        {
            try
            {
                List<DichVu> dichVu = await db.DichVu.ToListAsync();

                return View(dichVu);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Load danh sách dịch vụ thất bại.";
                return View();
            }
        }
    }
}