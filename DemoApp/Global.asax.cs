using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DemoApp
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			#region Staff with MvcCodeRouting specific code

			// Don't use area feature and MvcCodeRouting together.
			// AreaRegistration.RegisterAllAreas();

			// register routes with MvcCodeRouting
			RouteConfig.RegisterRoutes(RouteTable.Routes);

			// adjust view engines for MvcCodeRouting support
			ViewConfig.AdjustViewEngines(ViewEngines.Engines);

			#endregion

			// and classic ASP.NET MVC staff
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
