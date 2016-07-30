using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
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
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres;
                return View(model);
            }

            var gig = new Gig
            {
                Venue = model.Venue,
                GenreId = model.Genre,
                UserId = User.Identity.GetUserId(),
                DateTime = model.GetDateTime()
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}