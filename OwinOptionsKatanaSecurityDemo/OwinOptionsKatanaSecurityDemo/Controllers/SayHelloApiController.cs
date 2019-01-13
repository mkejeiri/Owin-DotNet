using System.Net;
using System.Web.Http;

namespace OwinOptionsKatanaSecurityDemo.Controllers
{
    [RoutePrefix("api")]
    public class SayHelloApiController : ApiController
    {
        [Route("hello")]
        [HttpGet]
        public IHttpActionResult SayHello()
        {
            return Content(HttpStatusCode.OK, "Hello from Web Api");
        }
    }
}
