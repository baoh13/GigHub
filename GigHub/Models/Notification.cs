using GigHub.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public DateTime? OriginalDateTime { get; set; }

        public string Venue { get; set; }

        public string OriginalVenue { get; set; }

        public NotificationType NotificationType { get; private set; }

        public Gig Gig { get; private set; }

        [Required]
        public int GigId { get; private set; }

        public Notification(NotificationType notificationType, Gig gig)
        {
            if (gig == null)
            {
                throw new ArgumentNullException(nameof(gig));
            }

            DateTime = DateTime.Now;
            NotificationType = notificationType;
            Gig = gig;
        }
    }
}