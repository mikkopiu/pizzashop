using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class Topping
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}", ConvertEmptyStringToNull = true)]
        public decimal PriceEur { get; set; }

    }
}