namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pizzas", "Order_ID", "dbo.Orders");
            DropIndex("dbo.Pizzas", new[] { "Order_ID" });
            CreateTable(
                "dbo.CustomPizzaToppings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderLineID = c.Int(nullable: false),
                        ToppingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.OrderLines", t => t.OrderLineID, cascadeDelete: true)
                .ForeignKey("dbo.Toppings", t => t.OrderLineID, cascadeDelete: true)
                .Index(t => t.OrderLineID);
            
            CreateTable(
                "dbo.OrderLines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        PizzaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Pizzas", t => t.PizzaID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.PizzaID);
            
            DropColumn("dbo.Pizzas", "Order_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pizzas", "Order_ID", c => c.Int());
            DropForeignKey("dbo.CustomPizzaToppings", "OrderLineID", "dbo.Toppings");
            DropForeignKey("dbo.CustomPizzaToppings", "OrderLineID", "dbo.OrderLines");
            DropForeignKey("dbo.OrderLines", "PizzaID", "dbo.Pizzas");
            DropForeignKey("dbo.OrderLines", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderLines", new[] { "PizzaID" });
            DropIndex("dbo.OrderLines", new[] { "OrderID" });
            DropIndex("dbo.CustomPizzaToppings", new[] { "OrderLineID" });
            DropTable("dbo.OrderLines");
            DropTable("dbo.CustomPizzaToppings");
            CreateIndex("dbo.Pizzas", "Order_ID");
            AddForeignKey("dbo.Pizzas", "Order_ID", "dbo.Orders", "ID");
        }
    }
}
