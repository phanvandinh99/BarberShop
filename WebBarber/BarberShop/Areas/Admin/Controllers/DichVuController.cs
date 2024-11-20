using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BarberShop.Areas.Admin.Controllers
{
    public class DichVuController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Danh sách dịch vụ
        public async Task<ActionResult> Index()
        {
            List<DichVu> dichVu = await db.DichVu.ToListAsync();
            return View(dichVu);
        }

        // Thêm mới sản phẩm
        public async Task<ActionResult> ThemMoi()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ThemMoi(DichVu model)
        {
            try
            {
                DichVu dichVu = new DichVu();
                dichVu .TenDichVu = model.TenDichVu;
                dichVu .DonGia = model.DonGia;
                dichVu .ThoiGian = model.ThoiGian;

                db.DichVu.Add(dichVu);
                db.SaveChanges();

                TempData["ToastMessage"] = "success|Thêm dịch vụ thành công.";
                return RedirectToAction("Index", "DichVu");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Đăng Ký Không Thành Công";
                return View();
            }
        }

    }
}