using System.Diagnostics;
using System.Web.UI.WebControls;
using Owin;
using OwinSecondDemo.SimpleMiddleware;

namespace OwinSecondDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            //app.Use<SimpleMiddleware.SimpleMiddleware>(new SimpleMiddlewareOptions());

            //Add a watch

            app.Use<SimpleMiddleware.SimpleMiddleware>(new SimpleMiddlewareOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopWatch"] = watch;
                },
                OutgoingRequest = (ctx) =>
                {
                    var watch = ctx.Environment["DebugStopWatch"] as Stopwatch;
                    watch.Stop();
                    Debug.WriteLine($"Request took: {watch.ElapsedMilliseconds} ms");
                }
            });

            //app.Use<SimpleMiddleware.SimpleMiddleware>();

            //app.Use(async (ctx, next) =>
            //{
            //    Debug.WriteLine($"Incoming request path: {ctx.Request.Path}");
            //    await next();
            //    Debug.WriteLine($"Outgoing request path: {ctx.Request.Path}");
            //});

            app.Use(async (ctx, next) => { await ctx.Response.WriteAsync("Hello world"); });
        }
    }
}