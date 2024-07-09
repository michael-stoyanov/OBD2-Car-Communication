namespace ObdLogApi
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //            .UseKestrel()
            //.UseIISIntegration()
            //.UseKestrel((context, options) =>
            //{
            //    options.Limits.MaxRequestBodySize = null;
            //})
            .UseStartup<Startup>();
    }
}
