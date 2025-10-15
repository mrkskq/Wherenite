using Microsoft.AspNet.Identity;
using Stripe.Checkout;
using Stripe;
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
    public class CartItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CartItems
        public ActionResult Index()
        {
            //var cartItems = db.CartItems.Include(c => c.Event);
            //return View(cartItems.ToList());
            string userId = User.Identity.GetUserId();

            var items = db.CartItems.Include(c => c.Event).Where(c => c.UserId == userId).ToList();

            return View(items);
        }

        [Authorize(Roles = "Admin, User")]

        public ActionResult AddItemToCart(int eventId)
        {
            //var ev = db.Events.Find(eventId);
            //if (ev == null)
            //{
            //    return HttpNotFound();
            //}

            //ViewBag.Event = ev;

            //var item = new CartItem
            //{
            //    EventId = ev.Id,
            //    Quantity = 1,
            //};


            //return View(item);

      
        
            var ev = db.Events.Include("TicketCategories").FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                return HttpNotFound();
            }

            ViewBag.Event = ev;

            ViewBag.Categories = ev.TicketCategories.Select(tc => new SelectListItem
                {
                    Text = $"{tc.Name} - {tc.Price} ден.",
                    Value = tc.Name  
                }).ToList();

            var cartItem = new CartItem
            {
                EventId = ev.Id,
                Quantity = 1
            };

            return View(cartItem);
        

        }

        [Authorize(Roles = "Admin, User")]

        [HttpPost]
        public ActionResult AddItemToCart(CartItem item)
        {
            //ako e najaven vrati ID na korisnik, ako ne koristi go session ID-to

            string userId = User.Identity.GetUserId();
            Console.WriteLine(userId);

            var category = db.TicketCategories.FirstOrDefault(tc => tc.EventId == item.EventId && tc.Name == item.TicketCategoryId);
            if (category == null) return HttpNotFound();

            var existingItems = db.CartItems.FirstOrDefault(c => c.EventId == item.EventId && c.UserId == userId && c.TicketCategoryId == item.TicketCategoryId);

            if (existingItems != null)
            {
                existingItems.Quantity += item.Quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    EventId = item.EventId,
                    TicketCategoryId = category.Name,
                    TicketCategoryPrice = category.Price,
                    Quantity = item.Quantity,
                    UserId = userId
                };
                db.CartItems.Add(cartItem);
            }

            db.SaveChanges();

            ViewBag.Event = db.Events.Find(item.EventId);
            return RedirectToAction("Index", "CartItems");
        }

        public CartItemsController()
        {
            StripeConfiguration.ApiKey = "sk_test_51RvFTgQiVsUwIa0EwegdmmWDSKX7LxcaI3wfQU04pEXRba0rw4QmraSg44EE0x7eGtaYALZXv5UKMMkxraPiSPWd007r2bRsQk";
        }

        [HttpPost]
        public ActionResult CreateCheckoutSession(int cartItemId)
        {
            var cartItem = db.CartItems
                .Include(c => c.Event)
                .Include(c => c.Event.TicketCategories)
                .FirstOrDefault(c => c.Id == cartItemId);

            if (cartItem == null)
            {
                return HttpNotFound();
            }

            var ev = cartItem.Event;
            var quantity = cartItem.Quantity;
            var category = ev.TicketCategories.FirstOrDefault(c => c.Name == cartItem.TicketCategoryId);

            var domain = "https://localhost:44362";

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                   
                    UnitAmount = (long)(category.Price * 100),
                    Currency = "mkd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = ev.Title,
                        Description = ev.Description
                    }
                },
                Quantity = quantity
            }
        },
                Mode = "payment",
                SuccessUrl = domain + $"/CartItems/Success?session_id={{CHECKOUT_SESSION_ID}}&eventId={cartItem.EventId}",
                CancelUrl = domain + "/CartItems/Cancel"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Redirect(session.Url);
        }


        public ActionResult Success(string session_id, int eventId)
        {
            var userId = User.Identity.GetUserId();

            var cartItem = db.CartItems.FirstOrDefault(c => c.UserId == userId && c.EventId == eventId);
            if (cartItem != null)
            {
                db.CartItems.Remove(cartItem);
                db.SaveChanges();
            }

            ViewBag.SessionId = session_id;
            return View();
        }

        public ActionResult Cancel()
        {
            return View();
        }

        // GET: CartItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            return View(cartItem);
        }

        // GET: CartItems/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title");
            return View();
        }

        // POST: CartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventId,Quantity,UserId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.CartItems.Add(cartItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", cartItem.EventId);
            return View(cartItem);
        }

        // GET: CartItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", cartItem.EventId);
            return View(cartItem);
        }

        // POST: CartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventId,Quantity,UserId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Events, "Id", "Title", cartItem.EventId);
            return View(cartItem);
        }

        // GET: CartItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CartItem cartItem = db.CartItems.Find(id);
            if (cartItem == null)
            {
                return HttpNotFound();
            }
            //return View(cartItem);
            db.CartItems.Remove(cartItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: CartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CartItem cartItem = db.CartItems.Find(id);
            db.CartItems.Remove(cartItem);
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
