using GigHub.Models;
using GigHub.Models.Enums;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Apis
{
    [Authorize]
    public class GigsController : ApiController
    {
        private ApplicationDbContext _context;

        public GigsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var gig = _context.Gigs.Single(g => g.Id == id
                                                && g.ArtistId == userId
                                                && !g.IsCanceled);

                if (gig == null)
                    return NotFound();

                gig.IsCanceled = true;

                var notification = new Notification
                {
                    DateTime = DateTime.Now,
                    NotificationType = NotificationType.GigCanceled,
                    GigId = gig.Id
                };

                var attendees = _context.Attendances.Where(g => g.GigId == id)
                                                    .Select(a => a.Attendee)
                                                    .ToList();

                foreach (var attendee in attendees)
                {
                    var userNotification = new UserNotification
                    {
                        UserId = attendee.Id,
                        NotificationId = notification.Id
                    };

                    _context.UserNotifications.Add(userNotification);
                }

                _context.Notifications.Add(notification);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            
        }
    }
}
