using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Models;

namespace PizzaShop.Models
{
    public class Pizza
    {
        private List<Topping> toppings = new List<Topping>();

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Topping> Toppings
        {
            get { return toppings; }
            set { toppings = value; }
        }
    
        public void addTopping(Topping topping)
        {
            toppings.Add(topping);
        }

        public void removeTopping(Topping topping)
        {
            toppings.Remove(topping);
        }

        public void removeToppingByIndex(int index)
        {
            toppings.RemoveAt(index);
        }

    }
}