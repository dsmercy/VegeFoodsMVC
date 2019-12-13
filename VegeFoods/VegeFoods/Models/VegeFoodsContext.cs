using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class VegeFoodsContext : DbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public VegeFoodsContext() : base("name=VegeFoodsContext")
    {
    }
    
    public DbSet<VegeFoods.Models.Cart> Cart { get; set; }
    public DbSet<VegeFoods.Models.CartStatus> CartStatus { get; set; }
    public DbSet<VegeFoods.Models.Category> Category { get; set; }
    public DbSet<VegeFoods.Models.MemberRole> MemberRole { get; set; }
    public DbSet<VegeFoods.Models.Members> Members { get; set; }
    public DbSet<VegeFoods.Models.Product> Product { get; set; }
    public DbSet<VegeFoods.Models.Roles> Roles { get; set; }
    public DbSet<VegeFoods.Models.ShippingDetails> ShippingDetails { get; set; }

    public System.Data.Entity.DbSet<VegeFoods.ViewModels.CategoryDetail> CategoryDetails { get; set; }
}
