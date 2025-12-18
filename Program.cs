using ClinicManagementWeb.Data;      // namespace của ApplicationDbContext + SeedRoles
using ClinicManagementWeb.Models;    // namespace chứa ApplicationUser
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// 1. Kết nối SQL Server
// ==========================
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ==========================
// 2. Cấu hình Identity (Đăng ký, Đăng nhập, Role)
// ==========================
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ==========================
// 3. Thêm MVC
// ==========================
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ==========================
// 4. Seed Roles khi khởi động
// ==========================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedRoles.Initialize(services);
}

// ==========================
// 5. Reset mật khẩu cho các tài khoản
// ==========================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] emails = new[]
    {
        "nguyenduckhanh26082002@gmail.com",
        "khanh.nd11246@sinhvien.hoasen.edu.vn",
        "khoa.ld04882@sinhvien.hoasen.edu.vn",
        "duy.lq16183@sinhvien.hoasen.edu.vn",
        "thinh.hh01928@sinhvien.hoasen.edu.vn"
    };

    string newPassword = "NewPass123!"; // nhớ thêm ký tự đặc biệt

    foreach (var email in emails)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
        {
            // Xóa mật khẩu cũ (nếu có)
            var removePassword = await userManager.RemovePasswordAsync(user);
            if (!removePassword.Succeeded)
            {
                Console.WriteLine($"⚠️ Không thể xóa mật khẩu cũ của {email}: {string.Join(", ", removePassword.Errors.Select(e => e.Description))}");
                continue;
            }

            // Đặt mật khẩu mới
            var addPassword = await userManager.AddPasswordAsync(user, newPassword);
            if (addPassword.Succeeded)
                Console.WriteLine($"✅ Đặt lại mật khẩu cho {email} → {newPassword}");
            else
                Console.WriteLine($"❌ Lỗi khi đặt mật khẩu cho {email}: {string.Join(", ", addPassword.Errors.Select(e => e.Description))}");
        }
        else
        {
            Console.WriteLine($"⚠️ Không tìm thấy user: {email}");
        }
    }
}

    // ==========================
    // 6. Middleware pipeline
    // ==========================
    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Quan trọng: Bật xác thực + phân quyền
app.UseAuthentication();
app.UseAuthorization();

// ==========================
// 7. Route mặc định
// ==========================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
