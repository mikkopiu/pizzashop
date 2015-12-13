namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pizzaImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pizzas", "ImageFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pizzas", "ImageFileName");
        }
    }
}
