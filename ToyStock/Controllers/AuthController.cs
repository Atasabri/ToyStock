using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ToyStock.Models;

namespace ToyStock.Controllers
{
    public class AuthController : Controller
    {
        DB db = new DB();
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (db.Admins.Any(x => x.UserName == UserName && x.Password == Password))
                {
                    FormsAuthentication.SetAuthCookie(UserName, false);
                    if (ReturnUrl != null && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admins");
                    }
                }
                else
                {
                    ViewBag.error = "User Name And Password Is Not Valid !!";
                }
            }
            return View();
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}