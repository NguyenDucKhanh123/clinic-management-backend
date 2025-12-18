namespace ClinicManagementWeb.Models
{
    public class BenhNhan
    {
        public int Id { get; set; }
        public string? ApplicationUserId { get; set; }

        public string? HoTen { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? DiaChi { get; set; }
        public string? Phone { get; set; }
        public string? CCCD { get; set; }
    }
}
