using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Models
{
    public class OrderLine
    {
        private List<CustomPizzaTopping> customPizzaToppings = new List<CustomPizzaTopping>();

        [Key]
        public int ID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int PizzaID { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        [ForeignKey("PizzaID")]
        public Pizza Pizza { get; set; }

        public List<CustomPizzaTopping> CustomPizzaToppings
        {
            get { return customPizzaToppings; }
            set { customPizzaToppings = value; }
        }

        public void addCustomPizzaTopping(CustomPizzaTopping topping)
        {
            customPizzaToppings.Add(topping);
        }

        public void removeCustomPizzaTopping(CustomPizzaTopping topping)
        {
            customPizzaToppings.Remove(topping);
        }

        public void removeCustomPizzaToppingByIndex(int index)
        {
            customPizzaToppings.RemoveAt(index);
        }
    }
}