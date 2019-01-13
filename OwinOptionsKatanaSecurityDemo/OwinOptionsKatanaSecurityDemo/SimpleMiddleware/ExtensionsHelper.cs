
using OwinOptionsKatanaSecurityDemo.SimpleMiddleware;
namespace Owin
{
    public static class ExtensionsHelper
    {
        public static void UseSimpleMiddleware(this IAppBuilder app, SimpleMiddlewareOptions options = null )
        {
            if (options == null)
            {
                options = new SimpleMiddlewareOptions();
            }
            app.Use<SimpleMiddleware>(options);
        }
    }
}