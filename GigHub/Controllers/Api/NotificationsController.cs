using AutoMapper;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;


namespace GigHub.Controllers.Api
{
    public class NotificationsController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        public IEnumerable<NotificationDto> GetNewNotification()
        {
            var UserId = User.Identity.GetUserId();
            var notfications = db.UserNotifications
                .Where(a => a.UserId == UserId && ! a.IsRead)
                .Select(a => a.Notification)
                .Include(a => a.Gig.Artist)
                .ToList();
            
            return notfications.Select(Mapper.Map<Notification, NotificationDto>);
            
        }

        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = db.UserNotifications
                .Where(a => a.UserId == userId && !a.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());
            db.SaveChanges();
            return Ok();
        }
    }
}
