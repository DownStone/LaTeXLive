
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;

namespace LaTeXAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder_Develop(args).Build().Run();
            //CreateWebHostBuilder_Test(args).Build().Run();
            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder_Develop(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseUrls("https://*:5002")
                .UseDefaultServiceProvider(options =>
                {
                    options.ValidateScopes = false;
                })
                .UseStartup<Startup>();
        }

        public static IWebHostBuilder CreateWebHostBuilder_Test(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseUrls("https://*:5002")
            .UseKestrel()
            .UseDefaultServiceProvider(options =>
            {
                options.ValidateScopes = false;
            })
            .UseStartup<Startup>();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args).UseUrls("https://*:5002")
            .UseKestrel(delegate (KestrelServerOptions option)
             {
                 option.ConfigureHttpsDefaults(delegate (HttpsConnectionAdapterOptions i)
                 {
                     i.ServerCertificate = new X509Certificate2("C:\\work\\zs.pfx", "192781");
                 });
             })
            .UseDefaultServiceProvider(options =>
            {
                options.ValidateScopes = false;
            })
            .UseStartup<Startup>();
        }
    }
}
