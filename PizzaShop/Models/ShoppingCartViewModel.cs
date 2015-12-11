using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class ShoppingCartViewModel
    {
        public List<Pizza> CartPizzas { get; set; }
        public int CartTotalCents { get; set; }
    }
}