using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.UI.WebControls;
using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Api
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize]
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = db.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);
            if (exists)
                return BadRequest("The attedance is allready exists");
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };
            db.Attendances.Add(attendance);
            db.SaveChanges();
            return Ok();
        }
        //// GET: api/Attendances
        //public IQueryable<Attendance> GetAttendances()
        //{
        //    return db.Attendances;
        //}

        //// GET: api/Attendances/5
        //[ResponseType(typeof(Attendance))]
        //public IHttpActionResult GetAttendance(int id)
        //{
        //    Attendance attendance = db.Attendances.Find(id);
        //    if (attendance == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(attendance);
        //} 

        //// PUT: api/Attendances/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAttendance(int id, Attendance attendance)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != attendance.GigId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(attendance).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AttendanceExists(id))
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

        //// POST: api/Attendances
        //[ResponseType(typeof(Attendance))]
        //public IHttpActionResult PostAttendance(Attendance attendance)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Attendances.Add(attendance);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (AttendanceExists(attendance.GigId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = attendance.GigId }, attendance);
        //}

        //// DELETE: api/Attendances/5
        //[ResponseType(typeof(Attendance))]
        //public IHttpActionResult DeleteAttendance(int id)
        //{
        //    Attendance attendance = db.Attendances.Find(id);
        //    if (attendance == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Attendances.Remove(attendance);
        //    db.SaveChanges();

        //    return Ok(attendance);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool AttendanceExists(int id)
        //{
        //    return db.Attendances.Count(e => e.GigId == id) > 0;
        //}
    }
}