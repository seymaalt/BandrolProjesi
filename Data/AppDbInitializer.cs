using Microsoft.AspNetCore.Identity;
using ProjeBandrol.Data.Static;
using ProjeBandrol.Models;

namespace ProjeBandrol.Data
{
    public class AppDbInitializer
    {
    

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
               

                string adminUserEmail = "yonetici@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Ad = "Admin User",
                        Soyad = "admin-user",
                        TcKimlikNo = "0",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Sifre123*");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }



            
            }
        }
    }
}
