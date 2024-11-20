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
    public class DangNhapController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DangNhap(FormCollection f)
        {
            try
            {
                // Kiểm tra tên đăng nhập và mật khẩu
                string ssTaiKhoan = f["txtTaiKhoan"].ToString();
                string ssMatKhau = f["txtMatKhau"].ToString();

                var nv = await db.NhanVien.SingleOrDefaultAsync(n => n.TaiKhoanNV == ssTaiKhoan & n.MatKhauNV == ssMatKhau);

                if (nv == null)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại !");
                    return View();
                }
                else if (nv.TrangThai == 1)
                {
                    TempData["ToastMessage"] = "error|Tài khoản của bạn đã bị khóa.";
                    return View();
                }
                else
                {
                    TempData["ToastMessage"] = "info|Đăng nhập thành công.";

                    Session["TaiKhoanNV"] = nv;
                    return RedirectToAction("Index", "Home");
                }

            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Đăng nhập thất bại.";
                return View();
            }
        }

        // Đăng xuất
        public ActionResult DangXuat()
        {
            try
            {
                Session["TaiKhoanNV"] = null;
                TempData["ToastMessage"] = "info|Đã đăng xuất.";
                return RedirectToAction("DangNhap", "DangNhap");
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Đăng xuất thất bại.";
                return View();
            }
        }
    }
}