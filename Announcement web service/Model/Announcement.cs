using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Announcement_web_service.Model
{
    public class Announcement
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tittle { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
