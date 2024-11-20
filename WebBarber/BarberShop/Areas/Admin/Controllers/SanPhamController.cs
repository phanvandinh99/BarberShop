using BarberShop.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BarberShop.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // GET: Admin/SanPham
        public async Task<ActionResult> Index()
        {
            List<SanPham> sanPham = await db.SanPham.ToListAsync();
            return View(sanPham);
        }
    }
}