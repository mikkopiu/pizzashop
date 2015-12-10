namespace PizzaShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PizzaUpdate3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PizzaToppings");
        }
        
        public override void Down()
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
    }
}
