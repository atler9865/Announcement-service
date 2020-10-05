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

        public IEnumerable<Announcement> Announcements { get; set; }
        public IEnumerable<int> ThreeSimilarAnnouncementsId { get; set; }
        public List<Announcement> ThreeSimilarAnnouncements { get; set; }

        public IEnumerable<int> getSimularAnnouncements(Announcement announcement)
        {
            //Select all items from db except selected announcement
            Announcements = _db.Announcements.Where(i => i.Id != announcement.Id).ToList();

            //ititialize dictionary with key = announcement.Id, keyValue = similar words count
            Dictionary<int, int> IdOfSimilarAnnouncements = new Dictionary<int, int>(Announcements.Count());

            //words of selected announcement
            var ourAnnouncementWords = announcement.Tittle.ToLower().Split(' ')
                .Union(announcement.Description.ToLower().Split(' '));

            foreach(var item in Announcements)
            {
                int sameWordsCount = 0;

                var otherAnnouncementWord = item.Tittle.ToString().ToLower().Split(' ')
                .Union(item.Description.ToLower().Split(' '));

                var sameWords = ourAnnouncementWords.Intersect(otherAnnouncementWord);

                sameWordsCount = sameWords.Count();

                if (sameWordsCount == 0)
                {
                    continue;
                }
                else
                {
                    IdOfSimilarAnnouncements.Add(item.Id, sameWordsCount);
                }
                
            }

            //get id of 3 most similar announcement
            IEnumerable<int> ListOfThreeSimilarAnnouncements = IdOfSimilarAnnouncements.OrderByDescending(t => t.Value).Take(3).Select(t => t.Key);

            return ListOfThreeSimilarAnnouncements;

        }

        public void OnGet(int id)
        {
            Announcement = _db.Announcements.Find(id);
            
                ThreeSimilarAnnouncementsId = getSimularAnnouncements(Announcement);

                foreach (var item in ThreeSimilarAnnouncementsId)
                {
                    ThreeSimilarAnnouncements = _db.Announcements.Where(i => ThreeSimilarAnnouncementsId.Contains(i.Id)).ToList();
                }
            
        }
    }
}