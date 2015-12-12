using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Models
{
    public class CustomPizzaTopping
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int OrderLineID { get; set; }

        [Required]
        public int ToppingId { get; set; }


        [ForeignKey("OrderLineID")]
        public OrderLine OrderLine { get; set; }

        [ForeignKey("OrderLineID")]
        public Topping Topping { get; set; }
    }
}