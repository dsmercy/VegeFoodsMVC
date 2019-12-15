namespace VegeFoods.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.CategoryDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CategoryDetails",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(),
                        IsDelete = c.Boolean(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
        }
    }
}
