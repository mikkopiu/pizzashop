namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PizzaToppings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PizzaID = c.Int(nullable: false),
                        ToppingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PizzaToppings");
        }
    }
}
