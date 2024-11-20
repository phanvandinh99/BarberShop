using BarberShop.Models;
using BarberShop.Models.ViewModels;
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
                if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
                {
                    TempData["ToastMessage"] = "error|Bạn cần đăng nhập để đặt lịch.";
                    return RedirectToAction("Index", "Home");
                }

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

        [HttpPost]
        public async Task<ActionResult> DatLich(LichHenItems model)
        {
            try
            {
                if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
                {
                    TempData["ToastMessage"] = "error|Bạn cần đăng nhập để đặt lịch.";
                    return RedirectToAction("Index", "Home");
                }

                if (!ModelState.IsValid)
                {
                    TempData["ToastMessage"] = "error|Dữ liệu không hợp lệ.";
                    return RedirectToAction("Index", "DatLich");
                }

                // Lấy thời gian hiện tại và kiểm tra thời gian chọn
                var currentTime = DateTime.Now;
                if (!DateTime.TryParse(model.ThoiGianDat, out DateTime selectedTime))
                {
                    TempData["ToastMessage"] = "error|Thời gian không hợp lệ.";
                    return RedirectToAction("Index", "DatLich");
                }

                if (selectedTime < currentTime)
                {
                    TempData["ToastMessage"] = "error|Thời gian bạn chọn đã qua.";
                    return RedirectToAction("Index", "DatLich");
                }

                // Tính toán tổng tiền và thời gian dịch vụ
                decimal totalAmount = 0;
                int totalTime = 0;

                List<DichVu> listDichVu = await db.DichVu
                                                 .Where(dv => model.Services.Contains(dv.MaDichVu))
                                                 .ToListAsync();

                foreach (var service in listDichVu)
                {
                    totalAmount += service.DonGia;
                    totalTime += int.Parse(service.ThoiGian);
                }

                KhachHang kh = (KhachHang)Session["TaiKhoan"];

                // Tạo đối tượng LichHen
                var lichHen = new LichHen
                {
                    HoTen = model.HoTen,
                    SDT = model.SDT,
                    ThoiGianDat = model.ThoiGianDat,
                    ThoiGianHuy = selectedTime.AddMinutes(15),
                    NgayDat = DateTime.Now,
                    TongTien = totalAmount,
                    TrangThai = 0,
                    MaChiNhanh = model.MaChiNhanh,
                    TaiKhoanKH = kh.TaiKhoanKH,
                };

                // Thêm các dịch vụ vào LichHen
                foreach (var service in listDichVu)
                {
                    var suDungDichVu = new SuDungDichVu
                    {
                        MaDichVu = service.MaDichVu,
                        MaLichHen = lichHen.MaLichHen,
                        ThanhTien = service.DonGia,
                        Huy = false,
                    };

                    db.SuDungDichVu.Add(suDungDichVu);
                }

                // Thêm lịch hẹn vào cơ sở dữ liệu
                db.LichHen.Add(lichHen);

                await db.SaveChangesAsync();

                TempData["ToastMessage"] = "success|Đặt lịch thành công.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Đặt lịch thất bại.";
                return RedirectToAction("Index", "DatLich");
            }
        }

        #region Xem Lịch Hẹn sau khi đặt
        [HttpGet]
        public async Task<ActionResult> XemLichHen(string sTaiKhoanKH, int? iTrangThai)
        {
            try
            {
                if (string.IsNullOrEmpty(sTaiKhoanKH))
                {
                    TempData["ToastMessage"] = "info|Bạn cần phải đăng nhập";
                    return RedirectToAction("Index", "Home");
                }

                // Tìm khách hàng
                KhachHang kh = await db.KhachHang.FindAsync(sTaiKhoanKH);

                if (kh == null)
                {
                    TempData["ToastMessage"] = "error|Khách hàng không tồn tại.";
                    return RedirectToAction("Index", "Home");
                }

                // Lấy danh sách lịch hẹn khách hàng
                List<LichHen> lichHen = await db.LichHen.Where(n => n.TaiKhoanKH == kh.TaiKhoanKH)
                                                        .OrderByDescending(n => n.MaLichHen)
                                                        .ToListAsync(); ;


                ViewBag.TrangThai = iTrangThai;
                ViewBag.TaiKhoanKH = sTaiKhoanKH;

                // Trả về danh sách lịch hẹn
                return View(lichHen);
            }
            catch (Exception ex)
            {
                // Log lỗi nếu cần thiết
                TempData["ToastMessage"] = "error|Xem lịch hẹn thất bại.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<ActionResult> XemChiTiet(int? iMaLichHen)
        {
            try
            {
                if (iMaLichHen == null)
                {
                    TempData["ToastMessage"] = "info|Lịch hẹn không tồn tại";
                    return RedirectToAction("Index", "Home");
                }

                LichHen lichHen = await db.LichHen.FindAsync(iMaLichHen);

                ViewBag.SuDungDichVu = await db.SuDungDichVu.Where(n => n.MaLichHen == iMaLichHen)
                                                            .ToListAsync();

                return View(lichHen);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Xem chi tiết lịch hẹn thất bại.";
                return RedirectToAction("Index", "Home");
            }
        }


        public async Task<ActionResult> XoaDichVu(int iMaDichVu, int iMaLichHen, string strURL)
        {
            try
            {
                if (iMaDichVu <= 0 || iMaLichHen <= 0)
                {
                    TempData["ToastMessage"] = "info|Mã dịch vụ hoặc mã lịch hẹn không hợp lệ.";
                    return RedirectToAction("Index", "Home");
                }

                // Tìm dịch vụ sử dụng theo mã
                var suDungDichVu = await db.SuDungDichVu
                                           .SingleOrDefaultAsync(n => n.MaDichVu == iMaDichVu && n.MaLichHen == iMaLichHen);

                if (suDungDichVu == null)
                {
                    TempData["ToastMessage"] = "info|Không tồn tại dịch vụ với thông tin cung cấp.";
                    return RedirectToAction("Index", "Home");
                }

                // Lấy lịch hẹn liên quan
                var lichHen = await db.LichHen.FindAsync(iMaLichHen);
                if (lichHen == null)
                {
                    TempData["ToastMessage"] = "error|Lịch hẹn không tồn tại.";
                    return RedirectToAction("Index", "Home");
                }

                // Trừ dịch vụ và cập nhật tổng tiền
                lichHen.TongTien -= suDungDichVu.ThanhTien;
                db.SuDungDichVu.Remove(suDungDichVu);

                // Lưu thay đổi
                await db.SaveChangesAsync();

                TempData["ToastMessage"] = "success|Xóa dịch vụ thành công.";
                return Redirect(strURL);
            }
            catch (Exception ex)
            {
                TempData["ToastMessage"] = "error|Xóa dịch vụ thất bại: " + ex.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

    }
}