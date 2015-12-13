namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PriceEur", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Orders", "PriceCents");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PriceCents", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "PriceEur");
        }
    }
}
