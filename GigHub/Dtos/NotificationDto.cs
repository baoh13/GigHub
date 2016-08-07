using GigHub.Models.Enums;
using System;

namespace GigHub.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string Venue { get; set; }

        public string OriginalVenue { get; set; }

        public NotificationType NotificationType { get; set; }

        public GigDto Gig { get; set; }

    }
}