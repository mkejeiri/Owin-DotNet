using System.Diagnostics;
using Owin;

namespace OwinFirstDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.Use(async (ctx, next) =>
            {
                Debug.WriteLine("Incomming request: " + ctx.Request.Path);
                //to execute the rest of the pipeline
                await next();
                Debug.WriteLine("Outgoing response: " + ctx.Request.Path);
            });

            //since we will use await for the delegate we will use async! (vs before 2015 intellisense!)
            //ctx.Response: gives access to stream in dictionary stored as owin.reponseBody
            app.Use(async (ctx, next) => { await ctx.Response.WriteAsync("<html><head></head><body>Hello world!</body></html>"); });

        }
    }
} 