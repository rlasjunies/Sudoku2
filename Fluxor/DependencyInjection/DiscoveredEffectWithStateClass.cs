using System;

namespace Fluxor.DependencyInjection
{
	internal class DiscoveredEffectWithStateClass
	{
		public readonly Type ImplementingType;

		public DiscoveredEffectWithStateClass(Type implementingType)
		{
			ImplementingType = implementingType;
		}
	}
}
