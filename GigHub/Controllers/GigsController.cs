using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{

    public class GigsController : Controller
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
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
        [Authorize]
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

            return RedirectToAction("UpComingGigs", "Gigs");
        }

        public ActionResult UpComingGigs()
        {
            var upcomingGigs = _context.Gigs
                                       .Where(g => g.DateTime > DateTime.Now)
                                       .Include(g => g.Genre)
                                       .Include(g => g.Artist)
                                       .ToList();

            var model = new UpComingGigsViewModel
            {
                ShowingTheActions = User.Identity.IsAuthenticated,
                UpcomingGigs = upcomingGigs
            };
            return View(model);
        }
    }
}