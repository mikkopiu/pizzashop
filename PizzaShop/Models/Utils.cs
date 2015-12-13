using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PizzaShop.Models
{
    public class Utils
    {
        // Get a UNIX timestamp (millis), because C# doesn't have such a method
        public static long UnixTimeNow()
        {
            return (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds; ;
        }

        // Calcuate the total price of a list of CartItem's (i.e. cart in session)
        public static decimal CountCartTotalPrice(List<CartItem> cart)
        {
            return cart?
                .Select(item => item.GetActualPrice())
                .Aggregate((acc, price) => acc + price) ?? 0.0m;
        }
    }
}