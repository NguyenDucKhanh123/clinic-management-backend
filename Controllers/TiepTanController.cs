using ClinicManagementWeb.Data;
using ClinicManagementWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClinicManagementWeb.Controllers
{
    [Authorize(Roles = "TiepTan")]
    public class TiepTanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiepTanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================================
        // 🏥 TRANG CHÍNH
        // ================================
        public IActionResult NhanBenh()
        {
            ViewData["Title"] = "Nhận bệnh";
            return View();
        }

        public IActionResult CapNhatBenhNhan()
        {
            ViewData["Title"] = "Cập nhật bệnh nhân";
            return View();
        }

        public IActionResult CapNhatLich()
        {
            ViewData["Title"] = "Cập nhật lịch khám";
            return View();
        }

        public IActionResult Xemls()
        {
            ViewData["Title"] = "Xem lịch sử bệnh nhân";
            return View();
        }

        // ================================
        // 🔍 TRA CỨU / THÊM BỆNH NHÂN
        // ================================
        [HttpPost]
        public IActionResult TimBenhNhan(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Json(new { success = false, message = "Vui lòng nhập SĐT hoặc tên bệnh nhân" });

            var bn = _context.BenhNhans
                .FirstOrDefault(x => x.Phone == keyword || (x.HoTen != null && x.HoTen.Contains(keyword)));

            if (bn == null)
                return Json(new { success = false, message = "Không tìm thấy bệnh nhân" });

            return Json(new
            {
                success = true,
                data = new
                {
                    bn.Id,
                    bn.HoTen,
                    bn.GioiTinh,
                    bn.NgaySinh,
                    bn.Phone,
                    bn.DiaChi,
                    bn.CCCD
                }
            });
        }

        [HttpPost]
        public IActionResult ThemBenhNhan(BenhNhan model)
        {
            if (string.IsNullOrWhiteSpace(model.HoTen) || string.IsNullOrWhiteSpace(model.Phone))
                return Json(new { success = false, message = "Họ tên và SĐT là bắt buộc" });

            var existing = _context.BenhNhans.FirstOrDefault(x => x.Phone == model.Phone);
            if (existing != null)
            {
                existing.HoTen = model.HoTen;
                existing.NgaySinh = model.NgaySinh;
                existing.GioiTinh = model.GioiTinh;
                existing.DiaChi = model.DiaChi;
                existing.CCCD = model.CCCD;
                _context.Update(existing);
            }
            else
            {
                _context.BenhNhans.Add(model);
            }

            _context.SaveChanges();
            return Json(new { success = true, message = "Đã lưu bệnh nhân thành công!" });
        }

        // ================================
        // 📋 QUẢN LÝ DANH SÁCH BỆNH NHÂN
        // ================================
        [HttpGet]
        public IActionResult GetAllBenhNhan()
        {
            var ds = _context.BenhNhans
                .Select(b => new
                {
                    id = b.Id,
                    hoTen = b.HoTen,
                    gioiTinh = b.GioiTinh,
                    ngaySinh = b.NgaySinh,
                    phone = b.Phone,
                    cccd = b.CCCD,
                    diaChi = b.DiaChi
                })
                .ToList();

            return Json(new { success = true, data = ds });
        }

        [HttpGet]
        public IActionResult GetBenhNhanById(int id)
        {
            var bn = _context.BenhNhans
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    id = x.Id,
                    hoTen = x.HoTen,
                    gioiTinh = x.GioiTinh,
                    ngaySinh = x.NgaySinh,
                    phone = x.Phone,
                    cccd = x.CCCD,
                    diaChi = x.DiaChi
                })
                .FirstOrDefault();

            if (bn == null)
                return Json(new { success = false, message = "Không tìm thấy bệnh nhân" });

            return Json(new { success = true, data = bn });
        }

        [HttpPost]
        public IActionResult UpdateBenhNhan(BenhNhan model)
        {
            var bn = _context.BenhNhans.FirstOrDefault(x => x.Id == model.Id);
            if (bn == null)
                return Json(new { success = false, message = "Không tìm thấy bệnh nhân để cập nhật" });

            bn.HoTen = model.HoTen;
            bn.GioiTinh = model.GioiTinh;
            bn.NgaySinh = model.NgaySinh;
            bn.Phone = model.Phone;
            bn.CCCD = model.CCCD;
            bn.DiaChi = model.DiaChi;

            _context.SaveChanges();
            return Json(new { success = true, message = "Cập nhật thành công!" });
        }

        // ================================
        // 📅 LỊCH KHÁM
        // ================================
        [HttpPost]
        public IActionResult ThemLichKham(LichKham model)
        {
            if (model.BenhNhanId == 0 || model.BacSiId == 0)
                return Json(new { success = false, message = "Thiếu thông tin bệnh nhân hoặc bác sĩ!" });

            // 🔍 Lấy bệnh nhân và bác sĩ theo ID
            var benhNhan = _context.BenhNhans.FirstOrDefault(x => x.Id == model.BenhNhanId);
            var bacSi = _context.BacSis.FirstOrDefault(x => x.Id == model.BacSiId);

            if (benhNhan == null)
                return Json(new { success = false, message = "Không tìm thấy bệnh nhân!" });

            if (bacSi == null)
                return Json(new { success = false, message = "Không tìm thấy bác sĩ!" });

            // ✅ Gán tên bác sĩ tự động (chỉ để hiển thị nhanh)
            model.BacSiTen = bacSi.HoTen;
            model.TrangThai = "Chưa khám";

            // Kiểm tra trùng lịch
            bool exists = _context.LichKhams.Any(l => l.BacSiId == model.BacSiId &&
                                           l.NgayKham.Year == model.NgayKham.Year &&
                                           l.NgayKham.Month == model.NgayKham.Month &&
                                           l.NgayKham.Day == model.NgayKham.Day &&
                                           l.NgayKham.Hour == model.NgayKham.Hour &&
                                           l.NgayKham.Minute == model.NgayKham.Minute);


            if (exists)
            {
                ViewBag.Error = "Bác sĩ đã có lịch khám vào thời gian này. Vui lòng chọn giờ khác.";
                ViewBag.BacSis = _context.BacSis.ToList(); // để dropdown vẫn hiển thị
                return View("CapNhatLich", model);
            }

            // Thêm lịch khám
            _context.LichKhams.Add(model);
            _context.SaveChanges();

            ViewBag.Success = "✅ Đã thêm lịch khám thành công!";
            ViewBag.BacSis = _context.BacSis.ToList();
            return View("CapNhatLich");
        }

        [HttpGet]
        public IActionResult GetLichKham()
        {
            var ds = _context.LichKhams
                .Include(x => x.BenhNhan)
                .Include(x => x.BacSi)
                .Select(x => new
                {
                    x.Id,
                    benhNhan = x.BenhNhan != null ? x.BenhNhan.HoTen : "Không rõ",
                    bacSi = x.BacSi != null ? x.BacSi.HoTen : x.BacSiTen ?? "Chưa gán",
                    ngayKham = x.NgayKham.ToString("dd/MM/yyyy HH:mm"),
                    x.TrangThai
                })
                .ToList();

            return Json(ds);
        }

        // 📘 Lấy danh sách bác sĩ (đổ vào dropdown)
        [HttpGet]
        public IActionResult GetBacSi()
        {
            var bacSis = _context.BacSis
                .Select(b => new { id = b.Id, hoTen = b.HoTen })
                .ToList();
            return Json(bacSis);
        }

        // ================================
        // 🩺 LỊCH SỬ KHÁM BỆNH
        // ================================
        [HttpGet]
        public IActionResult LichSu(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return Json(new { message = "Vui lòng nhập từ khóa" });

            var bn = _context.BenhNhans.FirstOrDefault(x => x.Phone == keyword || (x.HoTen != null && x.HoTen.Contains(keyword)));
            if (bn == null)
                return Json(new List<object>());

            var ds = _context.LichKhams
                .Include(x => x.BacSi)
                .Where(x => x.BenhNhanId == bn.Id)
                .Select(x => new
                {
                    bacSi = x.BacSi != null ? x.BacSi.HoTen : "Chưa gán",
                    ngayKham = x.NgayKham.ToString("dd/MM/yyyy HH:mm"),
                    x.TrangThai
                }).ToList();

            return Json(ds);
        }

        // ================================
        // ❌ XÓA BỆNH NHÂN
        // ================================
        [HttpPost]
        public IActionResult DeleteBenhNhan(int id)
        {
            var bn = _context.BenhNhans.Find(id);
            if (bn == null)
                return Json(new { success = false, message = "Không tìm thấy bệnh nhân!" });

            var hasLichKham = _context.LichKhams.Any(x => x.BenhNhanId == id);
            if (hasLichKham)
                return Json(new { success = false, message = "Không thể xóa! Bệnh nhân này đã có lịch khám." });

            _context.BenhNhans.Remove(bn);
            _context.SaveChanges();

            return Json(new { success = true, message = "Đã xóa bệnh nhân thành công!" });
        }

        //
        [HttpDelete]
        public IActionResult XoaLichKham(int id)
        {
            var lich = _context.LichKhams.FirstOrDefault(x => x.Id == id);
            if (lich == null)
                return Json(new { success = false, message = "Không tìm thấy lịch khám!" });

            _context.LichKhams.Remove(lich);
            _context.SaveChanges();

            return Json(new { success = true, message = "🗑️ Đã xóa lịch khám thành công!" });
        }

    }
}
