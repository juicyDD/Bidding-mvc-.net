namespace A.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class History : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BiddingHistories",
                c => new
                    {
                        BiddingID = c.Int(nullable: false, identity: true),
                        BiddingTime = c.DateTime(nullable: false),
                        BiddingPrice = c.Long(nullable: false),
                        BiddingUser_Id = c.String(maxLength: 128),
                        Product_ProductID = c.Int(),
                    })
                .PrimaryKey(t => t.BiddingID)
                .ForeignKey("dbo.AspNetUsers", t => t.BiddingUser_Id)
                .ForeignKey("dbo.Products", t => t.Product_ProductID)
                .Index(t => t.BiddingUser_Id)
                .Index(t => t.Product_ProductID);
            
            AddColumn("dbo.Products", "CurrentPrice", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BiddingHistories", "Product_ProductID", "dbo.Products");
            DropForeignKey("dbo.BiddingHistories", "BiddingUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.BiddingHistories", new[] { "Product_ProductID" });
            DropIndex("dbo.BiddingHistories", new[] { "BiddingUser_Id" });
            DropColumn("dbo.Products", "CurrentPrice");
            DropTable("dbo.BiddingHistories");
        }
    }
}
