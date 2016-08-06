using System.Linq;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers.Apis
{
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Attend([FromBody] int gigId)
        {
            var userId = User.Identity.GetUserId();

            var extistedAttending = _context.Attendances
                                            .Where(a => a.GigId == gigId && a.AttendeeId == userId)
                                            .ToList();
            if (extistedAttending.Any())
            {
                return BadRequest("Already attending this requested gig");
            }

            var attendingGig = _context.Gigs
                                       .SingleOrDefault(g => g.Id == gigId);

            if (attendingGig == null)
                return BadRequest("The requested gig does not exist");

            var attendance = new Attendance
            {
                GigId = attendingGig.Id,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}
