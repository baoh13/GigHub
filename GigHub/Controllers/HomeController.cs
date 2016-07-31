using GigHub.Models;
using GigHub.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var upcomingGigs = _context.Gigs
                                      .Where(g => g.DateTime > DateTime.Now)
                                      .Include(g => g.Genre)
                                      .Include(g => g.Artist)
                                      .ToList();

            var model = new GigsViewModel
            {
                ShowingTheActions = User.Identity.IsAuthenticated,
                UpcomingGigs = upcomingGigs
            };
            return View(model);
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