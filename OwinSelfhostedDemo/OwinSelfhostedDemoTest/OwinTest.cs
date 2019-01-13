using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using OwinSelfhostedDemo;

namespace OwinSelfhostedDemoTest
{
    [TestClass]
    public class OwinTest
    {
        [TestMethod]
        public async Task Owin_Retunrs_200_on_request_to_root()
        {
            var statusCode  = await CallServer(async (x) =>
            {
                 var response = await x.GetAsync("/");
                return response.StatusCode;
            });
            Assert.Equals(HttpStatusCode.OK, statusCode);
        }


        [TestMethod]
        public async Task Owin_Retunrs_hello_on_request_to_root()
        {
           var body = await CallServer(async (x) =>
            {
                var response = await x.GetAsync("/");
                return response.Content.ReadAsStringAsync();
            });
            Assert.Equals("Hello from owin Selfhost", body);
        }


        [TestMethod]
        public async Task Owin_Retunrs_correct_content_on_request_to_jpg()
        {
            var contenttype = await CallServer(async (x) =>
            {
                var response = await x.GetAsync("/appFunc.jpg");
                return response.Content.Headers.ContentType.MediaType;
            });
            Assert.Equals("image/jpeg", contenttype);
        }

       //Create a server
        private async Task<T> CallServer<T>(Func<HttpClient, Task<T>> callback)
        {
            using (var server = TestServer.Create<Startup>())
            {
                return await callback(server.HttpClient);
            }
        }
    }
}
