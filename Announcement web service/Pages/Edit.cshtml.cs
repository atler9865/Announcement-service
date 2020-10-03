using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Announcement_web_service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Announcement_web_service.Pages
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Announcement Announcement { get; set; }

        public async Task OnGet(int id)
        {
            Announcement = await _db.Announcements.FindAsync(id);
        }

        public async Task<IActionResult> OnPost()
        {
            if(ModelState.IsValid)
            {
                var AnnouncementFromDb = await _db.Announcements.FindAsync(Announcement.Id);

                AnnouncementFromDb.Tittle = Announcement.Tittle;
                AnnouncementFromDb.Description = Announcement.Description;

                await _db.SaveChangesAsync();

                return RedirectToPage("Index");

            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}