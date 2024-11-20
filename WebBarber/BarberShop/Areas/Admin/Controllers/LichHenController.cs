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
    public class LichHenController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Danh sách dịch vụ
        public async Task<ActionResult> Index()
        {
            List<LichHen> lichHen = await db.LichHen.ToListAsync();
            return View(lichHen);
        }

        // Cập nhật đơn hàng
        [HttpGet]
        public async Task<ActionResult> CapNhatLichHen(int? iMaLichHen, byte iTrangThai)
        {
            if (!iMaLichHen.HasValue)
            {
                TempData["ToastMessage"] = "info|Lịch hẹn không hợp lệ";
                return RedirectToAction("Index", "LichHen");
            }

            try
            {
                LichHen lichHen = await db.LichHen.FindAsync(iMaLichHen);
                if (lichHen == null)
                {
                    TempData["ToastMessage"] = "info|Lịch hẹn không tồn tại.";
                    return RedirectToAction("Index", "LichHen");
                }

                // Cập nhật trạng thái đơn hàng
                lichHen.TrangThai = iTrangThai;
                await db.SaveChangesAsync();


                TempData["ToastMessage"] = "success|Thành công.";


                // Chuyển hướng đến trang chi tiết đơn hàng
                return RedirectToAction("Index", "LichHen");
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                TempData["ToastMessage"] = "error|Lỗi trong quá trình xử lý lịch hẹn.";
                return RedirectToAction("Index", "LichHen");
            }
        }
    }
}