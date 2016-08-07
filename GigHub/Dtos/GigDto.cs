using System;

namespace GigHub.Dtos
{
    public class GigDto
    {
        public DateTime DateTime { get; set; }

        public string Venue { get; set; }

        public GenreDto Genre { get; set; }

        public bool IsCanceled { get; set; }

        public UserDto Artist { get; set; }
    }
}