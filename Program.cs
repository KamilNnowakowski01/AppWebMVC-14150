using AppDataBase.Data;
using AppModel.Models;
using AppWebMVC.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AppWebMVC.Services.Interfaces;

namespace AppWebMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IComputerServices, ComputerServices>();
            builder.Services.AddScoped<IGraphicsServices, GraphicsServices>();
            builder.Services.AddScoped<IComputersGraphicsServices, ComputersGraphicsServices>();
            builder.Services.AddScoped<IProducerServices, ProducerServices>();

            var app = builder.Build();

            var serviceProvider = app.Services.CreateScope().ServiceProvider;
            InitializeRoles(serviceProvider).Wait();
            InitializeAdminUser(serviceProvider).Wait();
            InitializeDefaultUser(serviceProvider).Wait();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areaRoute",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.MapRazorPages();

            app.Run();
        }
        private static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { RolesTypes.Admin, RolesTypes.User };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task InitializeAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<IdentityUser>>();

            var adminUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123#");

            var userExists = await userManager.FindByEmailAsync(adminUser.Email) != null;
            if (!userExists)
            {
                var result = await userManager.CreateAsync(adminUser);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RolesTypes.Admin);
                }
            }
        }

        private static async Task InitializeDefaultUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher<IdentityUser>>();

            var defaultUser = new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "user@user.com",
                NormalizedUserName = "USER@USER.COM",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            defaultUser.PasswordHash = passwordHasher.HashPassword(defaultUser, "User123#");

            var userExists = await userManager.FindByEmailAsync(defaultUser.Email) != null;
            if (!userExists)
            {
                var result = await userManager.CreateAsync(defaultUser);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, RolesTypes.User);
                }
            }
        }
    }
}
