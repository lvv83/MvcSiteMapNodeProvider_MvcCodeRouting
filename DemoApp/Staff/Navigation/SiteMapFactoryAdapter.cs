using MvcSiteMapProvider;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;

namespace DemoApp.Staff.Navigation
{
	// Tip!

	// Name of this class should meet to the MvcSiteMapProvider Common Conventions:
	// 1. Matching type name (I[TypeName] = [TypeName]) 
	//   or
	// 2. Matching type name + suffix Adapter (I[TypeName] = [TypeName]Adapter)
	//   and
	// 3. Not decorated with the [ExcludeFromAutoRegistrationAttribute]

	// If class doesn't meet to the Common Conventions requirements, 
	// then it should be explicitly registered in the DI container.

	public class SiteMapFactoryAdapter : SiteMapFactory
	{
		public SiteMapFactoryAdapter(
			ISiteMapPluginProviderFactory pluginProviderFactory,
			IMvcResolverFactory mvcResolverFactory,
			IMvcContextFactory mvcContextFactory,
			ISiteMapChildStateFactory siteMapChildStateFactory,
			IUrlPath urlPath,
			IControllerTypeResolverFactory controllerTypeResolverFactory,
			IActionMethodParameterResolverFactory actionMethodParameterResolverFactory)
			: base (
				pluginProviderFactory, 
				mvcResolverFactory, 
				mvcContextFactory, 
				siteMapChildStateFactory, 
				urlPath, 
				controllerTypeResolverFactory, 
				actionMethodParameterResolverFactory)
		{
		}

		public override ISiteMap Create(ISiteMapBuilder siteMapBuilder, ISiteMapSettings siteMapSettings)
		{
			var routes = mvcContextFactory.GetRoutes();
			var requestCache = mvcContextFactory.GetRequestCache();

			// IMPORTANT: We need to ensure there is one instance of controllerTypeResolver and 
			// one instance of ActionMethodParameterResolver per SiteMap instance because each of
			// these classes does internal caching.
			var controllerTypeResolver = controllerTypeResolverFactory.Create(routes);
			var actionMethodParameterResolver = actionMethodParameterResolverFactory.Create();
			var mvcResolver = mvcResolverFactory.Create(controllerTypeResolver, actionMethodParameterResolver);
			var pluginProvider = pluginProviderFactory.Create(siteMapBuilder, mvcResolver);

			return new RequestCacheableSiteMap_2(
				pluginProvider,
				mvcContextFactory,
				siteMapChildStateFactory,
				urlPath,
				siteMapSettings,
				requestCache);
		}
	}
}