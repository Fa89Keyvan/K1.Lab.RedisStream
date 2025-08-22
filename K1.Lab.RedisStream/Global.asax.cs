using System.Web.Mvc;
using System.Web.Routing;

namespace K1.Lab.RedisStream.Consumer.Controllers
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
