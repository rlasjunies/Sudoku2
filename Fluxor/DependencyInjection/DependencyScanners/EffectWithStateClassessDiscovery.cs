using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Fluxor.DependencyInjection.DependencyScanners
{
	internal static class EffectWithStateClassessDiscovery
	{

		internal static DiscoveredEffectWithStateClass[] DiscoverEffectWithStateClasses(
			IServiceCollection serviceCollection, IEnumerable<Type> allCandidateTypes)
		{
			
			DiscoveredEffectWithStateClass[] discoveredEffectWithStateInfos =
				allCandidateTypes
					.Where(t => typeof(IEffectWithState).IsAssignableFrom(t))
					.Where(t => t != typeof(EffectWithStateWrapper<,>))
					.Select(t => new DiscoveredEffectWithStateClass(implementingType: t))
					.ToArray();

			foreach (var t in discoveredEffectWithStateInfos)
			{
				Console.WriteLine($"{t.GetType().FullName}");
			}
			foreach (DiscoveredEffectWithStateClass discoveredEffectWithStateInfo in discoveredEffectWithStateInfos)
				serviceCollection.AddScoped(discoveredEffectWithStateInfo.ImplementingType);

			return discoveredEffectWithStateInfos;
		}
	}
}
