namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PizzaShop.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PizzaShop.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PizzaShop.Models.ApplicationDbContext context)
        {
            context.Toppings.AddOrUpdate(p => p.Name,
                new Topping
                {
                    Name = "Bacon",
                    PriceCents = 200
                },
                new Topping
                {
                    Name = "Pepperoni",
                    PriceCents = 150
                }
                );
        }
    }
}
