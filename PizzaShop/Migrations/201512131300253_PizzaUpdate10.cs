namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomPizzaToppings", "OrderLineID", "dbo.OrderLines");
            AddColumn("dbo.CustomPizzaToppings", "OrderLine_ID", c => c.Int());
            CreateIndex("dbo.CustomPizzaToppings", "OrderLine_ID");
            AddForeignKey("dbo.CustomPizzaToppings", "OrderLine_ID", "dbo.OrderLines", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomPizzaToppings", "OrderLine_ID", "dbo.OrderLines");
            DropIndex("dbo.CustomPizzaToppings", new[] { "OrderLine_ID" });
            DropColumn("dbo.CustomPizzaToppings", "OrderLine_ID");
            AddForeignKey("dbo.CustomPizzaToppings", "OrderLineID", "dbo.OrderLines", "ID", cascadeDelete: true);
        }
    }
}
