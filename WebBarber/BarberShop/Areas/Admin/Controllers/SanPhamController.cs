using BarberShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using System.Web;
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

        // Thêm mới sản phẩm
        public async Task<ActionResult> ThemMoi()
        {
            ViewBag.LoaiSanPham = await db.LoaiSanPham.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> ThemMoi(HttpPostedFileBase AnhBia, SanPham model)
        {
            try
            {
                #region Lưu hình ảnh vào thư mục
                if (AnhBia.ContentLength > 0)
                {
                    var tenAnh = Path.GetFileName(AnhBia.FileName);
                    var duongDan = Path.Combine(Server.MapPath("~/Images"), tenAnh);
                    if (!System.IO.File.Exists(duongDan))
                    {
                        AnhBia.SaveAs(duongDan);
                    }
                }
                #endregion

                SanPham sanPham = new SanPham();
                sanPham.TenSanPham = model.TenSanPham;
                sanPham.HinhAnh = AnhBia.FileName;
                sanPham.GiaBan = model.GiaBan;
                sanPham.SoLuong = model.SoLuong;
                sanPham.DaBan = 0;
                sanPham.TrangThai = 0;
                sanPham.MaLoaiSanPham = model.MaLoaiSanPham;

                db.SanPham.Add(sanPham);
                db.SaveChanges();
                ViewBag.LoaiSanPham = await db.LoaiSanPham.ToListAsync();
                TempData["ToastMessage"] = "success|Thêm sản phẩm thành công.";
                return RedirectToAction("Index", "SanPham");
            }
            catch (Exception ex)
            {
                ViewBag.ThongBao = "Đăng Ký Không Thành Công";
                return View();
            }
        }
    }
}