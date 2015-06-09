using System;

namespace DemoApp.Staff.Navigation
{
	class MvcCodeRoutingUtils
	{
		internal const string ROUTE_CONTEXT_TOKEN_KEY = "MvcCodeRouting.RouteContext";

		internal static string GetRouteContextToken(string controllerNamespace, string controllerName)
		{
			int controllersIndex = controllerNamespace.LastIndexOf(".Controllers.");
			if (controllersIndex == -1)
			{
				// this is a top level controller
				return String.Empty;
			}

			// for example if:
			// controllerNamespace = "DemoApp.Controllers.Sub1.Sub2.Sub3"

			// then selfNamespace is "Sub1.Sub2.Sub3"
			string selfNamespace = controllerNamespace.Substring(controllersIndex + 13);

			// selfNamespace = parentNamespace + "." + selfNamespaceLast
			int parentIndex = selfNamespace.LastIndexOf('.');
			string parentNamespace = String.Empty;
			string selfNamespaceLast = selfNamespace;

			if (parentIndex != -1)
			{
				// "Sub1.Sub2"
				parentNamespace = selfNamespace.Substring(0, parentIndex);
				// "Sub3"
				selfNamespaceLast = selfNamespace.Substring(parentIndex + 1);
			}

			// check for default controller
			return controllerName.Equals(selfNamespaceLast, StringComparison.InvariantCulture) 
				? parentNamespace // default
				: selfNamespace; // non-default
		}
	}
}