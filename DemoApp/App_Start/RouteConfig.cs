using MvcCodeRouting;
using System.Web.Mvc;
using System.Web.Routing;

namespace DemoApp
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// remove classic ASP.NET MVC route configuration

			//routes.MapRoute(
			//	name: "Default",
			//	url: "{controller}/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);

			// setup MvcCodeRouting routes

			routes.MapCodeRoutes(
				typeof(DemoApp.Controllers.HomeController),
				new CodeRoutingSettings
				{
					UseImplicitIdToken = true
				}
			);
		}
	}
}
