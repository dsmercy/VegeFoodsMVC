using OnlineShopping.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

public class Online_ShoppingEntities : DbContext
{
    // You can add custom code to this file. Changes will not be overwritten.
    // 
    // If you want Entity Framework to drop and regenerate your database
    // automatically whenever you change your model schema, please use data migrations.
    // For more information refer to the documentation:
    // http://msdn.microsoft.com/en-us/data/jj591621.aspx

    public Online_ShoppingEntities() : base("name=Online_ShoppingEntities")
    {
    }
    public virtual DbSet<Tbl_Cart> Tbl_Cart { get; set; }
    public virtual DbSet<Tbl_CartStatus> Tbl_CartStatus { get; set; }
    public virtual DbSet<Tbl_Category> Tbl_Category { get; set; }
    public virtual DbSet<Tbl_MemberRole> Tbl_MemberRole { get; set; }
    public virtual DbSet<Tbl_Members> Tbl_Members { get; set; }
    public virtual DbSet<Tbl_Product> Tbl_Product { get; set; }
    public virtual DbSet<Tbl_Roles> Tbl_Roles { get; set; }
    public virtual DbSet<Tbl_ShippingDetails> Tbl_ShippingDetails { get; set; }
}
