namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HomeAddress", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomePostCode", c => c.String());
            AddColumn("dbo.AspNetUsers", "HomeCity", c => c.String());
            AlterColumn("dbo.Orders", "PhoneNumber", c => c.String(nullable: false));
            DropColumn("dbo.Pizzas", "PriceCents");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pizzas", "PriceCents", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "PhoneNumber", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "HomeCity");
            DropColumn("dbo.AspNetUsers", "HomePostCode");
            DropColumn("dbo.AspNetUsers", "HomeAddress");
        }
    }
}
