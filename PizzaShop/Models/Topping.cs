using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class Topping
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal PriceEur { get; set; }

    }
}