using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using OwinOptionsKatanaSecurityDemo.Models;

namespace OwinOptionsKatanaSecurityDemo.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel()
            //{
            //    Username = "Mo",
            //    Password = "pass"
            //}
                ;

            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.Username.Equals("Mo", StringComparison.OrdinalIgnoreCase)
            && model.Password.Equals("pass", StringComparison.OrdinalIgnoreCase))
            {
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaims(new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Username),
                    new Claim(ClaimTypes.Name, model.Username)
                });

                HttpContext.GetOwinContext().Authentication.SignIn(identity);
            }
            return View(model);
        }

        //Logout
        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

    }
}