using ClinicManagementWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementWeb.Data
{
    // ApplicationDbContext kế thừa IdentityDbContext để quản lý user (ApplicationUser)
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Khai báo các bảng trong database
        public DbSet<Thuoc> Thuocs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<BenhNhan> BenhNhans { get; set; }
        public DbSet<BenhAn> BenhAns { get; set; }
        public DbSet<DonThuoc> DonThuocs { get; set; }
        public DbSet<LichKham> LichKhams { get; set; }

        public DbSet<BacSi> BacSis { get; set; }

        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<BenhAnDichVu> BenhAnDichVus { get; set; }



    }
}
