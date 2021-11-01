using System;
using System.Threading.Tasks;

namespace Fluxor.DependencyInjection
{
	internal class EffectWithStateWrapper<TAction, TState> : IEffectWithState
	{
		private delegate Task HandleWithActionParameterAsyncHandler(TAction action, TState state, IDispatcher dispatcher);
		private delegate Task HandleWithoutActionParameterAsyncHandler(IDispatcher dispatcher);
		private readonly HandleWithActionParameterAsyncHandler HandleAsync;

		Task IEffectWithState.HandleAsyncWithState(object action, object state, IDispatcher dispatcher) => HandleAsync((TAction)action, (TState) state, dispatcher);
		bool IEffectWithState.ShouldReactToAction(object action) => action is TAction;

		public EffectWithStateWrapper(object effectWithStateHostInstance, DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
		{
			HandleAsync =
				discoveredEffectWithStateMethod.RequiresActionParameterInMethod
				? CreateHandlerWithActionParameter(effectWithStateHostInstance, discoveredEffectWithStateMethod)
				: WrapEffectWithStateWithoutActionParameter(effectWithStateHostInstance, discoveredEffectWithStateMethod); ;
		}

		private static HandleWithActionParameterAsyncHandler WrapEffectWithStateWithoutActionParameter(
			object effectWithStateHostInstance,
			DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
		{
			HandleWithoutActionParameterAsyncHandler handler = CreateHandlerWithoutActionParameter(
				effectWithStateHostInstance,
				discoveredEffectWithStateMethod);

			return new HandleWithActionParameterAsyncHandler((action, state, dispatcher) => handler.Invoke(dispatcher));
		}

		private static HandleWithActionParameterAsyncHandler CreateHandlerWithActionParameter(
			object effectWithStateHostInstance,
			DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
			=>
				effectWithStateHostInstance == null
				? CreateStaticHandlerWithActionParameter(discoveredEffectWithStateMethod)
				: CreateInstanceHandlerWithActionParameter(effectWithStateHostInstance, discoveredEffectWithStateMethod);

		private static HandleWithActionParameterAsyncHandler CreateStaticHandlerWithActionParameter(
			DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
			=>
				(HandleWithActionParameterAsyncHandler)
					Delegate.CreateDelegate(
						type: typeof(HandleWithActionParameterAsyncHandler),
						method: discoveredEffectWithStateMethod.MethodInfo);

		private static HandleWithActionParameterAsyncHandler CreateInstanceHandlerWithActionParameter(
			object effectWithStateHostInstance,
			DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
			=>
				(HandleWithActionParameterAsyncHandler)
					Delegate.CreateDelegate(
						type: typeof(HandleWithActionParameterAsyncHandler),
						firstArgument: effectWithStateHostInstance,
						method: discoveredEffectWithStateMethod.MethodInfo);

		private static HandleWithoutActionParameterAsyncHandler CreateHandlerWithoutActionParameter(
			object effectWithStateHostInstance,
			DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
			=>
				effectWithStateHostInstance == null
				? CreateStaticHandlerWithoutActionParameter(discoveredEffectWithStateMethod)
				: CreateInstanceHandlerWithoutActionParameter(effectWithStateHostInstance, discoveredEffectWithStateMethod);

		private static HandleWithoutActionParameterAsyncHandler CreateStaticHandlerWithoutActionParameter(
			DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
			=>
				(HandleWithoutActionParameterAsyncHandler)
					Delegate.CreateDelegate(
						type: typeof(HandleWithoutActionParameterAsyncHandler),
						method: discoveredEffectWithStateMethod.MethodInfo);

		private static HandleWithoutActionParameterAsyncHandler CreateInstanceHandlerWithoutActionParameter(
			object effectWithStateHostInstance,
			DiscoveredEffectWithStateMethod discoveredEffectWithStateMethod)
			=>
				(HandleWithoutActionParameterAsyncHandler)
					Delegate.CreateDelegate(
						type: typeof(HandleWithoutActionParameterAsyncHandler),
						firstArgument: effectWithStateHostInstance,
						method: discoveredEffectWithStateMethod.MethodInfo);
	}
}
