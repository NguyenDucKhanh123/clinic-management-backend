using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementWeb.Models
{
    public class BenhAn
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime NgayKham { get; set; }

        // 🔗 Quan hệ với Bác sĩ
        public int? BacSiId { get; set; }
        [ForeignKey("BacSiId")]
        public BacSi? BacSi { get; set; }

        // 🔗 Quan hệ với Bệnh nhân
        public int BenhNhanId { get; set; }
        [ForeignKey("BenhNhanId")]
        public BenhNhan? BenhNhan { get; set; }

        // 🔗 Quan hệ với Lịch khám
        public int? LichKhamId { get; set; }
        [ForeignKey("LichKhamId")]
        public LichKham? LichKham { get; set; }

        // 🔍 Thông tin bệnh án
        public string? TrieuChung { get; set; }
        public string? ChanDoan { get; set; }
        public string? TinhTrang { get; set; }
        public string? GhiChu { get; set; }

        public DateTime? HenTaiKham { get; set; }

        // 💊 Danh sách đơn thuốc của bệnh án này
        public List<DonThuoc> DonThuocs { get; set; } = new List<DonThuoc>();

        // ⭐ Trạng thái phát thuốc
        public string? TrangThaiPhatThuoc { get; set; } = "ChoPhat";

        // ⏱ Số giờ bác sĩ khám
        public int SoGioKham { get; set; } = 1;

        // 💰 Giá 1 giờ khám (hiện tại cố định 100.000)
        public decimal DonGiaGioKham { get; set; } = 100000;

        // 🧪 Danh sách dịch vụ đã sử dụng
        public List<BenhAnDichVu> BenhAnDichVus { get; set; } = new();


    }
}
