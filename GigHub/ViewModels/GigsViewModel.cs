using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.ViewModels
{
    public class GigsViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)] 
        public string Venue { get; set; }
        [Required]
        [FutureDate]
        public string Date { get; set; }
        [Required]
        [validTime]
        public string Time { get; set; }
        [Required]
        public int Genre { get; set; }
        public IEnumerable<Genre> Genres { get; set; }

        public DateTime GetDateTime()
        {
           return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}