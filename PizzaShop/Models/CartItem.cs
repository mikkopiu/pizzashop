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
        public Pizza Pizza { get; set; }
    }
}