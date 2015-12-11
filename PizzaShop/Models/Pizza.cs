using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PizzaShop.Models;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models
{
    public class Pizza
    {
        private List<Topping> toppings = new List<Topping>();

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Price in Cents")]
        public int PriceCents { get; set; }
        public List<Topping> Toppings
        {
            get { return toppings; }
            set { toppings = value; }
        }

        [Display(Name = "Price")]
        public string DisplayPrice
        {
            get
            {
                return ((float)this.PriceCents / 100).ToString();
            }
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

        public Topping getToppingByIndex(int index)
        {
            return toppings.ElementAt(index);
        }

        public int getToppingIndex(Topping topping)
        {
            return toppings.IndexOf(topping);
        }

    }
}