using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VegeFoods.Models
{
    /// <summary>
    /// Summary description for VegeFoodsDTO
    /// </summary>

    public partial class Cart
    {
        [Key]
        public int CartId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<int> CartStatusId { get; set; }
        public Nullable<System.DateTime> AddedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public Nullable<int> ShippingDetailId { get; set; }

        public virtual CartStatus CartStatus { get; set; }
        public virtual Product Product { get; set; }
        public virtual Product Product1 { get; set; }
    }


    public partial class CartStatus
    {
        [Key]
        public int CartStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
    }


    public partial class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }

    public partial class MemberRole
    {
        [Key]
        public int MemberRoleId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public Nullable<int> RoleId { get; set; }

        public virtual Roles Roles { get; set; }
    }

    public partial class Members
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    }

    public partial class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> PriceSale { get; set; }
        public Nullable<bool> IsFeatured { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual Category Category { get; set; }
    }


    public partial class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<MemberRole> MemberRole { get; set; }
    }

    public partial class ShippingDetails
    {
        [Key]
        public int ShippingDetailId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string OrderId { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string PaymentType { get; set; }
    }

}