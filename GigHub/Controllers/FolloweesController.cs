using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class FolloweesController : Controller
    {
        private ApplicationDbContext _context;

        public FolloweesController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var followedArtists = _context.Followings
                                          .Where(f => f.FolloweeId == userId)
                                          .Select(f => f.Follower)
                                          .ToList();

            return View(followedArtists);
        }
    }
}