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

            decimal totalPrice = Utils.CountCartTotalPrice(cart);

            // For displaying the current cart price on load
            ViewBag.CartTotalPrice = totalPrice;

            var viewModel = new ShoppingCartViewModel
            {
                CartPizzas = cart,
                CartTotalPrice = totalPrice
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
                ID = long.Parse(addedPizza.ID.ToString() + Utils.UnixTimeNow().ToString()),
                Pizza = addedPizza
            });

            // Update session
            Session["cart"] = cart;

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(addedPizza.Name) +
                    " has been added to your cart.",
                CartTotalPrice = Utils.CountCartTotalPrice(cart),
                ItemCount = cart.Count()
            };
            return Json(results);
        }

        //
        // POST: /ShoppingCart/AddCustomToCart
        [HttpPost]
        public ActionResult AddCustomToCart(int id, int[] toppingIds)
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

            // Find the base pizza (no need to include toppings, as they will be replaced anyway)
            Pizza selectedBasePizza = db.Pizzas.FirstOrDefault(p => p.ID == id);
            List<Topping> toppings = new List<Topping>();

            // Not enough to just find every Topping in DB,
            // because there might be multiple instances of the same Topping
            // that need to be added here.
            foreach (int tId in toppingIds)
            {
                Topping t = db.Toppings.FirstOrDefault(d => d.ID == tId);
                if (t != null)
                {
                    toppings.Add(t);
                }
            }

            selectedBasePizza.Toppings = toppings;

            CartItem item = new CartItem
            {
                ID = long.Parse(selectedBasePizza.ID.ToString() + Utils.UnixTimeNow().ToString()),
                Pizza = selectedBasePizza
            };

            // Add to shopping cart
            cart.Add(item);

            // Update session
            Session["cart"] = cart;

            // Display the confirmation message
            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode("") +
                    " has been added to your cart.",
                CartTotalPrice = Utils.CountCartTotalPrice(cart),
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
                CartTotalPrice = Utils.CountCartTotalPrice(cart),
                ItemCount = cart.Count(),
                DeleteId = id
            };
            return Json(results);
        }

        // Public getter for current total price of cart items
        public static decimal GetCartTotalPrice()
        {
            List<CartItem> cart = (List<CartItem>)System.Web.HttpContext.Current.Session["cart"];
            return Utils.CountCartTotalPrice(cart);
        }
    }
}