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
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
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

            return RedirectToAction("UpComingGigs", "Gigs");
        }

        public ActionResult UpComingGigs()
        {
            var upcomingGigs = _context.Gigs
                                       .Where(g => g.DateTime > DateTime.Now)
                                       .Include(g => g.Genre)
                                       .Include(g => g.Artist)
                                       .ToList();
            return View(upcomingGigs);
        }
    }
}