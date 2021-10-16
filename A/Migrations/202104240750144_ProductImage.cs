namespace A.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ImageURL = c.String(nullable: false, maxLength: 128),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.ImageURL)
                .ForeignKey("dbo.Products", t => t.Product_ProductID)
                .Index(t => t.Product_ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "Product_ProductID", "dbo.Products");
            DropIndex("dbo.ProductImages", new[] { "Product_ProductID" });
            DropTable("dbo.ProductImages");
        }
    }
}
