﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
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
            var userId = User.Identity.GetUserId();
            var gig = _context.Gigs.Single(g => g.Id == id 
                                            && g.ArtistId == userId 
                                            && !g.IsCanceled);

            if (gig == null)
                return NotFound();

            gig.IsCanceled = true;
            
            _context.SaveChanges();

            return Ok();
        }
    }
}