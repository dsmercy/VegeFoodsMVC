namespace VegeFoods.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PriceSale", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "PriceSale");
        }
    }
}
