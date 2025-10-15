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
    public class TicketCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketCategories
        public ActionResult Index()
        {
            var ticketCategories = db.TicketCategories.Include(t => t.Event);
            return View(ticketCategories.ToList());
        }

        // GET: TicketCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCategory ticketCategory = db.TicketCategories.Find(id);
            if (ticketCategory == null)
            {
                return HttpNotFound();
            }
            return View(ticketCategory);
        }

        // GET: TicketCategories/Create
        public ActionResult Create(int eventId)
        {
            //ViewBag.EventId = new SelectList(db.Events, "Id", "Title");
            //return View();
            var model = new TicketCategory { EventId = eventId };
            return View(model);
        }

        // POST: TicketCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,EventId")] TicketCategory ticketCategory)
        {
            if (ModelState.IsValid)
            {
                db.TicketCategories.Add(ticketCategory);
                db.SaveChanges();
                return RedirectToAction("Details", "Events", new { id = ticketCategory.EventId});
            }

            //ViewBag.EventId = new SelectList(db.Events, "Id", "Title", ticketCategory.EventId);
            return View(ticketCategory);
        }

        // GET: TicketCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCategory ticketCategory = db.TicketCategories.Find(id);
            if (ticketCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", ticketCategory.EventId);
            return View(ticketCategory);
        }

        // POST: TicketCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,EventId")] TicketCategory ticketCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", ticketCategory.EventId);
            return View(ticketCategory);
        }

        // GET: TicketCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketCategory ticketCategory = db.TicketCategories.Find(id);
            if (ticketCategory == null)
            {
                return HttpNotFound();
            }
            return View(ticketCategory);
        }

        // POST: TicketCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketCategory ticketCategory = db.TicketCategories.Find(id);
            db.TicketCategories.Remove(ticketCategory);
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
