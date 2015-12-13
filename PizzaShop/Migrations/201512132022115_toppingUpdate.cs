namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toppingUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Toppings", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Toppings", "Name", c => c.String());
        }
    }
}
