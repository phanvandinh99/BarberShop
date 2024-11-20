using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
    }
}