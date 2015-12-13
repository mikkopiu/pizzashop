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
using System.Diagnostics;
using PizzaShop.Hubs;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace PizzaShop.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // List all orders
        // GET: /Orders/Index
        [System.Web.Mvc.Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(db.Orders.Include(o => o.OrderLines).OrderByDescending(o => o.OrderDate).ToList());
        }

        // GET: /Orders/Detail/5
        [System.Web.Mvc.Authorize(Roles = "admin")]
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders
                .Include(o => o.OrderLines)
                .Include("OrderLines.Pizza.Toppings")
                .Include("OrderLines.CustomPizzaToppings.Topping")
                .Where(c => c.ID == id).First();

            //var order = db.Orders.Include(o => o.OrderLines).Where(c => c.ID == id);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

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

            if (ModelState.IsValid)
            {
                var cart = (List<CartItem>)Session["cart"];

                decimal totalPrice = 0m;

                foreach (var cartItem in cart)
                {
                    OrderLine orderLine = new OrderLine();
                    //orderLine.Pizza = cartItem.Pizza;
                    orderLine.PizzaID = cartItem.Pizza.ID;
                    orderLine.Order = order;

                    totalPrice += cartItem.GetActualPrice();

                    var extraToppings = cartItem.ExtraToppings;

                    if (extraToppings != null)
                    {
                        foreach (var pizzaTopping in cartItem.ExtraToppings)
                        {
                            CustomPizzaTopping customPizzaTopping = new CustomPizzaTopping();
                            customPizzaTopping.OrderLine = orderLine;
                            //customPizzaTopping.Topping = pizzaTopping;
                            customPizzaTopping.ToppingID = pizzaTopping.ID;
                            orderLine.addCustomPizzaTopping(customPizzaTopping);
                        }
                    }

                    order.addOrderLine(orderLine);
                }

                order.PriceEur = totalPrice;

                db.Orders.Add(order);
                db.SaveChanges();

                // Parse the new Order into JSON.
                // Ignore self-referencing fields to avoid Exception (e.g. the Order-property).
                string json = JsonConvert.SerializeObject(order, Formatting.Indented,
                    new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                );

                // Inform web socket clients of the new order
                var context = GlobalHost.ConnectionManager.GetHubContext<OrderHub>();
                context.Clients.All.addNewOrder(json);

                return RedirectToAction("Index", "Home");
            }

            return View(order);
        }
    }
}