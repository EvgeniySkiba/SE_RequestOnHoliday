using SE_RequestOnHoliday.Models;
using SE_RequestOnHoliday.Repository.Abstract;
using SE_RequestOnHoliday.Repository.Concrete;
using SE_RequestOnHoliday.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SE_RequestOnHoliday.Controllers
{
    public class AuthController : Controller
    {
        IAuthRepository repo;

        public AuthController()
        {
            repo = new AuthRepository();
        }
        [AllowAnonymous]
        // GET: Auth
        public ActionResult Login(string returnUrl)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginDTO login, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (repo.ValidateUser(login.UserName, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Employeer");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect login or password");
                }
            }
            return View(login);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }
    }
}