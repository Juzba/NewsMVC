using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsMVC.Data;

namespace NewsMVC.Code
{
    public class Seed
    {
        public const string EditorRoleName = "editor";
        public const string AdminRoleName = "admin";

        readonly ApplicationDbContext _db;
        readonly UserManager<IdentityUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;

        public Seed(ApplicationDbContext db,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            _db.Database.MigrateAsync().Wait();
            InitUsers().Wait();
        }

        async Task InitUsers()
        {
            //Přidání rolí
            var adminRole = await FindOrAddRole(AdminRoleName);
            await FindOrAddRole(EditorRoleName);

            // Přidání výchozího uživatele
            var adminUser = await _userManager.FindByNameAsync("Juzba88@gmail.com");
            if(adminUser == null)
            {
                adminUser = new IdentityUser()
                {
                    UserName = "Juzba88@gmail.com",
                    Email = "Juzba88@gmail.com",
                    EmailConfirmed = true
                };
                await _userManager.CreateAsync(adminUser, "123456");
            }

            //Přidání role admina uživateli
            bool isAdmin = await _userManager.IsInRoleAsync(adminUser, AdminRoleName);
            if (!isAdmin)
                await _userManager.AddToRoleAsync(adminUser, AdminRoleName);
        }

        async Task<IdentityRole> FindOrAddRole(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if(role == null)
            {
                role = new IdentityRole(roleName);
                await _roleManager.CreateAsync(role);
            }
            return role;
        }

    }
}
