namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaNameNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pizzas", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pizzas", "Name", c => c.String(nullable: false));
        }
    }
}
