using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebHooks.Jobs;

namespace WebHooks
{
    public class Program
    {
        private const string LISTENING_PORT = "31415";
        public static JobQueue DeploymentQueue;
        public static void Main(string[] args)
        {
            DeploymentQueue = new JobQueue();

            Console.WriteLine($"Listening for webhooks on port: {LISTENING_PORT}");

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseUrls($"http://*:{LISTENING_PORT}")
                .UseStartup<Startup>();
    }
}
