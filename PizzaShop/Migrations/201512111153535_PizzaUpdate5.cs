namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pizzas", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pizzas", "Name", c => c.String());
        }
    }
}
