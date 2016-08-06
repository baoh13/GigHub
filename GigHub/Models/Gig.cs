using GigHub.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string Venue { get; set; }

        public Genre Genre { get; set; }

        public bool IsCanceled { get; private set; }

        [Required]
        public int GenreId { get; set; }

        public ApplicationUser Artist { get; set; }

        [Required]
        public string ArtistId { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = new Notification(notificationType: NotificationType.GigCanceled, gig: this);

            foreach (var attendance in Attendances)
            {
                attendance.Attendee.Notify(notification);
            }
        }
    }
}