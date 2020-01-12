using Newtonsoft.Json;
using OnlineShopping.Models;
using OnlineShopping.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    public class HomeController : Controller
    {

        private GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();



        public ActionResult Index()
        {
            List<OnlineShopping.Models.Tbl_Product> products = _unitOfWork.GetRepositoryInstance<OnlineShopping.Models.Tbl_Product>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(products);
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult Wishlist()
        {
            return View();
        }
        
        public ActionResult ProductSingle(int productId)
        {

            Tbl_Product pd = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productId);
            ProductDetail product = new ProductDetail();
            product.CategoryId = pd.CategoryId;
            product.CreatedDate = pd.CreatedDate;
            product.Description = pd.Description;
            //product.IsActive = pd.IsActive;
            ////product.IsDelete = pd.IsDelete;
            //product.IsFeatured = pd.IsFeatured;
            product.ModifiedDate = pd.ModifiedDate;
            product.Price = pd.Price;
            product.PriceSale = pd.PriceSale;
            product.ProductId = pd.ProductId;
            product.ProductImage = pd.ProductImage;
            product.ProductName = pd.ProductName;
            product.RelatedProducts= _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetAllRecordsIQueryable().Where(i => i.CategoryId == pd.CategoryId).ToList();
            //_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstOrDefault(productId);
            return View(product);
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogSingle()
        {
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
    }
}