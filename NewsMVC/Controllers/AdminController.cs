using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVC.Data;
using NewsMVC.Models;

namespace NewsMVC.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController(ApplicationDbContext db, RoleManager<IdentityRole> roleManager) : Controller
    {
        private readonly ApplicationDbContext _db = db;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;


        public async Task<IActionResult> AdminPage()
        {
            IList<UserDataForAdmin> userData;
            //var neco = _roleManager.Roles.Select(p=>p.Name).ToListAsync();

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
