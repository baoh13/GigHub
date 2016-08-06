using GigHub.ViewModels;
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

            var notification = Notification.GigCanceled(gig: this);

            Notify(notification);
        }

        public void Update(GigFormViewModel model)
        {
            var notification = Notification.GigUpdated(
                gig: this, originalDateTime: DateTime, originalVenue: Venue);

            DateTime = model.GetDateTime();
            Venue = model.Venue;
            GenreId = model.Genre;
        }

        private void Notify(Notification notification)
        {
            foreach (var attendance in Attendances)
            {
                attendance.Attendee.Notify(notification);
            }
        }

        public void Create()
        {
            var notification = Notification.GigCreated(this);
            Notify(notification: notification);
        }
    }
}