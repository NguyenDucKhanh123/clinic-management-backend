using System.ComponentModel.DataAnnotations;

namespace ClinicManagementWeb.Models
{
    public class DichVu
    {
        public int Id { get; set; }

        [Required]
        public string TenDichVu { get; set; } = string.Empty;

        public decimal DonGia { get; set; }

        public string? MoTa { get; set; }
    }
}
