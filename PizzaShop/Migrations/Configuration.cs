namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PizzaShop.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

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
                    PriceEur = 200
                },
                new Topping
                {
                    Name = "Pepperoni",
                    PriceEur = 150
                }
                );

            // Create user roles
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            if (!context.Roles.Any(r => r.Name == "admin"))
            {
                var role = new IdentityRole { Name = "admin" };
                manager.Create(role);
            }
            if (!context.Roles.Any(r => r.Name == "customer"))
            {
                var role = new IdentityRole { Name = "customer" };
                manager.Create(role);
            }

            // Create admin user
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var PasswordHash = new PasswordHasher();
            if (!context.Users.Any(u => u.UserName == "admin@admin.com"))
            {
                var user = new ApplicationUser
                {
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    PhoneNumber = "0401112222",
                    HomeAddress = "Admin street 1",
                    HomePostCode = "00001",
                    HomeCity = "Adminville",
                    PasswordHash = PasswordHash.HashPassword("Ding123?")
                };

                UserManager.Create(user);
                UserManager.AddToRole(user.Id, "admin");
            }
        }
    }
}
