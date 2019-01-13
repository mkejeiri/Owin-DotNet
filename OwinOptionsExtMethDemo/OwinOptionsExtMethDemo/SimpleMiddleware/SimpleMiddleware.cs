
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;
using AppFunc = System.Func<
System.Collections.Generic.IDictionary<string, object>,
System.Threading.Tasks.Task
>;
namespace OwinOptionsExtMethDemo.SimpleMiddleware
{
    //option2 : katana Implemetation independent 
    public class SimpleMiddleware
    {
        private AppFunc _next;
        private SimpleMiddlewareOptions _options;

        public SimpleMiddleware(AppFunc next, SimpleMiddlewareOptions options)
        {

            _next = next;
            _options = options;
            if (_options.OnIncomingRequest == null)
            {
                _options.OnIncomingRequest = (ctx) => { Debug.WriteLine($"Incoming request path: {ctx.Request.Path}"); };
            }

            if (_options.OutgoingRequest == null)
            {
                _options.OutgoingRequest = (ctx) => { Debug.WriteLine($"Outgoing request path: {ctx.Request.Path}"); };
            }
        }

        public async  Task Invoke(IDictionary<string,object> environment)
        {
            var ctx = new OwinContext(environment);
            _options.OnIncomingRequest(ctx);
            //Debug.WriteLine($"Incoming request path: {ctx.Request.Path}");
            await _next(environment);
            //Debug.WriteLine($"Outgoing request path: {ctx.Request.Path}");
            _options.OutgoingRequest(ctx);
            //Other solution to ctx.Request.Path is :
            //var path = (string)environment["owin.ResquestPath"];
            //Debug.WriteLine($"Incoming request path: {path}");
            //await _next(environment);
            //Debug.WriteLine($"Outgoing request path: {path}");
        }
    }
}