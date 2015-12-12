﻿using System;
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
    public class PizzasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pizzas
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Pizzas.ToList());
        }

        // GET: Pizzas/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> allToppings = GetSelectListItems();

            ViewBag.AllToppings = allToppings;

            var savedToppings = new int[5];

            for (int i = 0; i < 5; i++)
            {
                savedToppings[i] = 0;
            }

            ViewBag.SavedToppings = savedToppings;

            return View();
        }

        private IEnumerable<SelectListItem> GetSelectListItems()
        {
            List<Topping> toppings = db.Toppings.ToList();

            var selectList = new List<SelectListItem>();

            selectList.Add(
                new SelectListItem { Value = "0", Text = "Select Topping" }
            );

            foreach (Topping topping in toppings)
            {
                selectList.Add(
                    new SelectListItem { Value = topping.ID.ToString(), Text = topping.Name }
                );
            }

            return selectList;

        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PriceEur")] Pizza pizza)
        {
            var toppingIds = new List<string>();
            toppingIds.Add(Request.Form["Topping1"]);
            toppingIds.Add(Request.Form["Topping2"]);
            toppingIds.Add(Request.Form["Topping3"]);
            toppingIds.Add(Request.Form["Topping4"]);
            toppingIds.Add(Request.Form["Topping5"]);

            var savedToppings = new int[5];

            foreach (var value in toppingIds)
            {
                int intValue = int.Parse(value);
                if (intValue != 0)
                {
                    pizza.addTopping(db.Toppings.Find(intValue));
                }
            }

            for (int i = 0; i < 5; i++)
            {
                if (pizza.Toppings.ElementAtOrDefault(i) != null)
                {
                    savedToppings[i] = pizza.Toppings[i].ID;
                }
                else
                {
                    savedToppings[i] = 0;
                }
            }
            
            ViewBag.SavedToppings = savedToppings;

            IEnumerable<SelectListItem> allToppings = GetSelectListItems();
            ViewBag.AllToppings = allToppings;

            // Validate toppings.
            bool duplicates = pizza.Toppings.GroupBy(n => n).Any(c => c.Count() > 1);
            
            if(duplicates)
            {
                ViewBag.DuplicateToppings = "Please select a specific topping only once.";
                return View(pizza);
            }

            if (ModelState.IsValid)
            {
                db.Pizzas.Add(pizza);
                db.SaveChanges();
                
                return RedirectToAction("Index");
            }

            return View(pizza);
        }

        // GET: Pizzas/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Include(p => p.Toppings).SingleOrDefault(x => x.ID == id);
            if (pizza == null)
            {
                return HttpNotFound();
            }

            IEnumerable<SelectListItem> allToppings = GetSelectListItems();
            ViewBag.AllToppings = allToppings;

            var savedToppings = new int[5];

            for(int i = 0; i < 5; i++)
            {
                if(pizza.Toppings.ElementAtOrDefault(i) != null)
                {
                    savedToppings[i] = pizza.Toppings[i].ID;
                } else
                {
                    savedToppings[i] = 0;
                }
            }

            ViewBag.SavedToppings = savedToppings;

            return View(pizza);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PriceEur")] Pizza pizza)
        {
            var toppingIds = new List<string>();
            toppingIds.Add(Request.Form["Topping1"]);
            toppingIds.Add(Request.Form["Topping2"]);
            toppingIds.Add(Request.Form["Topping3"]);
            toppingIds.Add(Request.Form["Topping4"]);
            toppingIds.Add(Request.Form["Topping5"]);

            foreach (var value in toppingIds)
            {
                int intValue = int.Parse(value);
                if (intValue != 0)
                {
                    pizza.addTopping(db.Toppings.Find(intValue));
                }
            }

            IEnumerable<SelectListItem> allToppings = GetSelectListItems();
            ViewBag.AllToppings = allToppings;

            var savedToppings = new int[5];

            for (int i = 0; i < 5; i++)
            {
                if (pizza.Toppings.ElementAtOrDefault(i) != null)
                {
                    savedToppings[i] = pizza.Toppings[i].ID;
                }
                else
                {
                    savedToppings[i] = 0;
                }
            }

            ViewBag.SavedToppings = savedToppings;

            // Validate toppings.
            bool duplicates = pizza.Toppings.GroupBy(n => n).Any(c => c.Count() > 1);

            if (duplicates)
            {
                ViewBag.DuplicateToppings = "Please select a specific topping only once.";
                return View(pizza);
            }

            if (ModelState.IsValid)
            {
                

                Pizza dbPizza = db.Pizzas.Include(p => p.Toppings).Single(c => c.ID == pizza.ID);
                db.Entry(dbPizza).CurrentValues.SetValues(pizza);
                
                foreach(var topping in dbPizza.Toppings.ToList())
                {
                    if(!pizza.Toppings.Any(s => s.ID == topping.ID))
                    {
                        dbPizza.Toppings.Remove(topping);
                    }
                }

                foreach(var newTopping in pizza.Toppings)
                {
                    var dbTopping = dbPizza.Toppings.SingleOrDefault(s => s.ID == newTopping.ID);
                    if(dbTopping != null)
                    {
                        db.Entry(dbTopping).CurrentValues.SetValues(newTopping);
                    } else
                    {
                        dbPizza.Toppings.Add(newTopping);
                    }
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(pizza);
        }

        // GET: Pizzas/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pizza pizza = db.Pizzas.Find(id);
            if (pizza == null)
            {
                return HttpNotFound();
            }
            return View(pizza);
        }

        // POST: Pizzas/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pizza pizza = db.Pizzas.Find(id);
            db.Pizzas.Remove(pizza);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Detail(int id)
        {
            Pizza pizza = db.Pizzas.Include(p => p.Toppings).First(p => p.ID == id);
            List<Topping> toppings = db.Toppings.ToList();
            return Json(new
            {
                Toppings = toppings,
                Pizza = pizza
            });
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
