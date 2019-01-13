using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Owin;
using OwinSelfhostedDemo;

namespace OwinSelfhostedDemoTest
{
    [TestClass]
    public class OwinTestDry
    {
        [TestMethod]
        public async Task Owin_Retunrs_200_on_request_to_root()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await server.HttpClient.GetAsync("/");
                Assert.Equals(HttpStatusCode.OK, response.StatusCode);
            }
        }


        [TestMethod]
        public async Task Owin_Retunrs_hello_on_request_to_root()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await server.HttpClient.GetAsync("/");
                var body = await response.Content.ReadAsStringAsync();
                Assert.Equals("Hello from owin Selfhost", body);
            }
        }


        [TestMethod]
        public async Task Owin_Retunrs_correct_content_on_request_to_jpg()
        {
            using (var server = TestServer.Create<Startup>())
            {
                var response = await server.HttpClient.GetAsync("/appFunc.jpg");
                var contenttype = response.Content.Headers.ContentType.MediaType;
                Assert.Equals("image/jpeg", contenttype);
            }
        }

    }
}
