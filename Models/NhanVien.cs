using ClinicManagementWeb.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementWeb.Models;

public class NhanVien
{
    public int Id { get; set; }

    // Liên kết Identity
    public string? ApplicationUserId { get; set; }
    public ApplicationUser? ApplicationUser { get; set; }

    // Thông tin cá nhân do quản lý nhập
    [Required(ErrorMessage = "Họ tên bắt buộc nhập")]
    [StringLength(100)]
    public string? HoTen { get; set; }

    [Phone(ErrorMessage = "SĐT không hợp lệ")]
    [StringLength(20)]
    public string? SDT { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Lương phải >= 0")]
    public decimal? Luong { get; set; }

    [StringLength(50)]
    public string? PhongBan { get; set; }

    // Chức vụ đồng bộ với Role trong Identity
    public string? ChucVu { get; set; }
}
