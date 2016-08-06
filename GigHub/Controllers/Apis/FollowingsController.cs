using System.Linq;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Apis
{
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Follow([FromBody] string followerId)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings
                        .Any(f => f.FolloweeId == userId && f.FollowerId == followerId))
            {
                return BadRequest("Already follow the artist");
            }

            var follower = _context.Users.SingleOrDefault(u => u.Id == followerId);

            if (follower == null)
            {
                return BadRequest("Artist dooes not exist");
            }

            var following = new Following
            {
                FolloweeId = userId,
                FollowerId = follower.Id
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
