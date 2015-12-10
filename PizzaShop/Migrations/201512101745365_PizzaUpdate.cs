namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "PriceCents", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pizzas", "PriceCents");
        }
    }
}
