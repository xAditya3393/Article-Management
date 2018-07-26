using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication6_Project.Models;
using WebApplication6_Project.ViewModels;

namespace WebApplication6_Project.Controllers
{
    public class AuthController : Controller
    {


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("home");
                
        }


        public ActionResult Login()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Login(AuthLogin form, string returnUrl)
        {

            if (!ModelState.IsValid)
                return View(form);


            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);

            if (user == null)
                WebApplication6_Project.Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Username", "Username or password is incorrect");

            if (!ModelState.IsValid)
                return View(form);

            FormsAuthentication.SetAuthCookie(user.Username, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            return RedirectToRoute("home");

        }
    }

}