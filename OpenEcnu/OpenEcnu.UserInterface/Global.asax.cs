using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using OpenEcnu.UserInterface.Common;

namespace OpenEcnu.UserInterface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelValidatorProviders.Providers.Add(new AntiXssDataAnnotationsModelValidatorProvider());
        }
    }
}
