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
            // Init cart if it doesn't already exist
            List<CartItem> cart = (List<CartItem>)Session["cart"] ?? new List<CartItem>();

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
            var addedPizza = db.Pizzas
                .Include(p => p.Toppings)
                .Single(p => p.ID == id);
            
            // Init cart if it doesn't already exist
            List<CartItem> cart = (List<CartItem>)Session["cart"] ?? new List<CartItem>();

            // Add to shopping cart
            cart.Add(new CartItem {
                ID = long.Parse(addedPizza.ID.ToString() +
                    Utils.UnixTimeNow().ToString()),
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
            // Init cart if it doesn't already exist
            List<CartItem> cart = (List<CartItem>)Session["cart"] ?? new List<CartItem>();

            Pizza selectedBasePizza = db.Pizzas
                .Include(p => p.Toppings)
                .FirstOrDefault(p => p.ID == id);

            // The toppings won't replace the Pizza's toppings,
            // but will instead be included in the CartItem.ExtraToppings List.
            //
            // NOTE: Not enough to just find every Topping in DB,
            // because there might be multiple instances of the same Topping
            // that need to be added here.
            List<Topping> extraToppings = toppingIds
                .Select(tId => db.Toppings.FirstOrDefault(d => d.ID == tId))
                .Where(res => res != null)
                .ToList();

            // Create a CartItem with any selected extra Toppings
            // and the base Pizza
            CartItem item = new CartItem
            {
                ID = long.Parse(selectedBasePizza.ID.ToString() +
                    Utils.UnixTimeNow().ToString()),
                Pizza = selectedBasePizza,
                ExtraToppings = extraToppings
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

            CartItem itemToRemove = cart.SingleOrDefault(item => item.ID == id);

            string pizzaName = "";

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