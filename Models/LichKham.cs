using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementWeb.Models
{
    public class LichKham
    {
        [Key]
        public int Id { get; set; }

        public int BenhNhanId { get; set; }
        [ForeignKey("BenhNhanId")]
        public BenhNhan? BenhNhan { get; set; }

        public int? BacSiId { get; set; }
        [ForeignKey("BacSiId")]
        public BacSi? BacSi { get; set; }

        public DateTime NgayKham { get; set; }
        public string? TrangThai { get; set; }

        // ⭐ LIÊN KẾT 1 - 1 VỚI BỆNH ÁN
        public BenhAn? BenhAn { get; set; }

        // 🔹 Tên bác sĩ hiển thị (tùy chọn)
        [StringLength(100)]
        public string? BacSiTen { get; set; }
    }
}
