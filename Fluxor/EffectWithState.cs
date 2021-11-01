using System.Threading.Tasks;

namespace Fluxor
{
	/// <summary>
	/// A generic class that can be used as a base for effects.
	/// </summary>
	/// <typeparam name="TTriggerAction"></typeparam>
	public abstract class EffectWithState<TTriggerAction,TState> : IEffectWithState
	{
		/// <summary>
		/// <see cref="IEffect.HandleAsync(object, IDispatcher)"/>
		/// </summary>
		public abstract Task HandleAsyncWithState(TTriggerAction action, TState state, IDispatcher dispatcher);

        /// => HandleAsyncWithState((TTriggerAction)action, (TState)state, dispatcher);

        /// <summary>
        /// <see cref="IEffect.ShouldReactToAction(object)"/>
        /// </summary>
        public bool ShouldReactToAction(object action) =>
			action is TTriggerAction;

		Task IEffectWithState.HandleAsyncWithState(object action, object state, IDispatcher dispatcher) =>
			HandleAsyncWithState((TTriggerAction)action, (TState)state, dispatcher);
	}
}
