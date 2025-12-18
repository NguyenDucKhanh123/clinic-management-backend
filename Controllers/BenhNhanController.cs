using ClinicManagementWeb.Data;
using ClinicManagementWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ClinicManagementWeb.Controllers
{
    [Authorize(Roles = "BenhNhan")]
    public class BenhNhanController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BenhNhanController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ===========================
        // 1) GET: Form đặt lịch
        // ===========================
        public async Task<IActionResult> DatLich()
        {
            var userId = _userManager.GetUserId(User);
            var benhNhan = await _context.BenhNhans.FirstOrDefaultAsync(b => b.ApplicationUserId == userId);
            ViewBag.BacSis = await _context.BacSis.ToListAsync();
            return View(benhNhan);
        }

        // ===========================
        // 2) POST: Lưu thông tin & tạo lịch khám
        // ===========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatLich(BenhNhan benhNhan, DateTime ngayKham, int? bacSiId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.BacSis = await _context.BacSis.ToListAsync();
                return View(benhNhan);
            }

            var userId = _userManager.GetUserId(User);
            var bn = await _context.BenhNhans.FirstOrDefaultAsync(b => b.ApplicationUserId == userId);
            if (bn == null)
            {
                benhNhan.ApplicationUserId = userId;
                _context.BenhNhans.Add(benhNhan);
                await _context.SaveChangesAsync();
                bn = benhNhan;
            }
            else
            {
                bn.HoTen = benhNhan.HoTen;
                bn.GioiTinh = benhNhan.GioiTinh;
                bn.NgaySinh = benhNhan.NgaySinh;
                bn.DiaChi = benhNhan.DiaChi;
                bn.Phone = benhNhan.Phone;
                bn.CCCD = benhNhan.CCCD;
                _context.BenhNhans.Update(bn);
                await _context.SaveChangesAsync();
            }

            bool exists = await _context.LichKhams
                .AnyAsync(l => l.BacSiId == bacSiId &&
                               l.NgayKham.Year == ngayKham.Year &&
                               l.NgayKham.Month == ngayKham.Month &&
                               l.NgayKham.Day == ngayKham.Day &&
                               l.NgayKham.Hour == ngayKham.Hour &&
                               l.NgayKham.Minute == ngayKham.Minute);

            if (exists)
            {
                ModelState.AddModelError("", "Bác sĩ đã có lịch khám vào thời gian này. Vui lòng chọn giờ khác.");
                ViewBag.BacSis = await _context.BacSis.ToListAsync();
                return View(benhNhan);
            }

            var lichKham = new LichKham
            {
                BenhNhanId = bn.Id,
                BacSiId = bacSiId,
                NgayKham = ngayKham,
                TrangThai = "Chưa khám"
            };

            _context.LichKhams.Add(lichKham);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đặt lịch thành công!";
            return RedirectToAction(nameof(LichSuKham));
        }

        // ===========================
        // 3) Lịch sử khám
        // ===========================
        public async Task<IActionResult> LichSuKham()
        {
            var userId = _userManager.GetUserId(User);
            var bn = await _context.BenhNhans.FirstOrDefaultAsync(b => b.ApplicationUserId == userId);

            if (bn == null)
                return RedirectToAction(nameof(DatLich));

            var lichKhams = await _context.LichKhams
                .Include(l => l.BacSi)
                .Include(l => l.BenhAn) // để biết lịch nào đã có bệnh án
                .Where(l => l.BenhNhanId == bn.Id)
                .OrderByDescending(l => l.NgayKham)
                .ToListAsync();

            return View(lichKhams);
        }

        // ===========================
        // 4) Xem bệnh án chi tiết
        // ===========================
        public async Task<IActionResult> BenhAn(int lichKhamId)
        {
            var userId = _userManager.GetUserId(User);
            var bn = await _context.BenhNhans.FirstOrDefaultAsync(b => b.ApplicationUserId == userId);
            if (bn == null)
                return RedirectToAction(nameof(DatLich));

            var benhAn = await _context.BenhAns
                .Include(b => b.BacSi)
                .Include(b => b.LichKham)
                .Include(b => b.DonThuocs)
                .FirstOrDefaultAsync(b => b.LichKhamId == lichKhamId && b.BenhNhanId == bn.Id);

            if (benhAn == null)
            {
                TempData["Error"] = "Bệnh án chưa được tạo.";
                return RedirectToAction(nameof(LichSuKham));
            }

            return View(benhAn);
        }
    }
}
