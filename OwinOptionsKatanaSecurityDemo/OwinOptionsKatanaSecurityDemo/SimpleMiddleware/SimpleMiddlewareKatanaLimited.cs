using System;
using System.Threading.Tasks;
using Microsoft.Owin;
//Option 1 : use Katana implementation 
//Drawback: Katana specific!
namespace OwinOptionsKatanaSecurityDemo.SimpleMiddleware
{
    public class SimpleMiddlewareKatanaLimited : OwinMiddleware
    {
        public SimpleMiddlewareKatanaLimited(OwinMiddleware next) : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            throw new NotImplementedException();
        }
    }
}