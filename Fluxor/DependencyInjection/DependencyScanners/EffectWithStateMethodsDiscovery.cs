using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fluxor.DependencyInjection.DependencyScanners
{
	internal static class EffectWithStateMethodsDiscovery
	{
		internal static DiscoveredEffectWithStateMethod[] DiscoverEffectWithStateMethods(
			IServiceCollection serviceCollection,
			IEnumerable<TypeAndMethodInfo> allCandidateMethods)
		{
			DiscoveredEffectWithStateMethod[] discoveredEffectsWithState =
				allCandidateMethods
					.Select(c =>
						new
						{
							HostClassType = c.Type,
							c.MethodInfo,
							EffectWithStateAttribute = c.MethodInfo.GetCustomAttribute<EffectWithStateMethodAttribute>(false)
						})
					.Where(x => x.EffectWithStateAttribute != null)
					.Select(x =>
						new DiscoveredEffectWithStateMethod(
							x.HostClassType,
							x.EffectWithStateAttribute,
							x.MethodInfo))
					.ToArray();

			IEnumerable<Type> hostClassTypes =
				discoveredEffectsWithState
					.Select(x => x.HostClassType)
					.Where(t => !t.IsAbstract)
					.Distinct();

			foreach (Type hostClassType in hostClassTypes)
				serviceCollection.AddScoped(hostClassType);

			return discoveredEffectsWithState;
		}
	}
}
