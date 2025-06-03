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

        //public string Autor1 { get; set; } = default!;



        public async Task<IActionResult> News()
        {
            string Autor5 = "vcvxvcx";


            return View(await _db.News.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Delete(string id) => await ReturnDbItembyId(id);
        public async Task<IActionResult> Details(string id) => await ReturnDbItembyId(id);
        [Authorize]
        public async Task<IActionResult> Edit(string id) => await ReturnDbItembyId(id);


        [HttpPost]
        public async Task<IActionResult> Edit(New MyNew,string id = "")
        {
            if(string.IsNullOrEmpty(id) && User.Identity != null)
            {
                MyNew.Autor = await _db.Users.FirstOrDefaultAsync(x => x.UserName == User.Identity.Name);
                ModelState.Clear();
            }
            
            if (!ModelState.IsValid) return View(MyNew);



            // update NEW in database
            if (!string.IsNullOrEmpty(id)) _db.Update(MyNew);
            // add NEW to db
            else {
                await _db.AddAsync(MyNew);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("News", "News");
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(string id)
        {
            if (int.TryParse(id, out int intId))
            {
                var oneNew = await _db.News.FindAsync(intId);
                if(oneNew != null)
                {
                   _db.News.Remove(oneNew);
                   await _db.SaveChangesAsync();
                }
            }

            return RedirectToAction("News", "News");
        }


        private async Task<IActionResult> ReturnDbItembyId(string id)
        {
            if (int.TryParse(id, out int intId)) return View(await _db.News.FindAsync(intId));
            return View();
        }









    }
}
