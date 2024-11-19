using System.Web.Mvc;
using System.Web.Routing;

namespace BarberShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { Controller = "Home", action = "Index", id = UrlParameter.Optional },
             namespaces: new[] { "BarberShop.Areas.Customer.Controllers" }
         ).DataTokens.Add("Area", "Customer");
        }
    }
}
