using PizzaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PizzaShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            List<Pizza> cart;

            // Init cart if it doesn't already exist
            if (Session["cart"] == null)
            {
                cart = new List<Pizza>();
            }
            else
            {
                cart = (List<Pizza>)Session["cart"];
            }

            // Count total cart price
            int totalCents = 0;
            foreach (Pizza p in cart)
            {
                totalCents += p.PriceCents;
            }

            var viewModel = new ShoppingCartViewModel
            {
                CartPizzas = cart,
                CartTotalCents = totalCents
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

            List<Pizza> cart;

            // Init cart if it doesn't already exist
            if (Session["cart"] == null)
            {
                cart = new List<Pizza>();
            } else
            {
                cart = (List<Pizza>)Session["cart"];
            }

            // Add to shopping cart
            cart.Add(addedPizza);

            // Update session
            Session["cart"] = cart;

            // Count total cart price
            int totalCents = 0;
            foreach (Pizza p in cart)
            {
                totalCents += p.PriceCents;
            }

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedPizza.Name) +
                    " has been added to your cart.",
                CartTotalCents = totalCents,
                ItemCount = cart.Count(),
                DeleteId = id
            };
            return Json(results);

            // Go back to the main store page for more shopping
            // return RedirectToAction("Index");
        }
    }
}