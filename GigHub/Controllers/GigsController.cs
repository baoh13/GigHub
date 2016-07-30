using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System;
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
                Genres = _context.Genres
            };

            return View(gigFormViewModel);
        }

        [HttpPost]
        public ActionResult Create(GigFormViewModel model)
        {
            var userId = User.Identity.GetUserId();

            var gig = new Gig
            {
                Venue = model.Venue,
                GenreId = model.Genre,
                UserId = userId,
                DateTime = DateTime.Parse(string.Format("{0} {1}", model.Date, model.Time))
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}