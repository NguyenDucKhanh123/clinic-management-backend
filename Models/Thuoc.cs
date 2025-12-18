namespace ClinicManagementWeb.Models
{
    public class Thuoc
    {
        public int Id { get; set; }
        public string? TenThuoc { get; set; }
        public string? Loai { get; set; }
        public string? NhaSanXuat { get; set; }
        public int SoLuong { get; set; }        // Tổng tồn kho
        public int SoLuongTon { get; set; }     // ⭐ Số lượng còn lại
        public string? DonViTinh { get; set; }
        public decimal DonGia { get; set; }
        public string? CongDung { get; set; }
    }
}
