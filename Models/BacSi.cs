public class BacSi
{
    public int Id { get; set; }
    public string UserId { get; set; }  // Liên kết AspNetUsers
    public string HoTen { get; set; }
    public string ChuyenKhoa { get; set; }
    public string BangCap { get; set; }
    public string SoDienThoai { get; set; }

    public string? Email { get; set; }
}
