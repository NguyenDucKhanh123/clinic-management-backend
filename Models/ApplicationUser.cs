using Microsoft.AspNetCore.Identity;
using System;

namespace ClinicManagementWeb.Models
{
    // ApplicationUser sẽ mở rộng từ IdentityUser (cột AspNetUsers trong DB)
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
