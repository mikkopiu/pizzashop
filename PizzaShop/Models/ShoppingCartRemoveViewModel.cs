using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class ShoppingCartRemoveViewModel
    {
        public string Message { get; set; }
        public int CartTotalCents { get; set; }
        public int ItemCount { get; set; }
        public int DeleteId { get; set; }
    }
}