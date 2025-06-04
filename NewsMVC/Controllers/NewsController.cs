using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsMVC.Data;
using NewsMVC.Models;

namespace NewsMVC.Controllers
{

    public class NewsController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;


        public async Task<IActionResult> News() => View(await _db.News.Include(p => p.Autor).ToListAsync());
        public async Task<IActionResult> Details(string id) => await ReturnDbItembyId(id);
        [Authorize]
        public async Task<IActionResult> Delete(string id) => await ReturnDbItembyId(id);
        [Authorize]
        public async Task<IActionResult> Edit(string id) => await ReturnDbItembyId(id);


        [HttpPost]
        public async Task<IActionResult> Edit(New MyNew)
        {

            // New
            if (MyNew.Autor == null && User.Identity != null)
            {
                MyNew.Autor = await _db.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                ModelState.Clear();
            }


            if (!ModelState.IsValid) return View(MyNew);



            // add (NEW) to db
            if (MyNew.Id == 0) await _db.AddAsync(MyNew);
            // update (NEW) in database
            else  _db.Update(MyNew);

            await _db.SaveChangesAsync();
            return RedirectToAction("News", "News");
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(string id)
        {
            if (int.TryParse(id, out int intId))
            {
                var oneNew = await _db.News.FindAsync(intId);
                if (oneNew != null)
                {
                    _db.News.Remove(oneNew);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToAction("News", "News");
        }


        private async Task<IActionResult> ReturnDbItembyId(string id)
        {
            if (int.TryParse(id, out int intId)) return View(await _db.News.Include(p => p.Autor).FirstOrDefaultAsync(p => p.Id == intId));
            return View();
        }
    }
}
