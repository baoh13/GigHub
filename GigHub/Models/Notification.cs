using GigHub.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string Venue { get; set; }

        public string OriginalVenue { get; private set; }

        public NotificationType NotificationType { get; private set; }

        public Gig Gig { get; private set; }

        [Required]
        public int GigId { get; private set; }

        private Notification(NotificationType notificationType, Gig gig)
        {
            if (gig == null)
            {
                throw new ArgumentNullException(nameof(gig));
            }

            DateTime = DateTime.Now;
            NotificationType = notificationType;
            Gig = gig;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(NotificationType.GigCreated, gig);
        }

        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(NotificationType.GigCanceled, gig);
        }

        public static Notification GigUpdated(Gig gig, DateTime originalDateTime, string originalVenue)
        {
            var notification = new Notification(NotificationType.GigUpdated, gig)
            {
                OriginalDateTime = originalDateTime,
                OriginalVenue = originalVenue
            };

            return notification;
        }
    }
}