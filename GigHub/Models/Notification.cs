using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get; private set; }
        public DateTime? OriginalDateTime { get;private set; }
        public string OriginalVenue { get;private set; }

        [Required]
        public Gig Gig { get;private set; }

        private Notification(NotificationType type, Gig gig)
        {
            if (gig == null)
                throw new ArgumentNullException("gig");

            Type = type;
            Gig = gig;
            DateTime = DateTime.Now;
        }

        protected Notification()
        {
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.getCreated, gig);
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.getCanceled, gig);
        }
        public static Notification GigUpdated(Gig newgig , DateTime OriginaldateTime ,string OriginalVenue)
        {
            var notification = new Notification(NotificationType.getUpdated, newgig);
            notification.OriginalDateTime = OriginaldateTime;
            notification.OriginalVenue = OriginalVenue;

            return notification;
        }

    }
}