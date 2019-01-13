using Owin;

namespace OwinSelfhostedDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseStaticFiles();
            app.Use(async (ctx, next) => { await ctx.Response.WriteAsync("Hello from owin Selfhost"); });
        }
    }
} 
