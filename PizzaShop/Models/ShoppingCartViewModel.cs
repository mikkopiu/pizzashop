using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class ShoppingCartViewModel
    {
        public List<CartItem> CartPizzas { get; set; }
        public decimal CartTotalPrice { get; set; }
    }
}