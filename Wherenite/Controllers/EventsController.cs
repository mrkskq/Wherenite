using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wherenite.Models;

namespace Wherenite.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]

        // GET: Events
        public ActionResult Index(string id)
        {
            //var events = db.Events.Include(e => e.Venue);
            //return View(events.ToList());
            List<Event> events = new List<Event>();

            if (!string.IsNullOrEmpty(id))
            {
                events = db.Events.Where(e => e.Description == id).ToList();
                if(id.Equals("Сите"))
                {
                    events = db.Events.Include(e => e.Venue).ToList();
                }
            }
            else
            {
                events = db.Events.Include(e => e.Venue).ToList();
            }

            return View(events);
        }

        [AllowAnonymous]
        public PartialViewResult FilterByCategory(string category)
        {
            var events = db.Events.AsQueryable();
            if(!string.IsNullOrEmpty(category))
            {
                ViewBag.Category = category;
                if (category.Equals("Сите"))
                {
                    events = db.Events.Include(e => e.Venue);
                }
                else
                {
                    events = events.Where(e => e.Description == category);
                }
            }
            return PartialView("_EventsTablePartial", events.ToList());
        }

        [AllowAnonymous]

        //GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        [Authorize(Roles = "Admin")]

        public ActionResult AddSong()
        {
            ViewBag.Events = new SelectList(db.Events.OrderBy(e => e.Title).ToList(), "Id", "Title");
            //Event ev = db.Events.Find(id);
            //if (ev == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.EventId = ev.Id;
            //ViewBag.EventTitle = ev.Title;
            return View();
        }

        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSong(Song song, int SelectedEventId)
        {
            if (ModelState.IsValid)
            {
                var ev = db.Events.Find(SelectedEventId);
                if (ev == null)
                {
                    return HttpNotFound();
                }

                song.EventId = SelectedEventId;
                db.Songs.Add(song);
                db.SaveChanges();

                return RedirectToAction("Details", new { id = song.EventId });

            }
            return View(song);
        }

        [Authorize(Roles = "Admin")]

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name");
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,About,Date,Description,Price,VenueId,EventImage")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        [Authorize(Roles = "Admin")]

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,About,Date,Description,Price,VenueId,EventImage")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VenueId = new SelectList(db.Venues, "Id", "Name", @event.VenueId);
            return View(@event);
        }

        [Authorize(Roles = "Admin")]

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event e = db.Events.Find(id);
            if (e == null)
            {
                return HttpNotFound();
            }
            db.Events.Remove(e);
            db.SaveChanges();
            return RedirectToAction("Index");
            //return View(@event);

        }

        [Authorize(Roles = "Admin")]

        public ActionResult DeleteSong(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var song = db.Songs.FirstOrDefault(s => s.Id == id);
            if (song == null)
            {
                return HttpNotFound();
            }
            db.Songs.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Details", "Events", new { id = song.EventId });
            //return View(@event);

        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
