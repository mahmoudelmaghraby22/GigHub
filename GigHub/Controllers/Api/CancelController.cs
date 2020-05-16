using GigHub.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Resources;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    public class CancelController : ApiController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        [HttpDelete]
        public IHttpActionResult cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = db.Gigs
                .Include(a=>a.Attendances.Select(g=>g.Attendee))
                .Single(a => a.Id == id && a.ArtistId == userId);

            if (gig.IsCanceled)
                return NotFound();
            gig.cancel();
            
            db.SaveChanges();
            return Ok();
        }
    }
}
