using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using OwinOptionsKatanaSecurityDemo.Models;

namespace OwinOptionsKatanaSecurityDemo.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        [HttpGet]
        public ActionResult Login()
        {
            var model = new LoginModel();
            var providers = HttpContext.GetOwinContext()
                .Authentication.GetAuthenticationTypes(x => !string.IsNullOrEmpty(x.Caption))
                .ToList();
            model.AuthProviders = providers;
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

        //public ActionResult LoginFacebook()
        //{
        //    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
        //    { RedirectUri = "/secret" }, "Facebook");
        //    return new HttpUnauthorizedResult();
        //}

        //public ActionResult LoginTwitter()
        //{
        //    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
        //        { RedirectUri = "/secret" }, "Twitter");
        //    return new HttpUnauthorizedResult();
        //}

        //Social media manage any kind of social media  (Caption not empty) : Facebook + twitter
        public ActionResult SocialLogin(string id)
        {
            HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties
                { RedirectUri = "/secret" }, id );
            return new HttpUnauthorizedResult();
        } 
    }
}