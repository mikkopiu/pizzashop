using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class Order
    {
        private List<Pizza> pizzas = new List<Pizza>();

        [Required]
        public int ID { get; set; }

        [Required]
        public int PriceCents { get; set; }

        // Optional, orders can be made by unregistered users
        public ApplicationUser Client { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Delivery address is required")]
        [Display(Name = "Address")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [Display(Name = "Postal code")]
        public string DeliveryPostCode { get; set; }

        [Required(ErrorMessage = "Delivery town is required")]
        [Display(Name = "City")]
        public string DeliveryCity { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        public int PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        public List<Pizza> Pizzas
        {
            get { return pizzas; }
            set { pizzas = value; }
        }

        public void addPizza(Pizza pizza)
        {
            pizzas.Add(pizza);
        }

        public void removePizza(Pizza pizza)
        {
            pizzas.Remove(pizza);
        }

        public void removePizzaByIndex(int index)
        {
            pizzas.RemoveAt(index);
        }
    }
}