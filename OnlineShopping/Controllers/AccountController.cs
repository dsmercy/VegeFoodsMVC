using OnlineShopping.Models;
using OnlineShopping.Repository;
using OnlineShopping.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineShopping.Controllers
{
    public class AccountController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();


        #region Member Login ...         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                string EncryptedPassword = EncryptDecrypt.Encrypt(model.Password, true);
                var user = _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetFirstOrDefaultByParameter(i => i.EmailId == model.UserEmailId && i.Password == EncryptedPassword && i.IsDelete == false);
                if (user != null && user.IsActive == true)
                {
                    var roles = _unitOfWork.GetRepositoryInstance<Tbl_MemberRole>().GetFirstOrDefaultByParameter(i => i.MemberId == user.MemberId);
                    if (roles != null && roles.RoleId != 1)
                    {
                        FormsAuthentication.SetAuthCookie(user.FirstName,true);
                        Response.Cookies["MemberRole"].Value = _unitOfWork.GetRepositoryInstance<Tbl_Roles>().GetFirstOrDefaultByParameter(i => i.RoleId == roles.RoleId).RoleName;
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid username or password");
                        return View(model);
                    }
                    ViewBag.redirectUrl = (!string.IsNullOrEmpty(returnUrl) ? HttpUtility.HtmlDecode(returnUrl) : "/");
                }
                else
                {
                    if (user != null && user.IsActive == false) ModelState.AddModelError("Password", "Your account in not verified");
                    else ModelState.AddModelError("Password", "Invalid username or password");
                }
            }
            return PartialView("_Login", model);
        }
        #endregion



        #region Member Registration ...         
        [AllowAnonymous]
        public ActionResult Register()
        {
            RegisterViewModel model = new RegisterViewModel();
            model.UserType = 2;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Register(RegisterViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {                 // Adding Member                 
                Tbl_Members mem = new Tbl_Members();
                mem.FirstName = model.FirstName;
                mem.LastName = model.LastName;
                mem.EmailId = model.UserEmailId;
                mem.CreatedOn = DateTime.Now;
                mem.ModifiedOn = DateTime.Now;
                mem.Password = EncryptDecrypt.Encrypt(model.Password, true);
                mem.IsActive = true;
                mem.IsDelete = false;
                _unitOfWork.GetRepositoryInstance<Tbl_Members>().Add(mem);
                // Adding Member Role                 
                Tbl_MemberRole mem_Role = new Tbl_MemberRole();
                mem_Role.MemberId = mem.MemberId;
                mem_Role.RoleId = 2;
                _unitOfWork.GetRepositoryInstance<Tbl_MemberRole>().Add(mem_Role);

                TempData["VerificationLinlMsg"] = "You are registered successfully.";
                FormsAuthentication.SetAuthCookie(mem.FirstName, true);
                return RedirectToAction("Index", "Home");
            }
            return View("Register", model);
        }


        public JsonResult CheckEmailExist(string UserEmailId)
        {
            int LoginMemberId = Convert.ToInt32(Session["MemberId"]);
            var EmailExist = _unitOfWork.GetRepositoryInstance<Tbl_Members>().GetFirstOrDefaultByParameter(i => i.MemberId != LoginMemberId && i.EmailId == UserEmailId && i.IsDelete == false);
            return EmailExist == null ? Json(true, JsonRequestBehavior.AllowGet) : Json(false, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}