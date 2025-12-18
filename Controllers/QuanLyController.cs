using ClinicManagementWeb.Data;
using ClinicManagementWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ClinicManagementWeb.Controllers
{
    [Authorize(Roles = "QuanLy")]
    public class QuanLyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public QuanLyController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ========================= QUẢN LÝ THUỐC =========================
        public async Task<IActionResult> Thuoc(string? search)
        {
            var query = _context.Thuocs.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(t => t.TenThuoc.Contains(search));
            }
            var list = await query.ToListAsync();
            return View(list);
        }

        public IActionResult ThemThuoc() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemThuoc(Thuoc model)
        {
            if (ModelState.IsValid)
            {
                _context.Thuocs.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Thuoc");
            }
            return View(model);
        }

        public async Task<IActionResult> SuaThuoc(int id)
        {
            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc == null) return NotFound();
            return View(thuoc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaThuoc(Thuoc model)
        {
            if (ModelState.IsValid)
            {
                _context.Thuocs.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Thuoc");
            }
            return View(model);
        }

        public async Task<IActionResult> XoaThuoc(int id)
        {
            var thuoc = await _context.Thuocs.FindAsync(id);
            if (thuoc != null)
            {
                _context.Thuocs.Remove(thuoc);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Thuoc");
        }

        // ========================= QUẢN LÝ NHÂN VIÊN =========================
        public async Task<IActionResult> NhanVien()
        {
            var users = _userManager.Users.ToList();
            var nhanViens = await _context.NhanViens.Include(n => n.ApplicationUser).ToListAsync();

            var list = new List<NhanVien>();

            foreach (var u in users)
            {
                var nv = nhanViens.FirstOrDefault(n => n.ApplicationUserId == u.Id);
                if (nv == null)
                {
                    nv = new NhanVien
                    {
                        ApplicationUserId = u.Id,
                        ApplicationUser = u
                    };
                }

                var roles = await _userManager.GetRolesAsync(u);
                nv.ChucVu = roles.FirstOrDefault() ?? nv.ChucVu;

                list.Add(nv);
            }

            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CapNhatNhanVien(string ApplicationUserId, string HoTen, string SDT, decimal? Luong, string PhongBan)
        {
            if (string.IsNullOrWhiteSpace(HoTen))
            {
                TempData["Error"] = "Họ tên không được để trống!";
                return RedirectToAction("NhanVien");
            }

            var nv = await _context.NhanViens.FirstOrDefaultAsync(n => n.ApplicationUserId == ApplicationUserId);

            if (nv == null)
            {
                nv = new NhanVien { ApplicationUserId = ApplicationUserId };
                _context.NhanViens.Add(nv);
            }

            nv.HoTen = HoTen;
            nv.SDT = SDT;
            nv.Luong = Luong;
            nv.PhongBan = PhongBan;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật nhân viên thành công!";
            return RedirectToAction("NhanVien");
        }

        // ========================= PHÁT THUỐC =========================
        public IActionResult PhatThuoc()
        {
            ViewData["Title"] = "Phát thuốc";
            return View();
        }

        // ========================= DOANH THU =========================
        public IActionResult DoanhThu()
        {
            ViewData["Title"] = "Doanh thu";
            return View();
        }

        // ========================= API: Lấy danh sách bệnh án chờ phát thuốc =========================
        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> GetBenhAnChoPhat()
        {
            var list = await _context.BenhAns
                .Include(b => b.BenhNhan)
                .Include(b => b.DonThuocs)
                .Where(b => b.TrangThaiPhatThuoc == "ChoPhat" || b.TrangThaiPhatThuoc == "DaPhat")
                .Select(b => new
                {
                    b.Id,
                    NgayKham = b.NgayKham.ToString("dd/MM/yyyy HH:mm"),
                    BenhNhan = new { b.BenhNhan.HoTen },
                    DonThuocs = b.DonThuocs.Select(d => new
                    {
                        d.TenThuoc,
                        d.SoLuong
                    }).ToList(),
                    TrangThaiPhatThuoc = b.TrangThaiPhatThuoc
                })
                .ToListAsync();

            return Json(list);
        }


        // ========================= API: Xử lý phát thuốc =========================
        [HttpPost]
        public async Task<IActionResult> XacNhanPhatThuoc(int benhAnId)
        {
            var benhAn = await _context.BenhAns
                .Include(b => b.DonThuocs)
                .FirstOrDefaultAsync(b => b.Id == benhAnId);

            if (benhAn == null) return NotFound();

            foreach (var dt in benhAn.DonThuocs)
            {
                if (dt.ThuocId != null)
                {
                    var thuoc = await _context.Thuocs.FindAsync(dt.ThuocId);
                    if (thuoc != null)
                    {
                        thuoc.SoLuongTon -= dt.SoLuong;
                        if (thuoc.SoLuongTon < 0) thuoc.SoLuongTon = 0;
                    }
                }
            }

            benhAn.TrangThaiPhatThuoc = "DaPhat";

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đã phát thuốc thành công!" });
        }
    }
}
