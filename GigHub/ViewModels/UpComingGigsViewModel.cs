using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModels
{
    public class UpComingGigsViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowingTheActions { get; set; }
    }
}