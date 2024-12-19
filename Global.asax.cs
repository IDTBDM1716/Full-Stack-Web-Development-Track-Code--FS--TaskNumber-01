using SecureUserAuthenticationSystem;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace secure_user_authentication_system
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Register all areas
            AreaRegistration.RegisterAllAreas();

            // Register global filters
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            // Register routes
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Register bundles for optimization
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
