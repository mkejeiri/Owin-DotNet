using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;

namespace OwinSecondDemo.SimpleMiddleware
{
    public class SimpleMiddlewareOptions
    {
        public Action<IOwinContext> OnIncomingRequest { get; set; }
        public Action<IOwinContext> OutgoingRequest { get; set; }

    }
}