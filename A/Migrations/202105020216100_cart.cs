namespace A.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        product_ProductID = c.Int(),
                        user_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.product_ProductID)
                .ForeignKey("dbo.AspNetUsers", t => t.user_Id)
                .Index(t => t.product_ProductID)
                .Index(t => t.user_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShoppingCarts", "user_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShoppingCarts", "product_ProductID", "dbo.Products");
            DropIndex("dbo.ShoppingCarts", new[] { "user_Id" });
            DropIndex("dbo.ShoppingCarts", new[] { "product_ProductID" });
            DropTable("dbo.ShoppingCarts");
        }
    }
}
