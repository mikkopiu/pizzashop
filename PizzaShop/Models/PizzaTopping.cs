using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class PizzaTopping
    {
        public int ID { get; set; }
        public int PizzaID { get; set; }
        public int ToppingID { get; set; }

        public PizzaTopping(int pizzaId, int toppingId)
        {
            PizzaID = pizzaId;
            ToppingID = toppingId;
        }
    }
}