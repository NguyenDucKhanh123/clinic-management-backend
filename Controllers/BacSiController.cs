using ClinicManagementWeb.Data;
using ClinicManagementWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ClinicManagementWeb.Controllers
{
    [Authorize(Roles = "BacSi")]
    public class BacSiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BacSiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =============================
        // 1️⃣ Danh sách lịch khám của bác sĩ
        // =============================
        public IActionResult XemLichKham()
        {
            ViewData["Title"] = "Danh sách lịch khám";
            return View();
        }

        [HttpGet]
        public IActionResult GetLichKhamChoBacSi()
        {
            var email = User.Identity?.Name;
            if (email == null)
                return Json(new { success = false, message = "Không xác định được bác sĩ đang đăng nhập." });

            var bacSi = _context.BacSis.FirstOrDefault(x => x.Email == email);
            if (bacSi == null)
                return Json(new { success = false, message = "Không tìm thấy thông tin bác sĩ trong hệ thống." });

            var ds = _context.LichKhams
                .Include(x => x.BenhNhan)
                .Where(x => x.BacSiId == bacSi.Id)
                .Select(x => new
                {
                    x.Id,
                    BenhNhan = x.BenhNhan != null ? x.BenhNhan.HoTen : "Không rõ",
                    BacSi = x.BacSiTen ?? (x.BacSi != null ? x.BacSi.HoTen : "Không rõ"),
                    NgayKham = x.NgayKham.ToString("dd/MM/yyyy HH:mm"),
                    x.TrangThai
                })
                .ToList();

            return Json(new { success = true, data = ds });
        }

        // =============================
        // 2️⃣ Trang khám bệnh cụ thể
        // =============================
        public IActionResult KhamBenh(int id)
        {
            var lichKham = _context.LichKhams
                .Include(x => x.BenhNhan)
                .Include(x => x.BacSi)
                .FirstOrDefault(x => x.Id == id);

            if (lichKham == null)
                return NotFound("Không tìm thấy lịch khám.");

            var benhAn = _context.BenhAns
                .Include(x => x.DonThuocs)
                .Include(x => x.BenhAnDichVus)
                    .ThenInclude(x => x.DichVu)
                .FirstOrDefault(x => x.LichKhamId == id);

            if (benhAn == null)
            {
                benhAn = new BenhAn
                {
                    BacSiId = lichKham.BacSiId,
                    BenhNhanId = lichKham.BenhNhanId,
                    NgayKham = DateTime.Now,
                    LichKhamId = id,
                    BenhNhan = lichKham.BenhNhan,
                    TrangThaiPhatThuoc = "ChoPhat",
                    SoGioKham = 1,
                    BenhAnDichVus = new List<BenhAnDichVu>()
                };
            }
            else
            {
                if (benhAn.BenhNhan == null)
                    benhAn.BenhNhan = lichKham.BenhNhan;

                if (string.IsNullOrEmpty(benhAn.TrangThaiPhatThuoc))
                    benhAn.TrangThaiPhatThuoc = "ChoPhat";

                if (benhAn.BenhAnDichVus == null)
                    benhAn.BenhAnDichVus = new List<BenhAnDichVu>();
            }

            ViewBag.AllThuocs = _context.Thuocs.Select(t => t.TenThuoc).ToList();
            ViewBag.DichVus = _context.DichVus.ToList();

            ViewData["Title"] = "Khám bệnh";

            // 🔹 PHẦN B – Danh sách dịch vụ
            ViewBag.DichVus = _context.DichVus
                .Select(d => new
                {
                    id = d.Id,
                    tenDichVu = d.TenDichVu,
                    donGia = d.DonGia
                })
                .ToList();




            return View(benhAn);
        }

        // =============================
        // 3️⃣ Lưu bệnh án
        // =============================
        [HttpPost]
        public IActionResult LuuKhamBenh([FromBody] BenhAn model)
        {
            if (model == null)
                return BadRequest(new { success = false, message = "Dữ liệu không hợp lệ." });

            var benhAn = _context.BenhAns
                .Include(x => x.DonThuocs)
                .Include(x => x.BenhAnDichVus)
                .FirstOrDefault(x => x.LichKhamId == model.LichKhamId);

            if (benhAn == null)
            {
                benhAn = new BenhAn
                {
                    LichKhamId = model.LichKhamId,
                    BenhNhanId = model.BenhNhanId,
                    BacSiId = model.BacSiId,
                    NgayKham = model.NgayKham == default ? DateTime.Now : model.NgayKham,
                    TrieuChung = model.TrieuChung,
                    ChanDoan = model.ChanDoan,
                    GhiChu = model.GhiChu,
                    HenTaiKham = model.HenTaiKham,
                    SoGioKham = model.SoGioKham,
                    DonThuocs = model.DonThuocs ?? new List<DonThuoc>(),
                    BenhAnDichVus = model.BenhAnDichVus ?? new List<BenhAnDichVu>(),
                    TrangThaiPhatThuoc = "ChoPhat"
                };
                _context.BenhAns.Add(benhAn);
            }
            else
            {
                benhAn.TrieuChung = model.TrieuChung;
                benhAn.ChanDoan = model.ChanDoan;
                benhAn.GhiChu = model.GhiChu;
                benhAn.HenTaiKham = model.HenTaiKham;
                benhAn.SoGioKham = model.SoGioKham;

                benhAn.DonThuocs.Clear();
                if (model.DonThuocs != null)
                {
                    foreach (var t in model.DonThuocs)
                    {
                        benhAn.DonThuocs.Add(new DonThuoc
                        {
                            TenThuoc = t.TenThuoc,
                            LieuDung = t.LieuDung,
                            SoNgay = t.SoNgay,
                            GhiChu = t.GhiChu,
                            SoLuong = t.SoLuong,
                            DonViTinh = t.DonViTinh,
                            DonGia = t.DonGia,
                            CachDung = t.CachDung
                        });
                    }
                }

                benhAn.BenhAnDichVus.Clear();
                if (model.BenhAnDichVus != null)
                {
                    foreach (var dv in model.BenhAnDichVus)
                    {
                        benhAn.BenhAnDichVus.Add(new BenhAnDichVu
                        {
                            DichVuId = dv.DichVuId,
                            SoLuong = dv.SoLuong,
                            DonGia = dv.DonGia
                        });
                    }
                }

                if (string.IsNullOrEmpty(benhAn.TrangThaiPhatThuoc))
                    benhAn.TrangThaiPhatThuoc = "ChoPhat";
            }

            _context.SaveChanges();
            return Json(new { success = true, message = "Đã lưu bệnh án thành công." });
        }

        // =============================
        // 4️⃣ Hoàn tất khám
        // =============================
        [HttpPost]
        public IActionResult HoanThanhKham(int id)
        {
            var lichKham = _context.LichKhams.FirstOrDefault(x => x.Id == id);
            if (lichKham == null)
                return Json(new { success = false, message = "Không tìm thấy lịch khám." });

            var benhAn = _context.BenhAns.FirstOrDefault(x => x.LichKhamId == id);
            if (benhAn == null)
            {
                benhAn = new BenhAn
                {
                    BacSiId = lichKham.BacSiId,
                    BenhNhanId = lichKham.BenhNhanId,
                    NgayKham = DateTime.Now,
                    LichKhamId = id,
                    TrangThaiPhatThuoc = "ChoPhat"
                };
                _context.BenhAns.Add(benhAn);
            }
            else
            {
                if (string.IsNullOrEmpty(benhAn.TrangThaiPhatThuoc))
                    benhAn.TrangThaiPhatThuoc = "ChoPhat";
            }

            lichKham.TrangThai = "Đã khám";
            _context.SaveChanges();

            return Json(new { success = true, message = "Đã hoàn tất khám và lưu bệnh án." });
        }

        // =============================
        // 5️⃣ Lịch sử bệnh nhân
        // =============================
        public IActionResult LichSuBenhNhan(int benhNhanId)
        {
            var ds = _context.BenhAns
                .Include(x => x.BacSi)
                .Where(x => x.BenhNhanId == benhNhanId)
                .OrderByDescending(x => x.NgayKham)
                .ToList();

            ViewData["Title"] = "Lịch sử bệnh nhân";
            return View(ds);
        }
    }
}
