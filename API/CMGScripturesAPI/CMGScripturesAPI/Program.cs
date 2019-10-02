using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CMGScripturesAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options => {
                    // manually declare that this will run on port 5000
                    // this service will fail to start if another service is currently running on 5000
                    options.Listen(IPAddress.Loopback, 5000);
                })
                .UseStartup<Startup>();
    }
}
