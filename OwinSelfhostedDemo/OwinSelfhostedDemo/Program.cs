using System;
using Microsoft.Owin.Hosting;

namespace OwinSelfhostedDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12345"))
            {
                Console.WriteLine("Listening to port 12345...");
                Console.WriteLine("press any key to continue ...");
                Console.ReadLine();
            }
        }
    }
}
