using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class Order
    {
        private List<OrderLine> orderLines = new List<OrderLine>();

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
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        public List<OrderLine> OrderLines
        {
            get { return orderLines; }
            set { orderLines = value; }
        }

        public void addOrderLine(OrderLine orderLine)
        {
            orderLines.Add(orderLine);
        }

        public void removeOrderLine(OrderLine orderLine)
        {
            orderLines.Remove(orderLine);
        }

        public void removeOrderLineByIndex(int index)
        {
            orderLines.RemoveAt(index);
        }
    }
}