using Newtonsoft.Json;
using OnlineShopping.Models;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    //[AuthorizeUser(Roles = "Admin")]
    public class AdminController : Controller
    {
        #region Other Class references ...
        // Instance on Unit of Work
        private GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        //UploadContent uc = new UploadContent();
        #endregion

        

        #region Admin Change Password ...
        // <summary>
        // Change Password
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="cpm"></param>
        /// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        //public ActionResult ChangePassword(ChangePasswordViewModel cpm)
        //{
        //    int LoginMemberId = Convert.ToInt32(Session["MemberId"]);
        //    var ExistingDetails = _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetFirstOrDefaultByParameter(i => i.MemberId == LoginMemberId && i.IsActive == true && i.IsDelete == false);
        //    if (EncryptDecrypt.Encrypt(cpm.OldPassword, true) == ExistingDetails.Password)
        //    {
        //        ExistingDetails.Password = EncryptDecrypt.Encrypt(cpm.NewPassword, true);
        //        _unitOfWork.SaveChanges();
        //        ViewBag.PasswordChangeMsg = "Password changed successfully";
        //    }
        //    else
        //        ModelState.AddModelError("OldPassword", "Current password is wrong");
        //    return View();
        //}
        #endregion

        #region Admin Dashboard ...
        /// <summary>
        /// Admin Dashboard
        /// </summary>
        /// <returns></returns>
        public ActionResult Dashboard()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// Categories
        /// </summary>
        /// <returns></returns>
        #region Manage Categories ...
        public ActionResult Categories()
        {
            List<OnlineShopping.Models.Tbl_Category> AllCategories = _unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(AllCategories);
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != 0)
                cd =
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Category>().GetFirstOrDefault(categoryId)));
            else
                cd = new CategoryDetail();
            return View("UpdateCategory", cd);
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <param name="cd"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateCategory(CategoryDetail cd)
        {
            if (ModelState.IsValid)
            {
                Tbl_Category cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstOrDefault(cd.CategoryId);
                cat = cat != null ? cat : new Tbl_Category();
                cat.CategoryName = cd.CategoryName;
                if (cd.CategoryId != 0)
                {
                    _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(cat);
                    _unitOfWork.SaveChanges();
                }
                else
                {
                    cat.IsActive = true;
                    cat.IsDelete = false;
                    _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(cat);
                }

                return RedirectToAction("Categories");
            }
            else
                return View("UpdateCategory", cd);
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int DeleteCategory(int itemId)
        {
            _unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Category>().InactiveAndDeleteMarkByWhereClause(i => i.CategoryId == itemId, (u => { u.IsActive = false; u.IsDelete = true; }));
            return 1;
        }

        /// <summary>
        /// Check Category Exist
        /// </summary>
        /// <param name="CategoryName"></param>
        /// <returns></returns>
        public JsonResult CheckCategoryExist(string CategoryName)
        {
            int CategoryId = 0;
            if (HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["categoryId"] != null)
                CategoryId = Convert.ToInt32(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["categoryId"]);
            var CategoryExist = _unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Category>().GetAllRecordsIQueryable().Where(i => i.CategoryName == CategoryName && i.CategoryId != CategoryId && i.IsActive == true && i.IsDelete == false).Count();
            return CategoryExist == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Manage Products...
        #region Product Listing...
        /// <summary>
        /// Product Listing
        /// </summary>
        /// <returns></returns>
        public ActionResult Products()
        {
            List<OnlineShopping.Models.Tbl_Product> products = _unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Product>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(products);
        }
        #endregion

        #region Add/Update Product...
        /// <summary>
        /// Add Product
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProduct()
        {
            return UpdateProduct(0);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult UpdateProduct(int productId)
        {
            ProductDetail pd = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetListByParameter(i => i.ProductId == productId).Select(j => new ProductDetail { CategoryId = j.CategoryId, Description = j.Description, IsActive = j.IsActive ?? default(bool), Price = j.Price ?? default(decimal), ProductId = j.ProductId, ProductImage = j.ProductImage, ProductName = j.ProductName, IsFeatured = j.IsFeatured ?? default(bool) }).FirstOrDefault();
            pd = pd != null ? pd : new ProductDetail();
            pd.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable(), "CategoryId", "CategoryName");
            return View("UpdateProduct", pd);
        }

        /// <summary>
        /// Updating Product details to DB
        /// </summary>
        /// <param name="pd"></param>
        /// <param name="_ProductImage"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProduct(ProductDetail pd, HttpPostedFileBase _ProductImage)
        {
            if (ModelState.IsValid)
            {
                var ImagePath = Guid.NewGuid().ToString() + Path.GetFileName(_ProductImage.FileName);
                Tbl_Product prod = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(pd.ProductId);
                prod = prod != null ? prod : new Tbl_Product();
                prod.CategoryId = pd.CategoryId;
                prod.Description = pd.Description;
                prod.IsActive = pd.IsActive;
                prod.IsFeatured = pd.IsFeatured;
                prod.Price = pd.Price;
                prod.PriceSale = pd.PriceSale;
                prod.ProductImage = _ProductImage != null ? ImagePath : prod.ProductImage;
                prod.ProductName = pd.ProductName;
                prod.ModifiedDate = DateTime.Now;
                if (prod.ProductId == 0)
                {
                    prod.CreatedDate = DateTime.Now;
                    prod.IsDelete = false;
                    _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(prod);
                }
                else
                {
                    _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(prod);
                    _unitOfWork.SaveChanges();
                }
                    
                if (_ProductImage != null)
                {
                    //string pic = null;
                    //pic = System.IO.Path.GetFileName(_ProductImage.FileName);
                    //string path = System.IO.Path.Combine(Server.MapPath("~/ProductImage/"), pic);
                    //// file is uploaded
                    //_ProductImage.SaveAs(path);
                    string path = Path.Combine(Server.MapPath("~/ProductImage"), ImagePath);
                    _ProductImage.SaveAs(path);
                }



                //uc.UploadImage(_ProductImage, prod.ProductId + "_", "/Content/ProductImage/", Server, _unitOfWork, 0, prod.ProductId, 0);
                return RedirectToAction("Products");
            }
            pd.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable(), "CategoryId", "CategoryName");
            return View("UpdateProduct", pd);
        }

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public int DeleteProduct(int itemId)
        {
            int result = _unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Product>().InactiveAndDeleteMarkByWhereClause(i => i.ProductId == itemId, (u => { u.IsActive = false; u.IsDelete = true; }));
            if (result == 1)
            { return 1; }
            else
            { return 0; }
        }


        /// <summary>
        /// Check Product Exist
        /// </summary>
        /// <param name="ProductName"></param>
        /// <returns></returns>
        public JsonResult CheckProductExist(string ProductName)
        {
            int productId = 0;
            if (HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["productId"] != null)
                productId = Convert.ToInt32(HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["productId"]);
            var productExist = _unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Product>().GetAllRecordsIQueryable().Where(i => i.ProductName == ProductName && i.ProductId != productId && i.IsActive == true && i.IsDelete == false).Count();
            return productExist == 0 ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region Product Details...
        /// <summary>
        /// Product Detail
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult ProductDetail(int productId)
        {
            Tbl_Product pd = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productId);
            return View(pd);
        }
        #endregion

        /// <summary>
        /// All orders
        /// </summary>
        /// <returns></returns>
        public ActionResult Orders()
        {
            List<Tbl_Cart> AllOrders = _unitOfWork.GetRepositoryInstance<Tbl_Cart>().GetListByParameter(i => i.CartStatusId == 3).ToList();
            return View(AllOrders);
        }

        /// <summary>
        /// Order details of a product
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ActionResult OrderDetail(int productId)
        {
            List<Tbl_Cart> ProductOrders = _unitOfWork.GetRepositoryInstance<Tbl_Cart>().GetListByParameter(i => i.CartStatusId == 3 && i.ProductId == productId).ToList();
            return View(ProductOrders);
        }

        #region Disposing UnitOfWork Context ...
        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
        #endregion
    }
}
