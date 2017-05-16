using SE_RequestOnHoliday.Models;
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
        [AllowAnonymous]
        // GET: Auth
        public ActionResult Login(string returnUrl)
        {
            LoginVM loginVM = new LoginVM();
            ViewBag.ReturnUrl = returnUrl;
            return View(loginVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM login, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                if (ValidateUser(login.UserName, login.Password))
                {
                    FormsAuthentication.SetAuthCookie(login.UserName, login.RememberMe);
               /*     if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {*/
                        return RedirectToAction("Index", "HR");
                   // }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный пароль или логин");
                }
            }
            return View(login);
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;

            using (EmployersContext _db = new EmployersContext())
            {
                try
                {
                    Employer user = (from u in _db.Employers
                                     where u.Login == login && u.Password == password
                                 select u).FirstOrDefault();

                    if (user != null)
                    {
                        isValid = true;
                    }
                }
                catch
                {
                    isValid = false;
                }
            }
            return isValid;
        }

        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("Login");
        }
    }
}