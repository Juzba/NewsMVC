using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVC.Data;
using NewsMVC.Models;
using NuGet.Protocol;

namespace NewsMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly UserManager<IdentityUser> _userManager = userManager;


        public async Task<IActionResult> AdminPage()
        {
            IList<UserDataForAdmin> userData;
            //var neco = _roleManager.Roles.ToList() ;
            


            userData = await _db.Users.Select(p => new UserDataForAdmin 
            { 
                UserName = p.UserName,
                Email = p.Email,
                BanEnds = p.LockoutEnd
            }).ToListAsync();



            return View(userData);
        }
    }
}
