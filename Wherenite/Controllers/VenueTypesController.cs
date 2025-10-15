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
    [Authorize(Roles = "Admin")]
    public class VenueTypesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: VenueTypes
        public ActionResult Index()
        {
            return View(db.VenueTypes.ToList());
        }

        // GET: VenueTypes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueType venueType = db.VenueTypes.Find(id);
            if (venueType == null)
            {
                return HttpNotFound();
            }
            return View(venueType);
        }

        // GET: VenueTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VenueTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] VenueType venueType)
        {
            if (ModelState.IsValid)
            {
                db.VenueTypes.Add(venueType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venueType);
        }

        // GET: VenueTypes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueType venueType = db.VenueTypes.Find(id);
            if (venueType == null)
            {
                return HttpNotFound();
            }
            return View(venueType);
        }

        // POST: VenueTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] VenueType venueType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venueType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venueType);
        }

        // GET: VenueTypes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VenueType venueType = db.VenueTypes.Find(id);
            if (venueType == null)
            {
                return HttpNotFound();
            }
            return View(venueType);
        }

        // POST: VenueTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VenueType venueType = db.VenueTypes.Find(id);
            db.VenueTypes.Remove(venueType);
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
