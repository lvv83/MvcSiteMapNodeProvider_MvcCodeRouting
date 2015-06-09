using MvcSiteMapProvider.Reflection;
using System;
using System.Collections.Generic;

namespace DemoApp.Staff.Navigation
{
	public class MvcCodeRouting_ReflectionSiteMapNodeProviderFactory
	{
		protected readonly IMvcSiteMapNodeAttributeDefinitionProvider attributeNodeDefinitionProvider;
		protected readonly IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory;

		public MvcCodeRouting_ReflectionSiteMapNodeProviderFactory(
			IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory,
			IMvcSiteMapNodeAttributeDefinitionProvider attributeNodeDefinitionProvider
		)
		{
			if (attributeAssemblyProviderFactory == null)
				throw new ArgumentNullException("attributeAssemblyProviderFactory");
			if (attributeNodeDefinitionProvider == null)
				throw new ArgumentNullException("attributeNodeDefinitionProvider");

			this.attributeAssemblyProviderFactory = attributeAssemblyProviderFactory;
			this.attributeNodeDefinitionProvider = attributeNodeDefinitionProvider;
		}

		public MvcCodeRouting_ReflectionSiteMapNodeProvider Create(IEnumerable<String> includeAssemblies, IEnumerable<String> excludeAssemblies)
		{
			return new MvcCodeRouting_ReflectionSiteMapNodeProvider(
				includeAssemblies,
				excludeAssemblies,
				this.attributeAssemblyProviderFactory,
				this.attributeNodeDefinitionProvider
			);
		}

		public MvcCodeRouting_ReflectionSiteMapNodeProvider Create(IEnumerable<String> includeAssemblies)
		{
			return new MvcCodeRouting_ReflectionSiteMapNodeProvider(
				includeAssemblies,
				new string[0],
				this.attributeAssemblyProviderFactory,
				this.attributeNodeDefinitionProvider
			);
		}
	}
}