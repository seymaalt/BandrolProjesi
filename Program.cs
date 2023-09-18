using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjeBandrol.Data;
using ProjeBandrol.Data.Services;
using ProjeBandrol.Models;
namespace ProjeBandrol
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireUppercase = false;
            });

       

            builder.Services.AddScoped<IVehicleBandrolService, VehicleBandrolService>();
            builder.Services.AddScoped<IHomeService, HomeService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();;
            app.UseAuthorization();
            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            using (var scope = app.Services.CreateScope())
            {
                var roleManager =
                    scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Yonetici", "Kullanici" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

            }

            using (var scope = app.Services.CreateScope())
            {
                var userManager =
                    scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                string email = "yonetici43@yonetici.com";
                string password = "Yonetici1Sifre123*";
                string Ad = "yonetici1";
                string Soyad = "yonetici1";
                string TcKimlikNo = "12221";

                if (await userManager.FindByEmailAsync(email) == null)
                {
                    var user = new AppUser();

                    user.Email = email;
                    user.UserName = email;
                    user.Ad = Ad;
                    user.Soyad = Soyad;
                    user.TcKimlikNo = TcKimlikNo;

                    await userManager.CreateAsync(user, password);

                    await userManager.AddToRoleAsync(user, "Yonetici");
                }


                }

                app.Run(); 

        }
    }
}
