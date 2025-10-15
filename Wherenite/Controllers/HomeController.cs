using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wherenite.Models;

namespace Wherenite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        public ActionResult Index(int ?venueTypeId, int? venueId)
        {
            ViewBag.VenueTypes = new SelectList(db.VenueTypes, "Id", "Name", venueTypeId);
            List<Event> filteredEvents = new List<Event>();

            if (venueTypeId.HasValue)
            {
                ViewBag.Venues = new SelectList(db.Venues.Where(e => e.VenueTypeId == venueTypeId), "Id", "Name", venueId);
            }
            else
            {
                ViewBag.Venues = new SelectList(db.Venues, "Id", "Name", venueId);
            }


            if (venueTypeId.HasValue && venueId.HasValue)
            {
                filteredEvents = db.Events.Where(e => e.Venue.VenueTypeId == venueTypeId && e.Venue.Id == venueId).ToList();
                if (filteredEvents.Count == 0)
                {
                    filteredEvents = db.Events.Where(e => e.Venue.VenueTypeId == venueTypeId).ToList();
                }
            }
            else if (venueTypeId.HasValue && !venueId.HasValue)
            {
                filteredEvents = db.Events.Where(e => e.Venue.VenueTypeId == venueTypeId).ToList();
            }
            else if (!venueTypeId.HasValue && venueId.HasValue)
            {
                filteredEvents = db.Events.Where(e => e.Venue.Id == venueId).ToList();
            }
            else
            {
                filteredEvents = new List<Event>();
            }
                return View(filteredEvents);           
        }

        [AllowAnonymous]


        public ActionResult About()
        {
            //ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}