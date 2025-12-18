using System.ComponentModel.DataAnnotations.Schema;

namespace ClinicManagementWeb.Models
{
    public class BenhAnDichVu
    {
        public int Id { get; set; }

        // 🔗 Bệnh án
        public int BenhAnId { get; set; }
        [ForeignKey("BenhAnId")]
        public BenhAn? BenhAn { get; set; }

        // 🔗 Dịch vụ
        public int DichVuId { get; set; }
        [ForeignKey("DichVuId")]
        public DichVu? DichVu { get; set; }

        // Số lượng (nếu cần)
        public int SoLuong { get; set; } = 1;

        // 💰 Giá dịch vụ tại thời điểm khám
        public decimal DonGia { get; set; }
    }
}
