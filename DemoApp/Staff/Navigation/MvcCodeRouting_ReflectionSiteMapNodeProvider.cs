using MvcSiteMapProvider;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DemoApp.Staff.Navigation
{
	public class MvcCodeRouting_ReflectionSiteMapNodeProvider : ReflectionSiteMapNodeProvider
	{
		public MvcCodeRouting_ReflectionSiteMapNodeProvider(
			IEnumerable<String> includeAssemblies,
			IEnumerable<String> excludeAssemblies,
			IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory,
			IMvcSiteMapNodeAttributeDefinitionProvider attributeNodeDefinitionProvider
		) :base (
			includeAssemblies,
			excludeAssemblies,
			attributeAssemblyProviderFactory,
			attributeNodeDefinitionProvider)
		{
		}

		protected override ISiteMapNodeToParentRelation GetSiteMapNodeFromMvcSiteMapNodeAttribute(
			IMvcSiteMapNodeAttribute attribute, 
			Type type, 
			MethodInfo methodInfo, 
			ISiteMapNodeHelper helper)
		{
			var nodeParentMap = base.GetSiteMapNodeFromMvcSiteMapNodeAttribute(attribute, type, methodInfo, helper);

			if (nodeParentMap != null && String.IsNullOrEmpty(nodeParentMap.Node.UrlResolver))
			{
				// extract token from type
				string controllerName = type.Name.Substring(0, type.Name.LastIndexOf("Controller"));
				string token = MvcCodeRoutingUtils.GetRouteContextToken(type.Namespace, controllerName);

				// save token to the node
				nodeParentMap.Node.Attributes.Add(MvcCodeRoutingUtils.ROUTE_CONTEXT_TOKEN_KEY, token);

				// set url resolver
				nodeParentMap.Node.UrlResolver = typeof(MvcCodeRouting_SiteMapNodeUrlResolver).AssemblyQualifiedName;
			}
			return nodeParentMap;
		}
	}
}