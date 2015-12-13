using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    public class ToppingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Toppings
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            ViewBag.CartTotalPrice = ShoppingCartController.getCartTotalPrice();

            return View(db.Toppings.ToList());
        }

        // GET: Toppings/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.CartTotalPrice = ShoppingCartController.getCartTotalPrice();

            return View();
        }

        // POST: Toppings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,PriceEur")] Topping topping)
        {
            if (ModelState.IsValid)
            {
                db.Toppings.Add(topping);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(topping);
        }

        // GET: Toppings/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topping topping = db.Toppings.Find(id);
            if (topping == null)
            {
                return HttpNotFound();
            }

            ViewBag.CartTotalPrice = ShoppingCartController.getCartTotalPrice();

            return View(topping);
        }

        // POST: Toppings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,PriceEur")] Topping topping)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topping).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topping);
        }

        // GET: Toppings/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topping topping = db.Toppings.Find(id);
            if (topping == null)
            {
                return HttpNotFound();
            }

            ViewBag.CartTotalPrice = ShoppingCartController.getCartTotalPrice();

            return View(topping);
        }

        // POST: Toppings/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Topping topping = db.Toppings.Find(id);
            db.Toppings.Remove(topping);
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
