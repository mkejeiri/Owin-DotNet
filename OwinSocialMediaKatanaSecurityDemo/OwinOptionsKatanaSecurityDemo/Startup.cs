using System.Diagnostics;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Twitter;
using Nancy;
using Nancy.Owin;
using Owin;
using OwinOptionsKatanaSecurityDemo.SimpleMiddleware;

namespace OwinOptionsKatanaSecurityDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            //options 1
            //app.Use<SimpleMiddleware.SimpleMiddleware>(new SimpleMiddlewareOptions());
            //app.Use<SimpleMiddleware.SimpleMiddleware>();
            //app.Use(async (ctx, next) =>
            //{
            //    Debug.WriteLine($"Incoming request path: {ctx.Request.Path}");
            //    await next();
            //    Debug.WriteLine($"Outgoing request path: {ctx.Request.Path}");
            //});


            //options 2
            //Add a watch
            //app.Use<SimpleMiddleware.SimpleMiddleware>(new SimpleMiddlewareOptions
            //{
            //    OnIncomingRequest = (ctx) =>
            //    {
            //        var watch = new Stopwatch();
            //        watch.Start();
            //        ctx.Environment["RequestResponseTime"] = watch;
            //    },
            //    OutgoingRequest = (ctx) =>
            //    {
            //        var watch = ctx.Environment["RequestResponseTime"] as Stopwatch;
            //        watch.Stop();
            //        Debug.WriteLine($"Request took: {watch.ElapsedMilliseconds} ms");
            //    }
            //});

            //Options 3 using extenstion method
            app.UseSimpleMiddleware(new SimpleMiddlewareOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    ctx.Environment["RequestResponseTime"] = watch;
                },
                OutgoingRequest = (ctx) =>
                {
                    var watch = ctx.Environment["RequestResponseTime"] as Stopwatch;
                    Debug.WriteLine($"Request took: {watch.ElapsedMilliseconds} ms");
                }
                
            });

            //Adding a cookies authentication after debug so we could debug it
            //Install-Package Microsoft.Owin.Security.Cookies -Version 4.0.0
            app.UseCookieAuthentication( new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Auth/Login")
            });

            app.UseFacebookAuthentication( new FacebookAuthenticationOptions
            {
                AppId = "2229529203974366",
                AppSecret = "14c3d857e2416df64f1780ac0dd909cb", 
                SignInAsAuthenticationType = "ApplicationCookie"
            });

            //app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            //{
            //    ConsumerKey = "2229529203974366",
            //    ConsumerSecret = "14c3d857e2416df64f1780ac0dd909cb",
            //    SignInAsAuthenticationType = "ApplicationCookie",
            //    BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
            //    {
            //        "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
            //        "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
            //        "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
            //        "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
            //        "5168FF90AF0207753CCCD9656462A212B859723B", //DigiCert SHA2 High Assurance Server C‎A 
            //        "B13EC36903F8BF4701D498261A0802EF63642BC3" //DigiCert High Assurance EV Root CA
            //    })
            //});

            //Or remove all certification : No recommanded
            app.UseTwitterAuthentication(new TwitterAuthenticationOptions
            {
                ConsumerKey = "2229529203974366",
                ConsumerSecret = "14c3d857e2416df64f1780ac0dd909cb",
                SignInAsAuthenticationType = "ApplicationCookie",
                BackchannelCertificateValidator = null
            });

            app.Use(async (ctx, next) =>
            {
                if (ctx.Authentication.User.Identity.IsAuthenticated)
                {
                    Debug.WriteLine("User :"  + ctx.Authentication.User.Identity.Name);
                }
                else
                {
                    Debug.WriteLine("User is not authenticated");
                }

                await next();
            });

            //HttpConfiguration contains all the configuration needed for Web API to run.
            var config = new HttpConfiguration();
            //cause the configuration to go through the application, looking for any API controllers
            //it can find and map the attributed routes that we put in there
            config.MapHttpAttributeRoutes();

            //Add the Web API to the OWIN pipeline
            app.UseWebApi(config);


            app.Map("/Nancy", (mappedApp) => {mappedApp.UseNancy();});
            //Better way : but missed up internally with authentication
            //app.UseNancy(cfg =>
            //{
            //    cfg.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            //});

            //app.UseNancy();
            //app.Use(async (ctx, next) => { await ctx.Response.WriteAsync("Hello world"); });
        }
    }
}