using GigHub.Models;
using System.Data.Entity;
using System.Linq;
using GigHub.ViewModels;
 using System.Web.Mvc;
using System;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var upcomingGigs = db.Gigs.Include(g => g.Artist).Include(g=>g.Genre).Where(g => g.DateTime > DateTime.Now );
            var viewModel = new HomeViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}