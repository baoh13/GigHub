﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GigHub.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public int GigId { get; set; }

        public Gig Gig { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ArtistId { get; set; }

        public ApplicationUser Artist { get; set; }
    }
}