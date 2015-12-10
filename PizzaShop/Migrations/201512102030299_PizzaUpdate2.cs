namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Toppings", new[] { "Pizza_Id" });
            CreateIndex("dbo.Toppings", "Pizza_ID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Toppings", new[] { "Pizza_ID" });
            CreateIndex("dbo.Toppings", "Pizza_Id");
        }
    }
}
