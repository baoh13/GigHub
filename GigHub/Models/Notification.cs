using GigHub.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime? OriginalDateTime { get; set; }

        public string Venue { get; set; }

        public string OriginalVenue { get; set; }

        public NotificationType NotificationType { get; set; }
        public Gig Gig { get; set; }

        [Required]
        public int GigId { get; set; }
    }
}