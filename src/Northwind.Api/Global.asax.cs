using Northwind.Api.App_Start;
using System.Web.Http;

namespace Northwind.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutofacConfig.Bootstrapper();
        }
    }
}
