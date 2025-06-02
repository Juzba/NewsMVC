using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVC.Data;
using NewsMVC.Models;

namespace NewsMVC.Controllers
{
    public class NewsController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<IActionResult> News() => View(await _db.News.ToListAsync());
        public IActionResult Edit() => View();

        public async Task<IActionResult> Details(string id)
        {
            if (int.TryParse(id, out int intId)) return View(await _db.News.FindAsync(intId));
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(New MyNew)
        {
            if (!ModelState.IsValid) return View(MyNew);

            await _db.AddAsync(MyNew);
            await _db.SaveChangesAsync();

            return RedirectToAction("News", "News");
        }
    }
}
