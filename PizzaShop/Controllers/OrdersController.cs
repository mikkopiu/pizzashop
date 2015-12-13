using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaShop.Models;
using System.Data;
using System.Data.Entity;
using System.Net;
using Microsoft.AspNet.Identity;

namespace PizzaShop.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Enter order details
        public ActionResult Order()
        {
            var cart = (List<CartItem>)Session["cart"];

            // Return to cart if it doesn't exist or there are no CartItems.
            if(cart == null)
            {
                return RedirectToAction("Index", "ShoppingCart");
            } else if(cart.Count < 1)
            {
                return RedirectToAction("Index", "ShoppingCart");
            }

            var user = db.Users.Find(User.Identity.GetUserId());

            if(user != null)
            {
                Order order = new Order();
                order.DeliveryAddress = user.HomeAddress;
                order.DeliveryCity = user.HomeCity;
                order.DeliveryPostCode = user.HomePostCode;
                order.PhoneNumber = user.PhoneNumber;
                order.Email = user.Email;
                order.Client = user;

                return View(order);
            }

            ViewBag.CartTotalPrice = ShoppingCartController.GetCartTotalPrice();

            return View();
        }

        // POST: Orders/Order
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order([Bind(Include = "Id,DeliveryAddress,DeliveryPostCode,DeliveryCity,PhoneNumber,Email")] Order order)
        {
            order.OrderDate = DateTime.Now;

            bool isvalid = ModelState.IsValid;


            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }
    }
}