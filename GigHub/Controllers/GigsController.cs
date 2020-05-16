using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        // GET: Gigs
        public ActionResult Index()
        {
            return View(db.Gigs.ToList());
        }

        // GET: Gigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gig gig = db.Gigs.Find(id);
            if (gig == null)
            {
                return HttpNotFound();
            }
            return View(gig);
        }

        // GET: Gigs/Create
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigsViewModel
            {
                Genres = db.Genres.ToList()
            };
            return View("Create", viewModel);
        }

        // POST: Gigs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigsViewModel GigView)
        {
            if (!ModelState.IsValid)
            {
                GigView.Genres = db.Genres.ToList();
                return View( GigView);
            }

            var gigs = new Gig()
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = GigView.GetDateTime(),
                Venue = GigView.Venue,
                GenreId = GigView.Genre
            };
            db.Gigs.Add(gigs);
            db.SaveChanges();

            return RedirectToAction("Create", "Gigs");
        }
        [Authorize]
        public ActionResult Attend()
        {
            var userId = User.Identity.GetUserId();
            var gigs = db.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Gig)
                .Include(g=>g.Artist)
                .Include(g=>g.Genre)
                .ToList();
            var viewModel = new HomeViewModel()
            {
                UpcomingGigs = gigs,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
            
        }
        
        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var following = db.Followings
                .Where(f => f.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();
            
            return View();
        }
        [Authorize]
        public ActionResult MineGigs()
        {
            var userId = User.Identity.GetUserId();
            var myGigs = db.Gigs.Where(a => a.ArtistId == userId && a.DateTime > DateTime.Now && !a.IsCanceled).Include(g=>g.Genre).ToList();
            return View(myGigs);
        }

        // GET: Gigs/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = db.Gigs.Single(a => a.Id == id && a.ArtistId == userId);
            var viewModel = new GigsViewModel
            {
                Id = gig.Id,
                Genres = db.Genres.ToList(),
                Date = gig.DateTime.ToString("d MMM yyyy"),
                Time = gig .DateTime.ToString("HH:mm"),
                Venue = gig.Venue,
                Genre = gig.GenreId
            };
            return View( viewModel);
        }

        // POST: Gigs/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(GigsViewModel GigView)
        {
            if (!ModelState.IsValid)
            {
                GigView.Genres = db.Genres.ToList();
                return View("Edit", GigView);
            }
            var userId = User.Identity.GetUserId();
            var gigs = db.Gigs
                .Include(a=>a.Attendances.Select(g=>g.Attendee))
                .Single(g => g.Id == GigView.Id && g.ArtistId == userId);

            gigs.Mofify(GigView.GetDateTime(), GigView.Venue, GigView.Genre);

            db.SaveChanges();

            return RedirectToAction("MineGigs", "Gigs");
        }

        // GET: Gigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gig gig = db.Gigs.Find(id);
            if (gig == null)
            {
                return HttpNotFound();
            }
            return View(gig);
        }

        // POST: Gigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gig gig = db.Gigs.Find(id);
            db.Gigs.Remove(gig);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}