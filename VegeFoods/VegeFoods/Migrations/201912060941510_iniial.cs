namespace VegeFoods.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(),
                        MemberId = c.Int(),
                        CartStatusId = c.Int(),
                        AddedOn = c.DateTime(),
                        UpdatedOn = c.DateTime(),
                        ShippingDetailId = c.Int(),
                        Product_ProductId = c.Int(),
                        Product_ProductId1 = c.Int(),
                        Product1_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.CartId)
                .ForeignKey("dbo.CartStatus", t => t.CartStatusId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId1)
                .ForeignKey("dbo.Products", t => t.Product1_ProductId)
                .Index(t => t.CartStatusId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Product_ProductId1)
                .Index(t => t.Product1_ProductId);
            
            CreateTable(
                "dbo.CartStatus",
                c => new
                    {
                        CartStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.CartStatusId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        CategoryId = c.Int(),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        ModifiedDate = c.DateTime(),
                        Description = c.String(),
                        ProductImage = c.String(),
                        Price = c.Decimal(precision: 18, scale: 2),
                        IsFeatured = c.Boolean(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.MemberRoles",
                c => new
                    {
                        MemberRoleId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(),
                        RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.MemberRoleId)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailId = c.String(),
                        Password = c.String(),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                        CreatedOn = c.DateTime(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.ShippingDetails",
                c => new
                    {
                        ShippingDetailId = c.Int(nullable: false, identity: true),
                        MemberId = c.Int(),
                        AddressLine = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        ZipCode = c.String(),
                        OrderId = c.String(),
                        AmountPaid = c.Decimal(precision: 18, scale: 2),
                        PaymentType = c.String(),
                    })
                .PrimaryKey(t => t.ShippingDetailId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Carts", "Product1_ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "Product_ProductId1", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Carts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Carts", "CartStatusId", "dbo.CartStatus");
            DropIndex("dbo.MemberRoles", new[] { "RoleId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.Carts", new[] { "Product1_ProductId" });
            DropIndex("dbo.Carts", new[] { "Product_ProductId1" });
            DropIndex("dbo.Carts", new[] { "Product_ProductId" });
            DropIndex("dbo.Carts", new[] { "CartStatusId" });
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.Members");
            DropTable("dbo.Roles");
            DropTable("dbo.MemberRoles");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.CartStatus");
            DropTable("dbo.Carts");
        }
    }
}
