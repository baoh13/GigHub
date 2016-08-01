using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult MyUpComingGigs()
        {
            var userId = User.Identity.GetUserId();
            var myUpcomingGigs = _context.Gigs
                                         .Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                                         .Include(g => g.Genre)
                                         .Include(g => g.Artist)
                                         .ToList();

            var gigsViewModel = new GigsViewModel
            {
                Header = "My Upcoming Gigs",
                ShowingTheActions = User.Identity.IsAuthenticated,
                UpcomingGigs = myUpcomingGigs
            };

            return View(gigsViewModel);
        }

        public ActionResult Create()
        {
            var gigFormViewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };

            return View(gigFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                return View(model);
            }

            var gig = new Gig
            {
                Venue = model.Venue,
                GenreId = model.Genre,
                ArtistId = User.Identity.GetUserId(),
                DateTime = model.GetDateTime()
            };
            
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("MyUpcomingGigs", "Gigs");
        }

        public ActionResult AttendingGigs()
        {
            var userId = User.Identity.GetUserId();
            var attendingGigs = _context.Attendances
                                        .Where(a => a.AttendeeId == userId)
                                        .Select(a => a.Gig)
                                        .Include(a => a.Artist)
                                        .Include(a => a.Genre)
                                        .ToList();

            var gigsViewModel = new GigsViewModel
            {
                ShowingTheActions = User.Identity.IsAuthenticated,
                UpcomingGigs = attendingGigs,
                Header = "Gigs I am attending"
            };

            return View("GigsView", gigsViewModel);
        }
    }
}