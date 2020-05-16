using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Dto
{
    public class NotificationDto
    {
        public int Id { get; private set; }
        public DateTime DateTime { get;  set; }
        public NotificationType Type { get;  set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalVenue { get; set; }
        public GigDto Gig { get;set; }
    }
}