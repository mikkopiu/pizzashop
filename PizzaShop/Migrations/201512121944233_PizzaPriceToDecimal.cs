namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaPriceToDecimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "PriceEur", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Pizzas", "PriceCents");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pizzas", "PriceCents", c => c.Int(nullable: false));
            DropColumn("dbo.Pizzas", "PriceEur");
        }
    }
}
