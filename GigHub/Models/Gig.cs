using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        public bool IsCanceled { get;private set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }
        
        public Genre Genre { get; set; }

        [Required]
        public int GenreId { get; set; }

        public ICollection<Attendance> Attendances { get;private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }
        public void cancel()
        {
            IsCanceled = true;
            var notification = Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(g => g.Attendee))
            {
                attendee.Notify(notification);
            }   
        }
        public void Mofify(DateTime dateTime , string venue , int genre)
        {
            var notiffication = Notification.GigUpdated(this, dateTime, venue);
            
            Venue =venue;
            GenreId = genre;
            DateTime = dateTime;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notiffication);
        }
    }
}