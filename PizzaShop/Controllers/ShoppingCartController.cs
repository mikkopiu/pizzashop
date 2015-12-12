using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Diagnostics;

namespace PizzaShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<CartItem> cart;

            // Init cart if it doesn't already exist
            if (Session["cart"] == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = (List<CartItem>)Session["cart"];
            }

            var viewModel = new ShoppingCartViewModel
            {
                CartPizzas = cart,
                CartTotalCents = countTotalPriceCents(cart)
            };

            return View(viewModel);
        }

        //
        // GET: /Store/AddToCart/5
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            // Retrieve the Pizza to add from the database
            var addedPizza = db.Pizzas.Include(p => p.Toppings).Single(p => p.ID == id);

            List<CartItem> cart;

            // Init cart if it doesn't already exist
            if (Session["cart"] == null)
            {
                cart = new List<CartItem>();
            } else
            {
                cart = (List<CartItem>)Session["cart"];
            }

            // Add to shopping cart
            cart.Add(new CartItem {
                ID = long.Parse(addedPizza.ID.ToString() + UnixTimeNow().ToString()),
                Pizza = addedPizza
            });

            // Update session
            Session["cart"] = cart;

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedPizza.Name) +
                    " has been added to your cart.",
                CartTotalCents = countTotalPriceCents(cart),
                ItemCount = cart.Count()
            };
            return Json(results);
        }

        [HttpPost]
        public ActionResult AddCustomToCart(Pizza pizza)
        {
            List<CartItem> cart;

            // Init cart if it doesn't already exist
            if (Session["cart"] == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = (List<CartItem>)Session["cart"];
            }

            CartItem item = new CartItem
            {
                ID = long.Parse(pizza.ID.ToString() + UnixTimeNow().ToString()),
                Pizza = pizza
            };

            // Add to shopping cart
            cart.Add(item);

            // Update session
            Session["cart"] = cart;

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(pizza.Name) +
                    " has been added to your cart.",
                CartTotalCents = countTotalPriceCents(cart),
                ItemCount = cart.Count()
            };
            return Json(results);
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(long id)
        {
            // Remove the item from the cart
            var cart = (List<CartItem>)Session["cart"];
            
            string pizzaName = "";

            CartItem itemToRemove = cart.SingleOrDefault(item => item.ID == id);

            if (itemToRemove != null)
            {
                pizzaName = itemToRemove.Pizza.Name;
                cart.Remove(itemToRemove);
            }

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = "One (1) " + Server.HtmlEncode(pizzaName) +
                    " has been removed from your shopping cart.",
                CartTotalCents = countTotalPriceCents(cart),
                ItemCount = cart.Count(),
                DeleteId = id
            };
            return Json(results);
        }

        private int countTotalPriceCents(List<CartItem> pizzas)
        {
            // Count total cart price
            int totalCents = 0;
            foreach (CartItem p in pizzas)
            {
                totalCents += p.Pizza.PriceCents;
            }
            return totalCents;
        }

        private long UnixTimeNow()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds; ;
        }
    }
}