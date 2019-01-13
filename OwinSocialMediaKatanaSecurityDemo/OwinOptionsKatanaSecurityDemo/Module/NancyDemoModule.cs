using System.Web;
using Nancy;
using Nancy.Owin;
using Nancy.Security;

/*
     Nancy framework is ideal if we want to give a different responses 
     for different paths, instead of doing an if statement inside app.Use()
 */

namespace OwinOptionsKatanaSecurityDemo.Module
{
    public class NancyDemoModule: NancyModule
    {
        public readonly string HelloFromNancy  = "Hello from Nancy!, You requested (Path):";
        public readonly string modulePath = "/Nancy";

        public NancyDemoModule()
        {
            //make sure that the user is authenticated before trying to use anything inside of this module
            this.RequiresMSOwinAuthentication();
           Get[modulePath] = x =>
           {
               var user = Context.GetMSOwinUser();
                var env = Context.GetOwinEnvironment();
                return $"{HelloFromNancy} {env["owin.RequestPathBase"]}{env["owin.RequestPath"]}, <br><br> User : {user.Identity.Name} ";
            };
        }
    }
}