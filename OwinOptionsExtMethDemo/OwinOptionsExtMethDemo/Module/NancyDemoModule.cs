using Nancy;
using Nancy.Owin;

/*
     Nancy framework is ideal if we want to give a different responses 
     for different paths, instead of doing an if statement inside app.Use()
 */

namespace OwinOptionsExtMethDemo.Module
{
    public class NancyDemoModule: NancyModule
    {
        public readonly string HelloFromNancy  = "Hello from Nancy!, You requested (Path):";
        public readonly string modulePath = "/Nancy";

        public NancyDemoModule()
        {
           Get[modulePath] = x =>
            {
                var env = Context.GetOwinEnvironment();
                return $"{HelloFromNancy} {env["owin.RequestPathBase"]}{env["owin.RequestPath"]}";
            };
        }
    }
}