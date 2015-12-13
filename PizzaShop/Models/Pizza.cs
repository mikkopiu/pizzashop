using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace PizzaShop.Models
{
    public class Pizza
    {
        private List<Topping> toppings = new List<Topping>();

        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}", ConvertEmptyStringToNull = true)]
        [DataType(DataType.Currency)]
        public decimal PriceEur { get; set; }

        public List<Topping> Toppings
        {
            get { return toppings; }
            set { toppings = value; }
        }
        
        public string ImageFileName { get; set; }

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