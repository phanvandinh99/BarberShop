using BarberShop.Models;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BarberShop.Areas.Customer.Controllers
{
    public class DangNhapController : Controller
    {
        DatabaseBarberShop db = new DatabaseBarberShop();

        // Phần hiền thị thông tin đăng nhập
        public ActionResult User_Par()
        {
            //Kiểm tra đăng đăng nhập
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                ViewBag.User = null;
            }
            else
            {
                KhachHang kh = (KhachHang)Session["TaiKhoan"];
                ViewBag.User = kh;
            }

            return PartialView();
        }
        public ActionResult DangKi()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DangKi(KhachHang model)
        {
            try
            {
                KhachHang kh = new KhachHang();
                kh.TaiKhoanKH = model.TaiKhoanKH;
                kh.MatKhauKH = model.MatKhauKH;
                kh.HoTen = model.HoTen;
                kh.GioiTinh = model.GioiTinh;
                kh.SDT = model.SDT;
                kh.DiaChi = kh.DiaChi;
                kh.TrangThai = 0;
              
                db.KhachHang.Add(kh);
                db.SaveChanges();

                TempData["ToastMessage"] = "success|Đăng ký thành công.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Đăng Ký Không Thành Công";
                return View();
            }
        }

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
                if (ssTaiKhoan == "" & ssMatKhau == "")
                {
                    TempData["ToastMessage"] = "error|Đăng nhập thất bại.";
                    return View();
                }
                else if (ssTaiKhoan == "")
                {
                    TempData["ToastMessage"] = "error|Không bỏ trống user.";
                    return View();
                }
                else if (ssMatKhau == "")
                {
                    TempData["ToastMessage"] = "error|Không bỏ trống mật khẩu.";
                    return View();
                }
                else
                {
                    var kh = await db.KhachHang.SingleOrDefaultAsync(n => n.TaiKhoanKH == ssTaiKhoan & n.MatKhauKH == ssMatKhau);

                    if (kh == null)
                    {
                        TempData["ToastMessage"] = "error|Không tồn tại tài khoản.";
                        return View();
                    }
                    else if (kh.TrangThai == 1)
                    {
                        TempData["ToastMessage"] = "error|Tài khoản của bạn đã bị khóa.";
                        return View();
                    }
                    else
                    {
                        TempData["ToastMessage"] = "info|Đăng nhập thành công.";

                        Session["TaiKhoan"] = kh;
                        return RedirectToAction("Index", "Home");
                    }
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
                Session["TaiKhoan"] = null;
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