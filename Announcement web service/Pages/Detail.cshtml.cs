using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Announcement_web_service.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Announcement_web_service.Pages
{
    public class DetailModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DetailModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Announcement Announcement { get; set; }

        public void OnGet(int id)
        {
            Announcement = _db.Announcements.Find(id);
        }
    }
}