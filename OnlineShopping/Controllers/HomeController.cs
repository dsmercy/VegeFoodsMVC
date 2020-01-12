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

        public ActionResult ProductSingle()
        {
            return View();
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