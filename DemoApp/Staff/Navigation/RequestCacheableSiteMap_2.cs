using MvcSiteMapProvider;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Matching;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;
using System;
using System.Linq;

namespace DemoApp.Staff.Navigation
{
	public class RequestCacheableSiteMap_2 : RequestCacheableSiteMap
	{
		public RequestCacheableSiteMap_2(
			ISiteMapPluginProvider pluginProvider,
			IMvcContextFactory mvcContextFactory,
			ISiteMapChildStateFactory siteMapChildStateFactory,
			IUrlPath urlPath,
			ISiteMapSettings siteMapSettings,
			IRequestCache requestCache
		): base(
			pluginProvider, 
			mvcContextFactory, 
			siteMapChildStateFactory, 
			urlPath, 
			siteMapSettings, requestCache)
		{
		}

		protected override void AddNodeInternal(ISiteMapNode node, ISiteMapNode parentNode)
		{
			// This is almost one-to-one copy of the original method, except two things:
			// 1. isMvcUrl variable
			// 2. resource strings replaced with string literals for simplicity.

			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			lock (this.synclock)
			{
				IUrlKey url = null;

				// We use a custom url resolver for MVC urls. But original version have a little bug.
				// https://github.com/maartenba/MvcSiteMapProvider/issues/392

				bool isMvcUrl = string.IsNullOrEmpty(node.UnresolvedUrl); //&& node.UsesDefaultUrlResolver();

				// Only store URLs if they are clickable and are configured using the Url
				// property or provided by a custom URL resolver.
				if (!isMvcUrl && node.Clickable)
				{
					url = this.siteMapChildStateFactory.CreateUrlKey(node);

					// Check for duplicates (including matching or empty host names).
					if (this.urlTable
						.Where(k => string.Equals(k.Key.RootRelativeUrl, url.RootRelativeUrl, StringComparison.OrdinalIgnoreCase))
						.Where(k => string.IsNullOrEmpty(k.Key.HostName) || string.IsNullOrEmpty(url.HostName) || string.Equals(k.Key.HostName, url.HostName, StringComparison.OrdinalIgnoreCase))
						.Count() > 0)
					{
						var absoluteUrl = this.urlPath.ResolveUrl(node.UnresolvedUrl, string.IsNullOrEmpty(node.Protocol) ? Uri.UriSchemeHttp : node.Protocol, node.HostName);
						//throw new InvalidOperationException(string.Format(Resources.Messages.MultipleNodesWithIdenticalUrl, absoluteUrl));

						string errorMessage = "Multiple nodes with the same URL '{0}' were found. SiteMap requires that sitemap nodes have unique URLs.";
						throw new InvalidOperationException(string.Format(errorMessage, absoluteUrl));
					}
				}

				// Add the key
				string key = node.Key;
				if (this.keyTable.ContainsKey(key))
				{
					//throw new InvalidOperationException(string.Format(Resources.Messages.MultipleNodesWithIdenticalKey, key));

					string errorMessage = "Multiple nodes with the same key '{0}' were found. SiteMap requires that sitemap nodes have unique keys.";
					throw new InvalidOperationException(string.Format(errorMessage, key));
				}
				this.keyTable[key] = node;

				// Add the URL
				if (url != null)
				{
					this.urlTable[url] = node;
				}

				// Add the parent-child relationship
				if (parentNode != null)
				{
					this.parentNodeTable[node] = parentNode;
					if (!this.childNodeCollectionTable.ContainsKey(parentNode))
					{
						this.childNodeCollectionTable[parentNode] = siteMapChildStateFactory.CreateLockableSiteMapNodeCollection(this);
					}
					this.childNodeCollectionTable[parentNode].Add(node);
				}
			}
		}
	}
}