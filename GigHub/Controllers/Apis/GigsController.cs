﻿using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
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
            try
            {
                var userId = User.Identity.GetUserId();
                var gig = _context.Gigs.Include(g => g.Attendances.Select(a => a.Attendee))
                                       .Single(g => g.Id == id
                                                && g.ArtistId == userId
                                                && !g.IsCanceled);

                if (gig == null)
                    return NotFound();

                gig.Cancel();

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
            
        }
    }
}
