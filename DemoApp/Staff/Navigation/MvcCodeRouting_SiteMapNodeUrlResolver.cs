using MvcSiteMapProvider;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web.UrlResolver;
using System.IO;
using System.Web.Routing;

namespace DemoApp.Staff.Navigation
{
	public class MvcCodeRouting_SiteMapNodeUrlResolver : SiteMapNodeUrlResolver
	{
		public MvcCodeRouting_SiteMapNodeUrlResolver(IMvcContextFactory mvcContextFactory, IUrlPath urlPath)
			: base(mvcContextFactory, urlPath)
		{
		}

		protected override RequestContext CreateRequestContext(ISiteMapNode node, TextWriter writer)
		{
			var context = base.CreateRequestContext(node, writer);

			// pass token from node to the RequestContext instance
			if (node.Attributes[MvcCodeRoutingUtils.ROUTE_CONTEXT_TOKEN_KEY] != null)
				context.RouteData.DataTokens[MvcCodeRoutingUtils.ROUTE_CONTEXT_TOKEN_KEY] = node.Attributes[MvcCodeRoutingUtils.ROUTE_CONTEXT_TOKEN_KEY];

			return context;
		}
	}
}