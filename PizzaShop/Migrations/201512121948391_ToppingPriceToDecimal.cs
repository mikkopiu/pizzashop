namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToppingPriceToDecimal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Toppings", "PriceEur", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Toppings", "PriceCents");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Toppings", "PriceCents", c => c.Int(nullable: false));
            DropColumn("dbo.Toppings", "PriceEur");
        }
    }
}
