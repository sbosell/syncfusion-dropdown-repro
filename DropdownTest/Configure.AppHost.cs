using Funq;
using ServiceStack;
using DropdownTest.ServiceInterface;

[assembly: HostingStartup(typeof(DropdownTest.AppHost))]

namespace DropdownTest;

public class AppHost : AppHostBase, IHostingStartup
{
    public AppHost() : base("DropdownTest", typeof(MyServices).Assembly) { }

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
        });

        Plugins.Add(new CorsFeature(allowedHeaders: "Content-Type,Authorization",
            allowOriginWhitelist: new[]{
            "http://localhost:5000",
            "https://localhost:5001",
            "https://" + Environment.GetEnvironmentVariable("DEPLOY_CDN")
        }, allowCredentials: true));
    }

    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => 
            services.ConfigureNonBreakingSameSiteCookies(context.HostingEnvironment));
}
