using System;

namespace Fluxor.DependencyInjection
{
	internal static class EffectWithStateWrapperFactory
	{
		internal static IEffectWithState Create(IServiceProvider serviceProvider, DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
		{
			Type actionType = discoveredEffectWithStateMethod.ActionType;
			Type stateType = discoveredEffectWithStateMethod.StateType; // TODO Wrong but to progress

			Type hostClassType = discoveredEffectWithStateMethod.HostClassType;
			object effectHostInstance = discoveredEffectWithStateMethod.MethodInfo.IsStatic
				? null
				: serviceProvider.GetService(hostClassType);

			Type classGenericType = typeof(EffectWithStateWrapper<,>).MakeGenericType(actionType, stateType);
			var result = (IEffectWithState)Activator.CreateInstance(
				classGenericType,
				effectHostInstance,
				discoveredEffectWithStateMethod);
			return result;
		}
	}
}
