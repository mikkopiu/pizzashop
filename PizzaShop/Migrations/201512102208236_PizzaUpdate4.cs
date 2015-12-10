namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Toppings", "Pizza_ID", "dbo.Pizzas");
            DropIndex("dbo.Toppings", new[] { "Pizza_ID" });
            CreateTable(
                "dbo.PizzaTopping",
                c => new
                    {
                        PizzaID = c.Int(nullable: false),
                        ToppingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PizzaID, t.ToppingID })
                .ForeignKey("dbo.Pizzas", t => t.PizzaID, cascadeDelete: true)
                .ForeignKey("dbo.Toppings", t => t.ToppingID, cascadeDelete: true)
                .Index(t => t.PizzaID)
                .Index(t => t.ToppingID);
            
            DropColumn("dbo.Toppings", "Pizza_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toppings", "Pizza_ID", c => c.Int());
            DropForeignKey("dbo.PizzaTopping", "ToppingID", "dbo.Toppings");
            DropForeignKey("dbo.PizzaTopping", "PizzaID", "dbo.Pizzas");
            DropIndex("dbo.PizzaTopping", new[] { "ToppingID" });
            DropIndex("dbo.PizzaTopping", new[] { "PizzaID" });
            DropTable("dbo.PizzaTopping");
            CreateIndex("dbo.Toppings", "Pizza_ID");
            AddForeignKey("dbo.Toppings", "Pizza_ID", "dbo.Pizzas", "ID");
        }
    }
}
