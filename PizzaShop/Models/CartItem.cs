using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    // Simple class to make each Pizza a unique item in the cart
    public class CartItem
    {
        // ID = Pizza's ID + timestamp when it was added to the cart
        public long ID { get; set; }

        // Base pizza (includes some un-editable toppings + base price)
        public Pizza Pizza { get; set; }

        // Any extra toppings added from the customer's edit-modal (added to price)
        public List<Topping> ExtraToppings { get; set; }

        public decimal GetActualPrice()
        {
            return GetActualPrice(this);
        }

        // Get the actual price, i.e. Pizza's base price + any extra Toppings
        public static decimal GetActualPrice(CartItem item)
        {
            decimal price = 0;

            price += item?.Pizza?.PriceEur ?? 0;

            foreach (Topping t in item?.ExtraToppings ?? new List<Topping>())
            {
                price += t.PriceEur;
            }

            return price;
        }
    }
}