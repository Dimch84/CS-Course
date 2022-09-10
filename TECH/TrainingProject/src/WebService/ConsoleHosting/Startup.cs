using Owin;
using ProductsService;
using System.Web.Http;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(ConsoleHosting.Startup))]

namespace ConsoleHosting
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration = new HttpConfiguration();

            WebApiConfig.Register(HttpConfiguration);

            app.UseWebApi(HttpConfiguration);

            //HttpConfiguration.MessageHandlers.Add(new LogRequestAndResponseHandler());
        }
    }
}
