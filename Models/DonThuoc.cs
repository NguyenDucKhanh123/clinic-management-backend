using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementWeb.Models
{
    public class DonThuoc
    {
        [Key]
        public int Id { get; set; }

        // 🔗 Liên kết đến Bệnh án
        public int BenhAnId { get; set; }
        [ForeignKey("BenhAnId")]
        public BenhAn? BenhAn { get; set; }

        // 🔗 Liên kết đến thuốc (nếu bạn có bảng Thuoc)
        public int? ThuocId { get; set; }
        [ForeignKey("ThuocId")]
        public Thuoc? Thuoc { get; set; }

        // 🧾 Thông tin chi tiết
        [Required]
        public string TenThuoc { get; set; } = string.Empty;

        public string? LieuDung { get; set; }
        public int? SoNgay { get; set; }
        public int SoLuong { get; set; }
        public string? DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public string? CachDung { get; set; }
        public string? GhiChu { get; set; }
    }
}
