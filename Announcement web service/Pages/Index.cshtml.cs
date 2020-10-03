using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Announcement_web_service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Announcement_web_service.Pages
{
    public class IndexModel : PageModel
    {

        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Announcement> Announcements { get; set; }

        public async Task OnGet()
        {
            Announcements = await _db.Announcements.ToListAsync();
        }

        public async Task<IActionResult> OnPostDelete(int id)
        {
            var announcement = await _db.Announcements.FindAsync(id);

            if(announcement == null)
            {
                return NotFound();
            }

            _db.Announcements.Remove(announcement);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
