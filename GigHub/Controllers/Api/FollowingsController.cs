
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;

namespace GigHub.Controllers.Api
{
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
           
            if (db.Followings.Any(f => f.FolloweeId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("Following is allready Existes");

                var following = new Following
                {
                    FollowerId = userId,
                    FolloweeId = dto.FolloweeId 
                };
            db.Followings.Add(following);
            db.SaveChanges();
            return Ok();
        }

        // GET: api/Followings
        [HttpGet]
        public IEnumerable<Following> GetFollowings()
        {
            return db.Followings.ToList();
        }

        //// GET: api/Followings/5

        //[ResponseType(typeof(Following))]
        //public IHttpActionResult GetFollowing(string id)
        //{
        //    Following following = db.Followings.Find(id);
        //    if (following == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(following);
        //}

        //// PUT: api/Followings/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutFollowing(string id, Following following)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != following.FollowerId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(following).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FollowingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Followings
        //[ResponseType(typeof(Following))]
        //public IHttpActionResult PostFollowing(Following following)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Followings.Add(following);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (FollowingExists(following.FollowerId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = following.FollowerId }, following);
        //}

        //// DELETE: api/Followings/5
        //[ResponseType(typeof(Following))]
        //public IHttpActionResult DeleteFollowing(string id)
        //{
        //    Following following = db.Followings.Find(id);
        //    if (following == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Followings.Remove(following);
        //    db.SaveChanges();

        //    return Ok(following);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool FollowingExists(string id)
        //{
        //    return db.Followings.Count(e => e.FollowerId == id) > 0;
        //}
    }
}